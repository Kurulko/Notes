import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ModelsService } from 'src/app/services/models/models.service';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';

@Component({
    selector: 'app-models',
    template: ""
})
export abstract class ModelsComponent<T extends DbModel, K extends string|number> implements OnInit {
    @ViewChild('readOnlyTemplate', {static: false})
    readOnlyTemplate: TemplateRef<any>|undefined;

    @ViewChild('editTemplate', {static: false})
    editTemplate: TemplateRef<any>|undefined;

    models: T[];
    editedModel: T|null = null;
    isNewRecord: boolean = false;

    constructor(protected modelsService: ModelsService<T, K>){
    }

    ngOnInit(): void {
       this.loadModels();
    }

    private loadModels(){
        this.modelsService.getModels().subscribe((models: T[]) => {
            this.models = models;
        })
    }

    abstract createEmptyModel(): T;

    addModel(){
        this.editedModel = this.createEmptyModel();
        this.models.push(this.editedModel!);
        this.isNewRecord = true;
    }

    // abstract editModel(model: T): void;
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
                .subscribe(this.loadModels);
            this.isNewRecord = false;
        }
        else {
            this.modelsService.updateModel(this.editedModel as T)
                .subscribe(this.loadModels);
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
            .subscribe(this.loadModels);
    }
}
