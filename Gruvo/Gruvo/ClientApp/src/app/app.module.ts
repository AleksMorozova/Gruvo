import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Inject } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from '@app/app.component';
import { AppRoutingModule } from '@app/app-routing.module';
import { LoginComponent } from '@app/login/login.component';
import { LoginService } from '@app/login/login.service';
import { SignupComponent } from '@app/signup/signup.component';
import { HeaderComponent } from '@app/header/header.component';
import { FooterComponent } from '@app/footer/footer.component';
import { TweetComponent } from '@app/tweet/tweet.component';
import { ProfileComponent } from '@app/profile/profile.component';
import { ProfileService } from '@app/profile/profile.service';
import { MenuComponent } from '@app/menu/menu.component';
import { LoginGuard } from '@app/login-guard';
import { FeedComponent } from '@app/feed/feed.component';
import { FeedService } from '@app/feed/feed.service';
import { RecommendationComponent } from '@app/recommendation/recommendation.component';


@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    SignupComponent,
    ProfileComponent,
    MenuComponent,
    TweetComponent,
    FeedComponent,
    RecommendationComponent
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
    { provide: 'FEED_TWEETS_URL', useValue: 'api/feed/tweets' },
    { provide: 'FEED_RECOMMENDATIONS_URL', useValue: 'api/feed/recommendations'},
     LoginService,
     ProfileService,
     FeedService,
     LoginGuard
      ]
})
export class AppModule { }
