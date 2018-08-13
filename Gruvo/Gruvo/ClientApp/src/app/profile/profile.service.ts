import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpResponse } from 'selenium-webdriver/http';

import { map } from 'rxjs/operators';

import { IUser } from "./user.model";
import { ITweet } from "../tweet/tweet.model";


@Injectable()
export class ProfileService {
  constructor(
    @Inject('PROFILE_INFO_URL') private profileInfoApiURL: string,
    @Inject('PROFILE_TWEETS_URL') private profileTweetsApiURL: string,
    @Inject('PROFILE_SUBSCRIPTIONS_URL') private profileSubscriptionsApiURL: string,
    @Inject('PROFILE_SUBSRIBERS_URL') private profileSubscribersApiURL: string,
    private http: HttpClient) { }

  getUserData(): Observable<IUser> {
    return  this.http.get<IUser> (this.profileInfoApiURL);
  }
  getUserTweets(): Observable<ITweet[]> {
    return this.http.get<ITweet[]>(this.profileTweetsApiURL);
  }
  getSubscriptions(): Observable<IUser[]> {
    return this.http.get<IUser[]>(this.profileSubscriptionsApiURL);
  }
  getSubscribers(): Observable<IUser[]> {
    return this.http.get<IUser[]>(this.profileSubscкшиукыApiURL);
  }
}
