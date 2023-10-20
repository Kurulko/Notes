import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { Helpers } from '../../helpers/helpers';
import { TokenViewModel } from 'src/app/models/auth/token-viewmodel';
import { Observable, throwError  } from "rxjs";
import { catchError } from 'rxjs/operators';
import { EditModelComponent } from '../edit-model.component';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { of } from 'rxjs';

export abstract class AuthComponent extends EditModelComponent {
    constructor(protected helpers: Helpers, protected router: Router, protected authService: AuthService, snackBar: MatSnackBar){
        super(snackBar);
    }

    abstract getTokenViewModel() : Observable<TokenViewModel>;

    protected account(): void{
        this.getTokenViewModel()
        .pipe(this.catchError())
        .subscribe((token: TokenViewModel) => {
            this.helpers.setToken(token);
            this.router.navigate(['/home'])
        })
    }
}
