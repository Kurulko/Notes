import { AfterViewInit, Component, OnInit  } from '@angular/core';
import { Subscription } from 'rxjs';
import { Helpers } from '../helpers/helpers';
import { startWith, delay } from 'rxjs/operators';
import { AuthService } from '../services/auth/auth.service';
import { TokenModel } from '../models/auth/token-model';
import { TokenViewModel } from '../models/auth/token-viewmodel';
import { filter, switchMap, tap, take } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'], 
  providers: [AuthService]
})
export class AppComponent implements AfterViewInit, OnInit{
  readonly title = 'Notes!';

  authenticatedScription: Subscription;
  adminScription: Subscription;
  isAuthenticated: boolean = false;
  isAdmin: boolean = false;
  
  constructor(private helpers: Helpers, private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.token().subscribe((tokenViewModel: TokenViewModel|null) => {
        if(tokenViewModel !== null) {
          this.helpers.setToken(tokenViewModel);
        }
    })
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

