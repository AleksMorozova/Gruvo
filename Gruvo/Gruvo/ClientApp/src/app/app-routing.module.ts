import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import components and servises here

import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';

const appRoutes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full' },
 TODO:// { path: 'dashboard', canActivate: [/*AuthGuard*/], /*component: MainComponent,*/ },
    { path: 'login', component: LoginComponent, pathMatch: 'full' },
    TODO: /*children: [
      { path: 'edit', canActivate: [EditGuard], component: EditProfileComponent },
{ path: 'followers',  component: FollowersComponent },
{ path: 'subscribtions',  component: SubscribtionsComponent },
{ path: 'status/:id',  component: ViewPostComponent }    //to show a one specific post
    ]
  },*/
    { path: 'signup', component: SignupComponent, pathMatch: 'full' },
    TODO://{ path: 'status/add', canActivate: [/*AuthGuard*/], /*component: AddPostComponent,*/ },
    TODO:// { path: 'page404', /*component: Page404Component*/ },
  { path: '**', redirectTo: '/page404' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
