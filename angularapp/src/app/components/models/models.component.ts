import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { DbModel } from 'src/app/models/database/db-model';
import { IndexViewModel } from 'src/app/models/helpers/index-view-model';
import { ModelsService } from 'src/app/services/models/models.service';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';

@Component({
    selector: 'app-models',
    template: ""
})
export abstract class ModelsComponent<T extends DbModel, K extends string|number> implements OnInit {
    @ViewChild('readOnlyTemplate', {static: false})
    readOnlyTemplate: TemplateRef<any>|null;

    @ViewChild('editTemplate', {static: false})
    editTemplate: TemplateRef<any>|null;

    models: T[];
    editedModel: T|null = null;
    isNewRecord: boolean = false;

    constructor(protected modelsService: ModelsService<T, K>){
    }

    ngOnInit(): void {
       this.loadModels();
    }

    private loadModels(){
        this.modelsService.getModels().subscribe((indexViewModel: IndexViewModel<T>) => {
            this.models = indexViewModel.models;
        })
    }

    abstract createEmptyModel(): T;

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
        if(this.isNewRecord) {
            this.modelsService.createModel(this.editedModel as T)
                .subscribe(_ => {
                    this.loadModels();
                });
            this.isNewRecord = false;
        }
        else {
            this.modelsService.updateModel(this.editedModel as T)
            .subscribe(_ => {
                this.loadModels();
            });
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
        this.modelsService.deleteModel(model!.id as K)
            .subscribe(_ => {
                this.loadModels();
            });
    }
}
