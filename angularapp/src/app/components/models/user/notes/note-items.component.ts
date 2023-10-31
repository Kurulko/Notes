import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NoteItem } from 'src/app/models/database/notes/note-item';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { NoteModelsComponent } from './note-models.component';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'note-items-app',
    templateUrl: './note-items.component.html',
    providers: [ NoteItemService ]
})
export class NoteItemsComponent extends NoteModelsComponent<NoteItem> {
    constructor(router: Router, noteItemService: NoteItemService,route: ActivatedRoute, snackBar: MatSnackBar){
        super(router, noteItemService, route, snackBar);
    }

    @ViewChild('title') 
    titleModel: NgModel;

    @ViewChild('description') 
    descriptionModel: NgModel;

    protected override isValidModel(): boolean {
        return !(this.titleModel?.invalid || this.descriptionModel?.invalid);
    }
}
