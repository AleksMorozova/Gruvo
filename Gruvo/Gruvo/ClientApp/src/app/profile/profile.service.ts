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
    @Inject('PROFILE_SUBSCRIPTIONS_URL') private profileSubscriptionsApiURL: string,
    @Inject('PROFILE_SUBSRIBERS_URL') private profileSubscribersApiURL: string,
    @Inject('PROFILE_SUBSCRIBE_URL') private profileSubscribeApiURL: string,
    @Inject('PROFILE_UNSUBSCRIBE_URL') private profileUnsubscribeApiURL: string,
     
    private http: HttpClient) { }

  getUserData(id?: number): Observable<IUser> {
    return this.http.get<IUser>(id ? this.profileInfoApiURL+'/'+id: this.profileInfoApiURL);
  }
  getUserTweets(id?: number): Observable<ITweet[]> {
    return this.http.get<ITweet[]>(id ? this.profileTweetsApiURL + '/' + id : this.profileTweetsApiURL);
  }
  getSubscriptions(id?: number): Observable<IUser[]> {
    return this.http.get<IUser[]>(id ? this.profileSubscriptionsApiURL + '/' + id : this.profileSubscriptionsApiURL);
  }
  getSubscribers(id?: number): Observable<IUser[]> {
    return this.http.get<IUser[]>(id ? this.profileSubscribersApiURL + '/' + id : this.profileSubscribersApiURL );
  }
  subscribe(id: number): Observable<any>  {
    return this.http.post(this.profileSubscribeApiURL,  id );
  }
  unsubscribe(id: number): Observable<any> {
    return this.http.post(this.profileUnsubscribeApiURL, id );
  }  
}
