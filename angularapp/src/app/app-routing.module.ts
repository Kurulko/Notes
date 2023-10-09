import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './helpers/canActivateAuthGuard';
import { LoginComponent } from './components/auth/login.component';
import { LogoutComponent } from './components/auth/logout.component';
import { NotFoundComponent } from './components/not-found.component';
import { HomeComponent } from './components/home.component';
import { RegisterComponent } from './components/auth/register.component';
import { UserPasswordComponent } from './components/account/user-password.component';
import { UserComponent } from './components/account/user.component';
import { RolesComponent } from './components/models/admin/roles.component';
import { UsersComponent } from './components/models/admin/users.component';
import { CategoriesComponent } from './components/models/user/notes/categories.component';
import { NoteItemsComponent } from './components/models/user/notes/note-items.component';

const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'logout', component: LogoutComponent },
    { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },

    { path: 'user-password', component: UserPasswordComponent, canActivate: [AuthGuard] },
    { path: 'user', component: UserComponent, canActivate: [AuthGuard] },
    { path: 'roles', component: RolesComponent, canActivate: [AuthGuard] },//admin
    { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },//admin
    { path: 'categories', component: CategoriesComponent, canActivate: [AuthGuard] },
    { path: 'notes', component: NoteItemsComponent, canActivate: [AuthGuard] },

    { path: '**', component: NotFoundComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}