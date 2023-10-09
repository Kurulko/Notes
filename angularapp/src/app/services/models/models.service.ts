import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from '../base.service';
import { Helpers } from '../../helpers/helpers';
import { DbModel } from 'src/app/models/database/db-model';
import { IndexViewModel } from 'src/app/models/helpers/index-view-model';

export abstract class ModelsService<T extends DbModel, K extends string|number> extends BaseService {
    constructor(httpClient: HttpClient, helper: Helpers, controllerName: string) {
        super(httpClient, helper, controllerName);
    }

    getModelById(id: K): Observable<T> {
        return this.webClient.get<T>(id.toString())
    }

    getModels(): Observable<IndexViewModel<T>>{
        return this.webClient.get<IndexViewModel<T>>('')
    }

    updateModel(model:T): Observable<Object> {
        return this.webClient.put('', model)
    }

    createModel(model:T): Observable<T>{
        return this.webClient.post<T>('',model)
    }

    deleteModel(id: K): Observable<Object> {
        return this.webClient.delete(id.toString());
    }
}