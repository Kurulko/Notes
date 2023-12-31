import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { Helpers } from '../../helpers/helpers';
import { LoginModel } from 'src/app/models/auth/login-model';
import { TokenViewModel } from 'src/app/models/auth/token-viewmodel';
import { AuthComponent } from './auth.component';
import { Observable } from "rxjs";
import { MatSnackBar  } from '@angular/material/snack-bar';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    providers: [AuthService]
})
export class LoginComponent extends AuthComponent{
    loginModel: LoginModel = new LoginModel();
    
    constructor(helpers: Helpers, router: Router,  authService: AuthService, snackBar: MatSnackBar){
        super(helpers, router, authService, snackBar);
    }

    getTokenViewModel() : Observable<TokenViewModel> {
        return this.authService.login(this.loginModel);
    }
}
