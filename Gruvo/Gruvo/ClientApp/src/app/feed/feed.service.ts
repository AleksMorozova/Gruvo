import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators';
import { HttpResponse } from 'selenium-webdriver/http';
import { ITweet } from '../tweet/tweet.model';
import { IUser } from '../profile/user.model';

@Injectable()
export class FeedService {
  constructor(
    @Inject('FEED_TWEETS_URL') private feedTweetsApiURL: string,
    @Inject('FEED_RECOMMENDATIONS_URL') private feedRecommendApiUrl: string,
    private http: HttpClient) { }

  getTweets(): Observable<ITweet[]> {
    return this.http.get<ITweet[]>(this.feedTweetsApiURL);
  }

  getRecommendations(): Observable<IUser[]> {
    return this.http.get<IUser[]>(this.feedRecommendApiUrl);
  }
}
