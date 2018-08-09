import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './login/login.component';
import { LoginService } from './login/login.service';
import { SignupComponent } from './signup/signup.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { TweetComponent } from './tweet/tweet.component';
import { ProfileComponent } from './profile/profile.component';
import { ProfileService } from './profile/profile.service';
import { MenuComponent } from './menu/menu.component';
import { LoginGuard } from './login-guard';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    SignupComponent,
    ProfileComponent,
    MenuComponent,
    TweetComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule
  ],
  bootstrap: [AppComponent],
  providers: [
    { provide: 'LOGIN_URL', useValue: 'api/auth/login' },
    { provide: 'SIGNUP_URL', useValue: 'api/auth/signup' },
    { provide: 'TEST_URL', useValue: 'api/auth/test' }, 
     LoginService,
     ProfileService,    
    LoginGuard
      ]
})
export class AppModule { }
