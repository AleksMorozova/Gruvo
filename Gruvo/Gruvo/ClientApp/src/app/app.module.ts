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
import { TweetService } from '@app/tweet/tweet.service';
import { ProfileComponent } from '@app/profile/profile.component';
import { ProfileService } from '@app/profile/profile.service';
import { MenuComponent } from '@app/menu/menu.component';
import { MenuItemsComponent } from '@app/menu-items/menu-items.component';
import { LoginGuard } from '@app/login.guard';
import { AuthGuard } from '@app/auth.guard';
import { FeedComponent } from '@app/feed/feed.component';
import { FeedService } from '@app/feed/feed.service';
import { RecommendationComponent } from '@app/recommendation/recommendation.component';
import { CreateTweetComponent } from '@app/create-tweet/create-tweet.component';
import { CreateTweetService } from '@app/create-tweet/create-tweet.service';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    LoginComponent,
    SignupComponent,
    ProfileComponent,
    MenuComponent,
    MenuItemsComponent,
    TweetComponent,
    FeedComponent,
    RecommendationComponent,
    CreateTweetComponent
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
    { provide: 'PROFILE_SUBSCRIPTIONS_COUNT_URL', useValue: 'api/profile/subscriptionsCount' },
    { provide: 'PROFILE_SUBSRIBERS_COUNT_URL', useValue: 'api/profile/subscribersCount' },
    { provide: 'PROFILE_USER_POSTS_COUNT_URL', useValue: 'api/profile/userPostsCount' },
    { provide: 'CREATETWEET_POST_TWEET_URL', useValue: 'api/profile/postTweet' },
    { provide: 'TWEET_DELETE_TWEET_URL', useValue: 'api/profile/deleteTweet'},
    { provide: 'FEED_TWEETS_URL', useValue: 'api/feed/tweets' },
    { provide: 'FEED_RECOMMENDATIONS_URL', useValue: 'api/feed/recommendations'},
     LoginService,
     ProfileService,
     FeedService,
     CreateTweetService,
     TweetService,     
     LoginGuard,
     AuthGuard
      ]
})
export class AppModule { }
