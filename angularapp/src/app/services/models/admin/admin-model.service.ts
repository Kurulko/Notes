import { HttpClient } from '@angular/common/http';
import { Helpers } from 'src/app/helpers/helpers';
import { ModelsService } from '../models.service';
import { AdminModel } from 'src/app/models/database/admin/admin-model';

export abstract class AdminModelService<T extends AdminModel> extends ModelsService<T, string> {
    constructor(httpClient: HttpClient, helper: Helpers, controllerName: string) {
        super(httpClient, helper, controllerName);
    }
}
    