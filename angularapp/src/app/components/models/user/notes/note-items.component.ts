import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NoteItem } from 'src/app/models/database/notes/note-item';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { NoteModelsComponent } from './note-models.component';

@Component({
    selector: 'note-items-app',
    templateUrl: './note-items.component.html',
    providers: [ NoteItemService ]
})
export class NoteItemsComponent extends NoteModelsComponent<NoteItem> {
    constructor(noteItemService: NoteItemService){
        super(noteItemService);
    }

    override createEmptyModel(): NoteItem {
        return new NoteItem();
    }
}