import { NgModule } from '@angular/core';
import { Routes, RouterModule, CanActivate } from '@angular/router';

import { LoginComponent } from './login/login.component'
import { SignupComponent } from './signup/signup.component'
import { ProfileComponent } from './profile/profile.component';
import { LoginGuard } from './login-guard';
import { FeedComponent } from './feed/feed.component';


const appRoutes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full', canActivate: [LoginGuard] },
  { path: 'login', component: LoginComponent, pathMatch: 'full', canActivate: [LoginGuard]},
  { path: 'signup', component: SignupComponent, pathMatch: 'full', canActivate: [LoginGuard] },
  { path: 'profile', component: ProfileComponent, pathMatch: 'full' },
  { path: 'feed', component: FeedComponent, pathMatch: 'full'},
  { path: '**', redirectTo: '/page404' },

  /* TODO:
   
  { path: 'dashboard', canActivate: [AuthGuard], component: MainComponent, },
  { path: 'login', .... *children: [
      { path: 'edit', canActivate: [EditGuard], component: EditProfileComponent },
      { path: 'followers',  component: FollowersComponent },
      { path: 'subscribtions',  component: SubscribtionsComponent },
      { path: 'status/:id',  component: ViewPostComponent }    //to show a one specific post
    ]
  },
  { path: 'status/add', canActivate: [AuthGuard], component: AddPostComponent, },
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
