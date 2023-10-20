import { Component } from '@angular/core';
import { ChangePassword } from 'src/app/models/helpers/change-password';
import { UserService } from 'src/app/services/models/admin/user.service';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { EditModelComponent } from '../edit-model.component';
import { MatSnackBar  } from '@angular/material/snack-bar';

@Component({
    selector: 'user-password-app',
    templateUrl: './user-password.component.html',
    providers: [UserService]
})
export class UserPasswordComponent extends EditModelComponent {
    password: ChangePassword = new ChangePassword();

    constructor(private userService: UserService, snackBar: MatSnackBar){
        super(snackBar);
    }

    changePassword() {
        this.userService.changePassword(this.password)
            .pipe(this.catchError())
            .subscribe(_ => this.modelUpdatedSuccessfully());
    }
}
