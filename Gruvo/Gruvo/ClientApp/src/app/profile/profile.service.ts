import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
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
    @Inject('PROFILE_SUBSCRIPTIONS_COUNT_URL') private profileSubscriptionsCountURL: string,
    @Inject('PROFILE_SUBSCRIBERS_COUNT_URL') private profileSubscribersCountURL: string,
    @Inject('USERTWEETS_BATCH_URL') private tweetsBatchApiUrl: string,
    private http: HttpClient) { }

  getUserData(id?: number): Observable<IUser> {
    return this.http.get<IUser>(id ? this.profileInfoApiURL+'/'+id: this.profileInfoApiURL);
  }
  getUserTweets(id?: number): Observable<ITweet[]> {
    return this.http.get<ITweet[]>(id ? this.profileTweetsApiURL + '/' + id : this.profileTweetsApiURL);
  }
  getSubscriptions(lastSubscriptionId: number, id?: number): Observable<IUser[]> {
    let params = new HttpParams().set("subscriptionId", lastSubscriptionId ? lastSubscriptionId.toString() : undefined);
    return this.http.get<IUser[]>(id ? this.profileSubscriptionsApiURL + '/' + id : this.profileSubscriptionsApiURL, { params: params } );
  }
  getSubscriptionsCount(id?: number): Observable<number> {
    return this.http.get<number>(id ? this.profileSubscriptionsCountURL + '/' + id : this.profileSubscriptionsCountURL);
  }
  getSubscribers(lastSubscriberId: number, id?: number): Observable<IUser[]> {
    let params = new HttpParams().set("subscriberId", lastSubscriberId ? lastSubscriberId.toString() : undefined);
    return this.http.get<IUser[]>(id ? this.profileSubscribersApiURL + '/' + id : this.profileSubscribersApiURL, { params: params} );
  }
  getSubscribersCount(id?: number): Observable<number> {
    return this.http.get<number>(id ? this.profileSubscribersCountURL + '/' + id : this.profileSubscribersCountURL);
  }
  subscribe(id: number): Observable<any>  {
    return this.http.post(this.profileSubscribeApiURL,  id );
  }
  unsubscribe(id: number): Observable<any> {
    return this.http.post(this.profileUnsubscribeApiURL, id );
  }
  getTweetsBatch(date: Date, id?: number): Observable<ITweet[]> {
    return this.http.post<ITweet[]>(this.tweetsBatchApiUrl, { date: date, id: id});
  }
}
