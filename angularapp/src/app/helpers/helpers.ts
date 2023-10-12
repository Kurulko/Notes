import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { TokenModel } from '../models/auth/token-model';
import { TokenViewModel } from '../models/auth/token-viewmodel';
import { Observable } from "rxjs";
import { toTokenModel } from './toTokenModel';

@Injectable({
  providedIn: 'root'
})
export class Helpers {
    private authenticationChanged = new Subject<boolean>();
    private adminChanged = new Subject<boolean>();
    private readonly tokenStorageName: string = 'token';

    public isAuthenticated() : boolean {        
        return this.getActiveToken() ? true : false; 
    }

    public isAdmin() : boolean {
        if (this.isAuthenticated()) {
            const tokenModel:TokenModel = this.getActiveToken()!;
            return tokenModel.roles.indexOf('Admin') !== -1;
        }

        return false; 
    }

    private getActiveToken(): TokenModel|undefined{
        const localStorageToken = localStorage[this.tokenStorageName];
        const isExistedToken =  localStorageToken !== undefined && localStorageToken !== null && localStorageToken !== "undefined" && localStorageToken !== "null" && localStorageToken !== ''; 

        if(isExistedToken){
            const tokenModel = this.getTokenFromStorage()!;
            const today = new Date();
            const expirationDate = new Date(tokenModel.expirationDate)
            const isActive =  expirationDate > today;

            if(!isActive){
                this.clearToken();
                return undefined;
            }

            return tokenModel;
        }
        
        return undefined;
    }
    
    public isAuthenticationChanged(): Observable<boolean> {
        return this.authenticationChanged.asObservable();
    }

    public isAdminChanged(): Observable<boolean> {
        return this.adminChanged.asObservable();
    }
    
    public getToken(): string|undefined {
        if(this.isAuthenticated())
            return undefined;
        const tokenModel = this.getTokenFromStorage()!;
        return tokenModel.token;
    }

    private getTokenFromStorage(): TokenModel|undefined {
        return JSON.parse(localStorage[this.tokenStorageName]) as TokenModel|undefined;
    }

    public setToken(tokenViewModel: TokenViewModel): void {
        const tokenModel:TokenModel = toTokenModel(tokenViewModel);
        this.setStorageToken(JSON.stringify(tokenModel));
    }

    public failToken(): void {
        this.clearToken();
    }

    public logout(): void {
        this.clearToken();
    }

    private clearToken(): void{
        this.setStorageToken(undefined);
    }

    private setStorageToken(value: string|undefined): void {
        localStorage[this.tokenStorageName] = value;
        this.authenticationChanged.next(this.isAuthenticated());
        this.adminChanged.next(this.isAdmin());
    }
}
