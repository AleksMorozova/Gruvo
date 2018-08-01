import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import components and servises here

import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';

const appRoutes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent, pathMatch: 'full' },
  { path: 'signup', component: SignupComponent, pathMatch: 'full' },
  { path: '**', redirectTo: '/page404' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
