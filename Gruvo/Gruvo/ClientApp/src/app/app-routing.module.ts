import { NgModule } from '@angular/core';
import { Routes, RouterModule, CanActivate } from '@angular/router';

import { LoginComponent } from './login/login.component'
import { SignupComponent } from './signup/signup.component'
import { ProfileComponent } from './profile/profile.component';
import { LoginGuard } from './login.guard';
import { AuthGuard } from './auth.guard';
import { FeedComponent } from './feed/feed.component';
import { SettingsComponent } from './settings/settings.component';


const appRoutes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full', canActivate: [LoginGuard] },
  { path: 'login', component: LoginComponent, pathMatch: 'full', canActivate: [LoginGuard]},
  { path: 'signup', component: SignupComponent, pathMatch: 'full', canActivate: [LoginGuard] },
  { path: 'profile', component: ProfileComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'profile/:id', component: ProfileComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'feed', component: FeedComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: 'settings', component: SettingsComponent, pathMatch: 'full', canActivate: [AuthGuard] },
  { path: '**', redirectTo: '/page404' },

  /* TODO:   
  { path: 'page404', component: Page404Component }
  */
];



@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
