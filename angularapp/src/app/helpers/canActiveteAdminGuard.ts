import { Router, UrlTree } from '@angular/router';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Helpers } from './helpers';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { BaseGuard } from './canActivateGuard';

@Injectable()
export class AdminGuard extends BaseGuard{
    constructor(router: Router, helper: Helpers){
        super(router, helper);
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        if(!this.helper.isAdmin()){
            this.router.navigate(['/login']);
            return false;
        }
        return true;
    }
}