import { AfterViewInit, Component, OnInit  } from '@angular/core';
import { Subscription } from 'rxjs';
import { Helpers } from '../helpers/helpers';
import { startWith, delay } from 'rxjs/operators';
import { AuthService } from '../services/auth/auth.service';
import { TokenModel } from '../models/auth/token-model';
import { TokenViewModel } from '../models/auth/token-viewmodel';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'], 
  providers: [AuthService]
})
export class AppComponent implements AfterViewInit, OnInit{
  readonly title = 'Notes!';

  subscription: Subscription;
  isAuthenticated: boolean;
  isAdmin: boolean = true;

  
  constructor(private helpers: Helpers, private authService: AuthService) {}

  ngOnInit(): void {
      this.authService.token().subscribe((tokenViewModel: TokenViewModel) => {
        this.helpers.setToken(tokenViewModel);
    })
  }

  ngAfterViewInit(): void {
    this.subscription = this.helpers.isAuthenticationChanged().pipe(
      startWith(this.helpers.isAuthenticated()),
      delay(0)
    ).subscribe((value: boolean) => this.isAuthenticated = value);
  }

  ngOnDestroy(){
    this.subscription.unsubscribe();
  }
}

