import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NoteItem } from 'src/app/models/database/notes/note-item';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { ModelsComponent } from '../models.component';
import { NoteModelService } from 'src/app/services/models/notes/note-model.service';
import { AdminModelService } from 'src/app/services/models/admin/admin-model.service';
import { AdminModel } from 'src/app/models/database/admin/admin-model';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { EditModelsComponent } from '../edit-models.component';

export abstract class AdminModelsComponent<T extends AdminModel> extends EditModelsComponent<T, string> {
    constructor(adminModelService: AdminModelService<T>, snackBar: MatSnackBar){
        super(adminModelService, snackBar);
    }
}
