import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpResponse } from 'selenium-webdriver/http';

import { map } from 'rxjs/operators';

import { IUser } from "@app/profile/user.model";
import { ITweet } from "@app/tweet/tweet.model";

@Injectable()
export class ProfileService {
  constructor(
    @Inject('PROFILE_INFO_URL') private profileInfoApiURL: string,
    @Inject('PROFILE_TWEETS_URL') private profileTweetsApiURL: string,
    @Inject('PROFILE_SUBSCRIPTIONS_QLT_URL') private profileSubscriptionsQltApiURL: string,
    @Inject('PROFILE_SUBSRIBERS_QLT_URL') private profileSubscribersQltApiURL: string,
    @Inject('PROFILE_USER_POSTS_QLT_URL') private profilePostsQltApiURL: string,
    private http: HttpClient) { }

  getUserData(): Observable<IUser> {
    return  this.http.get<IUser> (this.profileInfoApiURL);
  }
  getUserTweets(): Observable<ITweet[]> {
    return this.http.get<ITweet[]>(this.profileTweetsApiURL);
  }
  getSubscriptionsQuality(): Observable<number> {
    return this.http.get<number>(this.profileSubscriptionsQltApiURL);
  }
  getSubscribersQuality(): Observable<number> {
    return this.http.get<number>(this.profileSubscribersQltApiURL);
  }
  getUserPostsQuality(): Observable<number> {
    return this.http.get<number>(this.profilePostsQltApiURL);
  }
  
}
