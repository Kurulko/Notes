import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './helpers/canActivateAuthGuard';
import { LoginComponent } from './components/auth/login.component';
import { LogoutComponent } from './components/auth/logout.component';
import { NotFoundComponent } from './components/not-found.component';
import { HomeComponent } from './components/home.component';
import { RegisterComponent } from './components/auth/register.component';

const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'logout', component: LogoutComponent },
    { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
    { path: '**', component: NotFoundComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {
}