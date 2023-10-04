import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from '../base.service';
import { Helpers } from '../../helpers/helpers';

export abstract class ModelsService<T extends DbModel, K extends string|number> extends BaseService {
    constructor(httpClient: HttpClient, helper: Helpers, controllerName: string) {
        super(httpClient, helper, controllerName);
    }

    getModelById(id: K): Observable<T> {
        return super.webClient.get<T>(id.toString())
    }

    getModels(): Observable<T[]>{
        return super.webClient.get<T[]>('')
    }

    updateModel(model:T): Observable<Object> {
        return super.webClient.put('', model)
    }

    createModel(model:T): Observable<T>{
        return super.webClient.post<T>('',model)
    }

    deleteModel(id: K): Observable<Object> {
        return super.webClient.delete(id.toString());
    }
}