import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpResponse } from 'selenium-webdriver/http';
import { map } from 'rxjs/operators';

@Injectable()
export class TweetService {
  constructor(
    @Inject('TWEET_DELETE_TWEET_URL') private tweetDeleteTweetApiURL: string,
    @Inject('TWEET_LIKES_URL') private tweetGetLikesApiURL: string,
    @Inject('TWEET_LIKE_URL') private tweetLikeApiUrl: string,
    @Inject('TWEET_CHECKLIKED_URL') private tweetCheckLikedApiUrl: string,
    private http: HttpClient) { }

  like(tweetId: number): Observable<any> {
    let params = new HttpParams().set("tweetId", tweetId.toString());
    return this.http.get(this.tweetLikeApiUrl, { params: params });
  }

  getNumOfLikes(tweetId: number): Observable<number> {
    let params = new HttpParams().set("tweetId", tweetId.toString());
    return this.http.get<number>(this.tweetGetLikesApiURL, { params: params });
  }

  checkLiked(tweetId: number): Observable<boolean> {
    let params = new HttpParams().set("tweetId", tweetId.toString());
    return this.http.get<boolean>(this.tweetCheckLikedApiUrl, { params: params });
  }

  deleteTweet(tweetId: number) {
    let params = new HttpParams().set("tweetId", tweetId.toString());
    return this.http.post(this.tweetDeleteTweetApiURL, tweetId);
  }
}
