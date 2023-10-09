import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NoteItem } from 'src/app/models/database/notes/note-item';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { ModelsComponent } from '../models.component';
import { NoteModelService } from 'src/app/services/models/notes/note-model.service';
import { AdminModelService } from 'src/app/services/models/admin/admin-model.service';
import { AdminModel } from 'src/app/models/database/admin/admin-model';

@Component({
    selector: 'admin-models-app',
    templateUrl: './admin-models.component.html',
})
export abstract class AdminModelsComponent<T extends AdminModel> extends ModelsComponent<T, string> {
    constructor(adminModelService: AdminModelService<T>){
        super(adminModelService);
    }
}
