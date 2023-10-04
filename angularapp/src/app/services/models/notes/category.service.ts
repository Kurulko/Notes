import { HttpClient } from '@angular/common/http';
import { Helpers } from 'src/app/helpers/helpers';
import { NoteModelService } from './note-model.service';
import { Category } from 'src/app/models/database/notes/category';
import { Injectable } from '@angular/core';

@Injectable()
export class CategoryService extends NoteModelService<Category> {
    constructor(httpClient: HttpClient, helper: Helpers) {
        super(httpClient, helper, 'categories');
    }
}
    