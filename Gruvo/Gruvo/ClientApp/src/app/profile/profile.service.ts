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
    @Inject('PROFILE_SUBSCRIPTIONS_COUNT_URL') private profileSubscriptionsCountApiURL: string,
    @Inject('PROFILE_SUBSRIBERS_COUNT_URL') private profileSubscribersCountApiURL: string,
    @Inject('PROFILE_USER_POSTS_COUNT_URL') private profilePostsCountApiURL: string,
    private http: HttpClient) { }

  getUserData(): Observable<IUser> {
    return  this.http.get<IUser> (this.profileInfoApiURL);
  }
  getUserTweets(): Observable<ITweet[]> {
    return this.http.get<ITweet[]>(this.profileTweetsApiURL);
  }
  getSubscriptionsCount(): Observable<number> {
    return this.http.get<number>(this.profileSubscriptionsCountApiURL);
  }
  getSubscribersCount(): Observable<number> {
    return this.http.get<number>(this.profileSubscribersCountApiURL);
  }
  getUserPostsCount(): Observable<number> {
    return this.http.get<number>(this.profilePostsCountApiURL);
  }
  
}
