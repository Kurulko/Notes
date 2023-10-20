import { Injectable } from '@angular/core';
import { Observable, throwError, catchError } from 'rxjs'
import { Helpers } from '../helpers/helpers';
import { WebClient } from '../helpers/webClient';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

@Injectable()
export abstract class BaseService {
    private readonly _pathBase:string = "api";
    protected readonly webClient: WebClient;

    constructor(httpClient: HttpClient, private readonly helper: Helpers, controllerName: string) {
        this.webClient = new WebClient(httpClient, `${this._pathBase}/${controllerName}`, this.header());
    }

    private handleError(error: HttpErrorResponse | any){
        let errorsMsg: string[] = [];
        if (error instanceof HttpErrorResponse) {
            if (error.error instanceof ErrorEvent) 
                errorsMsg = [`Error: ${error.error.message}`];
             else 
                errorsMsg = [`${error.status} - ${error.statusText || ''} ${error.message || ''}`];
        } 
        else {
            error.error.errors.Exception.forEach((e:string) => errorsMsg.push(...e.split(';')));
        }
        console.error(`ERROR:\n`, errorsMsg.join(';\n'));
        return throwError(() => errorsMsg);
    }

    private header(){
        let header = new HttpHeaders({'Content-Type': 'application/json'});
        if(this.helper.isAuthenticated())
            header  = header.append('Authorization', `Bearer ${this.helper.getToken()}`);
        return {headers: header};
    }

    protected returnModel<T>(model: Observable<T>): Observable<T>{
        return model.pipe(catchError(this.handleError));
    }
}