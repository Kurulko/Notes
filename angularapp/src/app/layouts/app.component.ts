import { AfterViewInit, Component, ViewChild  } from '@angular/core';
import { Subscription } from 'rxjs';
import { Helpers } from '../helpers/helpers';
import { startWith, delay } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements AfterViewInit{
  subscription: Subscription;
  authentication: boolean;
  readonly title = 'Notes!';
  
  constructor(private helpers: Helpers) {}

  ngAfterViewInit(): void {
    this.subscription = this.helpers.isAuthenticationChanged().pipe(
      startWith(this.helpers.isAuthenticated()),
      delay(0)
    ).subscribe((value: boolean) => this.authentication = value);
  }

  ngOnDestroy(){
    this.subscription.unsubscribe();
  }
}

