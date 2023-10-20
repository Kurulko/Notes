import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { BaseService } from '../base.service';
import { Helpers } from '../../helpers/helpers';
import { DbModel } from 'src/app/models/database/db-model';
import { IndexViewModel } from 'src/app/models/helpers/index-view-model';

export abstract class ModelsService<T extends DbModel, K extends string|number> extends BaseService {
    protected readonly emptyPath:string = '';
    constructor(httpClient: HttpClient, helper: Helpers, controllerName: string) {
        super(httpClient, helper, controllerName);
    }

    getModelById(id: K): Observable<T> {
        return this.returnModel(this.webClient.get<T>(id.toString()));
    }

    getModels(attribute?:string, orderBy?:string, pageNumber?:number, pageSize?:number): Observable<IndexViewModel<T>>{
        return this.returnModel(this.webClient.get<IndexViewModel<T>>(this.sortAndPadingUrlStr(this.emptyPath, attribute, orderBy, pageNumber, pageSize)));
    }

    updateModel(model:T): Observable<Object> {
        return this.returnModel(this.webClient.put(this.emptyPath, model));
    }

    createModel(model:T): Observable<T>{
        return this.returnModel(this.webClient.post<T>(this.emptyPath,model));
    }

    deleteModel(id: K): Observable<Object> {
        return this.returnModel(this.webClient.delete(id.toString()));
    }

    protected sortAndPadingUrlStr(path:string, attribute?:string, orderBy?:string, pageNumber?:number, pageSize?:number) : string{
        const attributeStr = this.getModelStr(attribute, 'attribute');
        const orderByStr = this.getModelStr(orderBy, 'orderBy');
        const pageNumberStr = this.getModelStr(pageNumber, 'pageNumber');
        const pageSizeStr = this.getModelStr(pageSize, 'pageSize');

        return `${path}?${attributeStr}${orderByStr}${pageNumberStr}${pageSizeStr}`;
    }

    private getModelStr(model:string|number|undefined, nameStr: string):string{
         return model ? `${nameStr}=${model}&` : '';
    }
}