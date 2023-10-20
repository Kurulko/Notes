import { Component, OnInit, TemplateRef, ViewChild, Input, ContentChild } from '@angular/core';
import { DbModel } from 'src/app/models/database/db-model';
import { IndexViewModel } from 'src/app/models/helpers/index-view-model';
import { EditModelComponent } from '../edit-model.component';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { UnaryFunction } from 'rxjs';
import { Observable } from 'rxjs'

@Component({
    selector: 'models-app',
    templateUrl: "./models.component.html",
})
export class ModelsComponent<T extends DbModel, K extends string|number> extends EditModelComponent implements OnInit {
    @Input() 
    getModels: () => Observable<IndexViewModel<T>>;

    @Input() 
    createModel: UnaryFunction<T, Observable<Object>>;

    @Input() 
    updateModel: UnaryFunction<T, Observable<Object>>;

    @Input() 
    removeModel: UnaryFunction<K, Observable<Object>>;

    @Input() 
    isSaveModel: () => boolean;

    @ViewChild('readOnlyTemplate', {static: false})
    readOnlyTemplate: TemplateRef<any>|null;

    @ViewChild('editTemplate', {static: false})
    editTemplate: TemplateRef<any>|null;

    @ContentChild('readOnlyTemplate') 
    readOnlyTemplateChild: TemplateRef<any>;

    @ContentChild('editTemplate')
    editTemplateChild: TemplateRef<any>;

    @ContentChild('tableHeader')
    tableHeaderChild: TemplateRef<any>;

    models: T[];
    editedModel: T|null = null;
    isNewRecord: boolean = false;

    get isEditedModel(): boolean {
        return this.editedModel != null;
    }
      
    constructor(snackBar: MatSnackBar){
        super(snackBar);
    }

    ngOnInit(): void {
       this.loadModels();
    }

    private loadModels(){
        this.getModels()
            .pipe(this.catchError())
            .subscribe((indexViewModel: IndexViewModel<T>) => {
                this.models = indexViewModel.models;
            })
    }

    private createEmptyModel(): T{
        const emptyModel = {} as T;
        return emptyModel;
    }

    addModel(){
        this.editedModel = this.createEmptyModel();
        this.models.push(this.editedModel!);
        this.isNewRecord = true;
    }

    editModel(model: T){
        this.editedModel = {...model};
    }

    loadTemplate(model: T) {
        if(this.editedModel && this.editedModel.id === model.id)
            return this.editTemplate;
        return this.readOnlyTemplate;
    }

    saveModel() {
        (this.isNewRecord ? 
            this.createModel(this.editedModel as T) : 
            this.updateModel(this.editedModel as T))
                .pipe(this.catchError())
                .subscribe(_ => {
                    this.loadModels();
                    this.modelUpdatedSuccessfully()
                });
        if(this.isNewRecord) {
            this.isNewRecord = false;
        }
        this.editedModel = null;
    }
    
    cancel() {
        if(this.isNewRecord){
            this.models.pop();
            this.isNewRecord = false;
        }
        this.editedModel = null;
    }

    deleteModel(model: T){
        this.removeModel(model!.id as K)
            .pipe(this.catchError())
            .subscribe(_ => {
                this.loadModels();
                this.modelDeletedSuccessfully()
            });
    }
}
