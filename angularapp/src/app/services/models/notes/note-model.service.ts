import { HttpClient } from '@angular/common/http';
import { Helpers } from 'src/app/helpers/helpers';
import { ModelsService } from '../models.service';
import { NoteModel } from 'src/app/models/database/notes/note-model';

export abstract class NoteModelService<T extends NoteModel> extends ModelsService<T, number> {
    constructor(httpClient: HttpClient, helper: Helpers, controllerName: string) {
        super(httpClient, helper, controllerName);
    }
}
    