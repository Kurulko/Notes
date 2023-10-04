import { Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { Helpers } from '../../helpers/helpers';
import { TokenViewModel } from 'src/app/models/auth/token-viewmodel';
import { Observable } from "rxjs";

export abstract class AuthComponent{
    constructor(protected helpers: Helpers, protected router: Router, protected authService: AuthService){}

    abstract getTokenViewModel() : Observable<TokenViewModel>;

    protected account(): void{
        this.getTokenViewModel().subscribe((token: TokenViewModel) => {
            this.helpers.setToken(token);
            this.router.navigate(['/home'])
        })
    }
}
