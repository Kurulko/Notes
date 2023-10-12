import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NoteItem } from 'src/app/models/database/notes/note-item';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { NoteModelsComponent } from './note-models.component';
import { MatSnackBar  } from '@angular/material/snack-bar';

@Component({
    selector: 'note-items-app',
    templateUrl: './note-items.component.html',
    providers: [ NoteItemService ]
})
export class NoteItemsComponent extends NoteModelsComponent<NoteItem> {
    constructor(noteItemService: NoteItemService, snackBar: MatSnackBar){
        super(noteItemService, snackBar);
    }

    override createEmptyModel(): NoteItem {
        return new NoteItem();
    }
}
