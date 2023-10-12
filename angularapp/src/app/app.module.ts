import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AppRoutingModule } from './app-routing.module'; 
import { LoginComponent } from './components/auth/login.component';
import { NotFoundComponent } from './components/not-found.component';
import { LogoutComponent } from './components/auth/logout.component';
import { AppComponent } from './layouts/app.component';
import { LeftPanelLayoutComponent } from './layouts/left-panel-layout.component';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from './components/auth/register.component';
import { HomeComponent } from './components/home.component';
import { AuthGuard } from './helpers/canActivateAuthGuard';
import { UsersComponent } from './components/models/admin/users.component';
import { RolesComponent } from './components/models/admin/roles.component';
import { CategoriesComponent } from './components/models/user/notes/categories.component';
import { NoteItemsComponent } from './components/models/user/notes/note-items.component';
import { NoteModelsComponent } from './components/models/user/notes/note-models.component';
import { UserComponent } from './components/account/user.component';
import { UserPasswordComponent } from './components/account/user-password.component';
import { BaseComponent } from './components/base.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { AdminGuard } from './helpers/canActiveteAdminGuard';

@NgModule({
    imports: [
      BrowserModule,
      BrowserAnimationsModule,
      MatButtonModule, 
      MatCheckboxModule,
      MatInputModule,
      MatFormFieldModule,
      MatSidenavModule,
      AppRoutingModule,
      HttpClientModule,
      MatSnackBarModule,
      FormsModule
    ],
   declarations: [
      AppComponent,
      LeftPanelLayoutComponent,
      HomeComponent,
      LoginComponent,
      RegisterComponent,
      LogoutComponent,
      NotFoundComponent,
      RolesComponent,
      UsersComponent,
      CategoriesComponent,
      NoteItemsComponent,
      UserComponent,
      UserPasswordComponent,
      BaseComponent
    ],
    bootstrap: [ AppComponent ],
    providers: [ AuthGuard, AdminGuard ]
})
export class AppModule { }
