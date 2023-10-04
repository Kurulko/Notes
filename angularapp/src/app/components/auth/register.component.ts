import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { Helpers } from '../../helpers/helpers';
import { RegisterModel } from 'src/app/models/auth/register-model';
import { TokenViewModel } from 'src/app/models/auth/token-viewmodel';
import { AuthComponent } from './auth.component';
import { Observable } from "rxjs";

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    providers: [AuthService]
})
export class RegisterComponent extends AuthComponent {
    registerModel: RegisterModel = new RegisterModel();
    constructor(helpers: Helpers, router: Router, authService: AuthService){
        super(helpers, router, authService);
    }

    getTokenViewModel() : Observable<TokenViewModel> {
        return this.authService.register(this.registerModel);
    }
}
