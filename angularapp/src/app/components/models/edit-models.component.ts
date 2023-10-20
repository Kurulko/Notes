import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { DbModel } from 'src/app/models/database/db-model';
import { IndexViewModel } from 'src/app/models/helpers/index-view-model';
import { ModelsService } from 'src/app/services/models/models.service';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { EditModelComponent } from '../edit-model.component';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { catchError } from 'rxjs/operators';
import { OperatorFunction, of } from 'rxjs';
import { ModelsComponent } from './models.component';


export abstract class EditModelsComponent<T extends DbModel, K extends string|number> extends ModelsComponent<T, K> implements OnInit {
    constructor(protected modelsService: ModelsService<T, K>, snackBar: MatSnackBar){
        super(snackBar);

        this.getModels = () => this.modelsService.getModels();
        this.createModel = (model:T) => modelsService.createModel(model);
        this.updateModel = (model:T) => modelsService.updateModel(model);
        this.removeModel = (key:K) => modelsService.deleteModel(key);
        this.isSaveModel = this.isValidModel.bind(this);
    }

    protected abstract isValidModel(): boolean;
}

// @Component({
//     selector: 'models-app',
//     templateUrl: "./models.component.html",
// })
// export abstract class EditModelsComponent<T extends DbModel, K extends string|number> extends EditModelComponent implements OnInit {
//     @ViewChild('readOnlyTemplate', {static: false})
//     readOnlyTemplate: TemplateRef<any>|null;

//     @ViewChild('editTemplate', {static: false})
//     editTemplate: TemplateRef<any>|null;

//     models: T[];
//     editedModel: T|null = null;
//     isNewRecord: boolean = false;

//     get isEditedModel(): boolean {
//         return this.editedModel != null;
//     }

//     constructor(protected modelsService: ModelsService<T, K>, snackBar: MatSnackBar){
//         super(snackBar);
//     }

//     ngOnInit(): void {
//        this.loadModels();
//     }

//     private loadModels(){
//         this.modelsService.getModels()
//             .pipe(this.catchError())
//             .subscribe((indexViewModel: IndexViewModel<T>) => {
//                 this.models = indexViewModel.models;
//             })
//     }

//     private createEmptyModel(): T{
//         const emptyModel = {} as T;
//         return emptyModel;
//     }

//     addModel(){
//         this.editedModel = this.createEmptyModel();
//         this.models.push(this.editedModel!);
//         this.isNewRecord = true;
//     }

//     editModel(model: T){
//         this.editedModel = {...model};
//     }

//     loadTemplate(model: T) {
//         if(this.editedModel && this.editedModel.id === model.id)
//             return this.editTemplate;
//         return this.readOnlyTemplate;
//     }

//     saveModel() {
//         (this.isNewRecord ? 
//             this.modelsService.createModel(this.editedModel as T) : 
//             this.modelsService.updateModel(this.editedModel as T))
//                 .pipe(this.catchError())
//                 .subscribe(_ => {
//                     this.loadModels();
//                     this.modelUpdatedSuccessfully()
//                 });
//         if(this.isNewRecord) {
//             this.isNewRecord = false;
//         }
//         this.editedModel = null;
//     }
    
//     cancel() {
//         if(this.isNewRecord){
//             this.models.pop();
//             this.isNewRecord = false;
//         }
//         this.editedModel = null;
//     }

//     deleteModel(model: T){
//         this.modelsService.deleteModel(model!.id as K)
//             .pipe(this.catchError())
//             .subscribe(_ => {
//                 this.loadModels();
//                 this.modelDeletedSuccessfully()
//             });
//     }
// }

