import { Injectable } from '@angular/core';
import { throwError } from 'rxjs'
import { Helpers } from '../helpers/helpers';
import { WebClient } from '../helpers/webClient';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export abstract class BaseService {
    private readonly _pathBase:string = "api";
    protected readonly webClient: WebClient;

    constructor(httpClient: HttpClient, private readonly helper: Helpers, controllerName: string) {
        this.webClient = new WebClient(httpClient, `${this._pathBase}/${controllerName}`, this.header());
    }

    public extractData(res: Response){
        const body = res.json();
        return body || {};
    }

    public handleError(error: Response | any){
        let errMsg: string;
        if(error instanceof Response){
            const body = error.json() || '';
            const err = body || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.log(errMsg);
        return throwError(errMsg);
    }

    private header(){
        let header = new HttpHeaders({'Content-Type': 'application/json'});
        if(this.helper.isAuthenticated())
            header  = header.append('Authorization', `Bearer ${this.helper.getToken()}`);
        return {headers: header};
    }

    public setToken(data:any){
        this.helper.setToken(data);
    }

    public failToken(error: Response | any){
        this.helper.failToken();
        return this.handleError(error);
    }
}