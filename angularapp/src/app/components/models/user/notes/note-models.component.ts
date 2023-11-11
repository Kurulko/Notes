import { Component, OnInit, TemplateRef, ViewChild, Input } from '@angular/core';
import { NoteItem } from 'src/app/models/database/notes/note-item';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { ModelsComponent } from '../../models.component';
import { NoteModelService } from 'src/app/services/models/notes/note-model.service';
import { NoteModel } from 'src/app/models/database/notes/note-model';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { EditModelsComponent } from '../../edit-models.component';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/models/admin/user.service';
import { Observable } from 'rxjs'
import { IndexViewModel } from 'src/app/models/helpers/index-view-model';

export abstract class NoteModelsComponent<T extends NoteModel> extends EditModelsComponent<T, number> {  
    constructor(router: Router, protected userService: UserService, noteModelsService: NoteModelService<T>, route: ActivatedRoute, snackBar: MatSnackBar){
        super(router, noteModelsService, route, snackBar);

        this.getModels = this.getUserModels;
    }

    protected abstract getUserModels(attribute?:string, orderBy?:string, pageNumber?:number, pageSize?:number): Observable<IndexViewModel<T>>;
}
