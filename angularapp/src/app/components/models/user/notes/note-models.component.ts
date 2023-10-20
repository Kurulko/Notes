import { Component, OnInit, TemplateRef, ViewChild, Input } from '@angular/core';
import { NoteItem } from 'src/app/models/database/notes/note-item';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { ModelsComponent } from '../../models.component';
import { NoteModelService } from 'src/app/services/models/notes/note-model.service';
import { NoteModel } from 'src/app/models/database/notes/note-model';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { EditModelsComponent } from '../../edit-models.component';

export abstract class NoteModelsComponent<T extends NoteModel> extends EditModelsComponent<T, number> {  
    constructor(noteModelsService: NoteModelService<T>, snackBar: MatSnackBar){
        super(noteModelsService, snackBar);
    }
}
