import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpResponse } from 'selenium-webdriver/http';

@Injectable()
export class TweetService {
  constructor(
    @Inject('TWEET_LIKES_URL') private tweetGetLikesApiURL: string,
    @Inject('TWEET_LIKE_URL') private tweetLikeApiUrl: string,
    @Inject('TWEET_CHECKLIKED_URL') private tweetCheckLikedApiUrl: string,
    private http: HttpClient) { }

  like(tweetId: number): Observable<any> {
    let myHeader = new HttpHeaders().set('tweetId', tweetId.toString());
    return this.http.get(this.tweetLikeApiUrl, { headers: myHeader });
  }

  getNumOfLikes(tweetId: number): Observable<number> {
    let myHeader = new HttpHeaders().set('tweetId', tweetId.toString());
    return this.http.get<number>(this.tweetGetLikesApiURL, { headers: myHeader });
  }

  checkLiked(tweetId: number): Observable<boolean> {
    let myHeader = new HttpHeaders().set('tweetId', tweetId.toString());
    return this.http.get<boolean>(this.tweetCheckLikedApiUrl, { headers: myHeader });
  }
}
