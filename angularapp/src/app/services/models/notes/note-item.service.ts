import { HttpClient } from '@angular/common/http';
import { Helpers } from 'src/app/helpers/helpers';
import { NoteModelService } from './note-model.service';
import { NoteItem } from 'src/app/models/database/notes/note-item';
import { Injectable } from '@angular/core';

@Injectable()
export class NoteItemService extends NoteModelService<NoteItem> {
    constructor(httpClient: HttpClient, helper: Helpers) {
        super(httpClient, helper, 'noteItems');
    }
}
    