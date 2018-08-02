import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login.component'
import { SignupComponent } from './signup/signup.component'
import { ProfileComponent } from './profile/profile.component';

const appRoutes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent, pathMatch: 'full' },
  { path: 'signup', component: SignupComponent, pathMatch: 'full' },
  { path: 'profile', component: ProfileComponent, pathMatch: 'full' },
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
