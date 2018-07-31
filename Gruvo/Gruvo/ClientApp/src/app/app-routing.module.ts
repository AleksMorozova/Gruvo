import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
//import components and servises here

const appRoutes: Routes = [
  { path: '', /*component: LoginComponent,*/ pathMatch: 'full' },
  { path: 'dashboard', canActivate: [/*AuthGuard*/], /*component: MainComponent,*/ },
  {
    path: ':login', canActivate: [/*AuthGuard*/], /*component: UserProfileComponent,*/ children: [
      { path: 'edit', canActivate: [/*EditGuard*/], /*component: EditProfileComponent*/ },
      { path: 'followers',  /*component: FollowersComponent*/ },
      { path: 'subscribtions',  /*component: SubscribtionsComponent*/ },
      { path: 'status/:id',  /*component: ViewPostComponent*/ }    //to show a one specific post
    ]
  },
  { path: 'status/add', canActivate: [/*AuthGuard*/], /*component: AddPostComponent,*/ },
  { path: 'page404', /*component: Page404Component*/ },
  { path: '**', redirectTo: '/page404' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
