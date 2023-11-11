import { AfterViewInit, Component, OnInit  } from '@angular/core';
import { Subscription } from 'rxjs';
import { Helpers } from '../helpers/helpers';
import { startWith, delay } from 'rxjs/operators';
import { AuthService } from '../services/auth/auth.service';
import { TokenModel } from '../models/auth/token-model';
import { TokenViewModel } from '../models/auth/token-viewmodel';
import { filter, switchMap, tap, take } from 'rxjs/operators';
import { UserService } from '../services/models/admin/user.service';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { EditModelComponent } from '../components/helpers/edit-model.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'], 
  providers: [AuthService, UserService]
})
export class AppComponent extends EditModelComponent implements AfterViewInit, OnInit {

  authenticatedScription: Subscription;
  adminScription: Subscription;
  isAuthenticated: boolean = false;
  isAdmin: boolean = false;
  isImpersonating: boolean = false;

  constructor(private helpers: Helpers, private authService: AuthService, private userService: UserService, private router: Router, snackBar: MatSnackBar) {
    super(snackBar);

    this.title = 'Notes!'
  }

  ngOnInit(): void {
    this.authService.token().subscribe((tokenViewModel: TokenViewModel|null) => {
        if(tokenViewModel !== null) {
          this.helpers.setToken(tokenViewModel);
        }
    });

    this.userService.isImpersonating().subscribe((isImpersonating: boolean) => 
        this.isImpersonating = isImpersonating
    );
  }
  
  finishImpersonating(): void {
    this.userService.dropUsedUserId()
    .pipe(this.catchError())
    .subscribe(_ => {
        this.isImpersonating = false;
        this.showSnackbar("Impersonation mode is disabled");
        this.router.navigateByUrl("/");
    });
  }

  ngAfterViewInit(): void {
    this.authenticatedScription = this.helpers.isAuthenticationChanged().pipe(
      startWith(this.helpers.isAuthenticated()),
      delay(0)
    ).subscribe((value: boolean) => this.isAuthenticated = value);

    this.adminScription = this.helpers.isAdminChanged().pipe(
      startWith(this.helpers.isAdmin()),
      delay(0)
    ).subscribe((value: boolean) => this.isAdmin = value);
  }

  ngOnDestroy(){
    this.authenticatedScription.unsubscribe();
    this.adminScription.unsubscribe();
  }
}

