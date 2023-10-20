import { BaseService } from "../base.service";
import { Injectable } from '@angular/core';
import { TokenModel } from '../../models/auth/token-model';
import { HttpClient } from "@angular/common/http";
import { Helpers } from "src/app/helpers/helpers";
import { Observable, catchError } from "rxjs";
import { RegisterModel } from "src/app/models/auth/register-model";
import { LoginModel } from "src/app/models/auth/login-model";
import { AuthModel } from 'src/app/models/auth/auth-model';
import { TokenViewModel } from "src/app/models/auth/token-viewmodel";

@Injectable()
export class AuthService extends BaseService{
    constructor(httpClient: HttpClient, helper: Helpers) {
        super(httpClient, helper, 'account');
    }
    
    private account(path:string, authModel: AuthModel): Observable<TokenViewModel> {
        return this.returnModel(this.webClient.post<TokenViewModel>(path, authModel));
    }

    login(loginModel: LoginModel): Observable<TokenViewModel> {
        return this.account('login', loginModel);
    }

    register(registerModel: RegisterModel): Observable<TokenViewModel> {
        return this.account('register', registerModel);
    }

    token(): Observable<TokenViewModel> {
        return this.returnModel(this.webClient.get<TokenViewModel>('token'));
    }

    logout(): Observable<Object> {
        return this.returnModel(this.webClient.post('logout'));
    }
}