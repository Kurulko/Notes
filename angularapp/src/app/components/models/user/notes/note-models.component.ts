import { Component, OnInit, TemplateRef, ViewChild, Input } from '@angular/core';
import { NoteItem } from 'src/app/models/database/notes/note-item';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { ModelsComponent } from '../../models.component';
import { NoteModelService } from 'src/app/services/models/notes/note-model.service';
import { NoteModel } from 'src/app/models/database/notes/note-model';

@Component({
    selector: 'note-models-app',
    templateUrl: './note-models.component.html',
})
export abstract class NoteModelsComponent<T extends NoteModel> extends ModelsComponent<T, number> {
    @Input() 
    rowCellTemplate: any;
    
    constructor(noteModelsService: NoteModelService<T>){
        super(noteModelsService);
    }
}
