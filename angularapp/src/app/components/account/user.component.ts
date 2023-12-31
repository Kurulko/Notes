import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/database/admin/user';
import { UserService } from 'src/app/services/models/admin/user.service';
import { EditModelComponent } from '../helpers/edit-model.component';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { Helpers } from 'src/app/helpers/helpers';
import { TokenViewModel } from 'src/app/models/auth/token-viewmodel';

@Component({
    selector: 'user-app',
    templateUrl: './user.component.html',
    providers: [UserService], 
})
export class UserComponent extends EditModelComponent implements OnInit{
    user: User|null = null;

    constructor(private helpers: Helpers, private userService: UserService, snackBar: MatSnackBar){
        super(snackBar);
    }
    
    get isLoading(): boolean {
        return this.user === null || this.user === undefined;
    }

    ngOnInit(): void {
        this.userService.getCurretUser()
            .pipe(this.catchError())
            .subscribe((user: User) => {
                this.user = user;
            });
    }

    updateUser() {
        this.userService.updateModel(this.user!)
            .pipe(this.catchError())
            .subscribe((token: any) => {
                this.modelUpdatedSuccessfully('User')
                this.helpers.setToken(token as TokenViewModel);
            });
    }
}
