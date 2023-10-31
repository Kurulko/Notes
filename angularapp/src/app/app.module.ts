import { HttpClientModule } from '@angular/common/http';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
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
import { BaseComponent } from './components/helpers/base.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { AdminGuard } from './helpers/canActiveteAdminGuard';
import { EditModelComponent } from './components/helpers/edit-model.component';
import { ShowErrorsComponent } from './components/helpers/show-errors.component';
import { ModelsComponent } from './components/models/models.component';
import { LoaderComponent } from './components/helpers/loader.component';

const layouts:any[] = [
  AppComponent,
  LeftPanelLayoutComponent,
];

const components:any[] = [
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
];

const helpers:any[] = [
  BaseComponent,
  EditModelComponent,
  ShowErrorsComponent, 
  ModelsComponent,
  LoaderComponent,
];

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
    declarations: 
      layouts
        .concat(components)
        .concat(helpers),
    bootstrap: [ AppComponent ],
    providers: [ AuthGuard, AdminGuard ],
    schemas: [NO_ERRORS_SCHEMA]
})
export class AppModule { }
