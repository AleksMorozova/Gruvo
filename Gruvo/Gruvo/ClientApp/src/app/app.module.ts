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
import { LoginGuard } from '@app/login.guard';
import { AuthGuard } from '@app/auth.guard';
import { FeedComponent } from '@app/feed/feed.component';
import { FeedService } from '@app/feed/feed.service';
import { RecommendationComponent } from '@app/recommendation/recommendation.component';
import { SettingsComponent } from '@app/settings/settings.component';
import { SettingsService } from '@app/settings/settings.service';
import { PasswordEditComponent } from '@app/password-edit/password-edit.component';


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
    RecommendationComponent,
    SettingsComponent,
    PasswordEditComponent
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
    { provide: 'PROFILE_INFO_URL', useValue: 'api/profile/userInfo' },
    { provide: 'PROFILE_TWEETS_URL', useValue: 'api/profile/userTweets'},
    { provide: 'PROFILE_SUBSCRIPTIONS_URL', useValue: 'api/profile/subscriptions' },
    { provide: 'PROFILE_SUBSRIBERS_URL', useValue: 'api/profile/subscribers' },
    { provide: 'FEED_TWEETS_URL', useValue: 'api/feed/tweets' },
    { provide: 'FEED_RECOMMENDATIONS_URL', useValue: 'api/feed/recommendations' },
    { provide: 'PROFILE_EDIT_GET_INFO_URL', useValue: 'api/settings/userEditGetInfo' },
    { provide: 'PROFILE_EDIT_INFO_URL', useValue: 'api/settings/editInfo' },
    { provide: 'PROFILE_EDIT_PASSWORD_URL', useValue: 'api/settings/editPassword' },
     LoginService,
     ProfileService,
     FeedService,
     LoginGuard,
     SettingsService,
     AuthGuard
      ]
})
export class AppModule { }
