import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { DbModel } from 'src/app/models/database/db-model';
import { IndexViewModel } from 'src/app/models/helpers/index-view-model';
import { ModelsService } from 'src/app/services/models/models.service';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { EditModelComponent } from '../helpers/edit-model.component';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { catchError } from 'rxjs/operators';
import { OperatorFunction, of } from 'rxjs';
import { ModelsComponent } from './models.component';
import { ActivatedRoute, Router } from '@angular/router';


export abstract class EditModelsComponent<T extends DbModel, K extends string|number> extends ModelsComponent<T, K> implements OnInit {
    constructor(router: Router, protected modelsService: ModelsService<T, K>, route: ActivatedRoute, snackBar: MatSnackBar){
        super(router, route, snackBar);

        this.getModels = (attribute?:string, orderBy?:string, pageNumber?:number, pageSize?:number) => this.modelsService.getModels(attribute, orderBy, pageNumber, pageSize);
        this.createModel = (model:T) => modelsService.createModel(model);
        this.updateModel = (model:T) => modelsService.updateModel(model);
        this.removeModel = (key:K) => modelsService.deleteModel(key);
        this.isSaveModel = this.isValidModel.bind(this);
    }

    protected abstract isValidModel(): boolean;
}