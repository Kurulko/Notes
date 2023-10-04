import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { TokenModel } from '../models/auth/token-model';
import { TokenViewModel } from '../models/auth/token-viewmodel';
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class Helpers {
    private authenticationChanged = new Subject<boolean>();
    private readonly tokenStorageName: string = 'token';

    public isAuthenticated() : boolean {
        const localStorageToken = localStorage[this.tokenStorageName];
        const isExistedToken =  localStorageToken !== undefined && localStorageToken !== null && localStorageToken !== "undefined" && localStorageToken !== "null" && localStorageToken !== ''; 

        if(isExistedToken){
            const tokenModel = this.getTokenFromStorage()!;
            const today = new Date();
            const expirationDate = new Date(tokenModel.expirationDate)
            const isActive =  expirationDate > today;

            if(!isActive){
                this.clearToken();
                return false;
            }

            return true;
        }
        
        return false; 
    }

    public isAuthenticationChanged(): Observable<boolean> {
        return this.authenticationChanged.asObservable();
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

    public setToken(data: TokenViewModel): void {
        const today = new Date();

        const tokenModel:TokenModel = new TokenModel();
        tokenModel.token = data.token;

        today.setDate(today.getDate() + data.expirationDays);
        tokenModel.expirationDate = today;

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

    private setStorageToken(value: string|undefined): void{
        localStorage[this.tokenStorageName] = value;
        this.authenticationChanged.next(this.isAuthenticated());
    }
}
