import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpResponse } from 'selenium-webdriver/http';

@Injectable()
export class TweetService {
  constructor(
    @Inject('TWEET_LIKES_URL') private tweetGetLikesApiURL: string,
    @Inject('TWEET_LIKE_URL') private tweetLikeApiUrl: string,
    private http: HttpClient) { }

  like(tweetId: number): Observable<any> {
    let myHeader = new HttpHeaders().set('tweetId', tweetId.toString());
    return this.http.post(this.tweetLikeApiUrl, {}, {headers: myHeader});
  }

  getNumOfLikes(tweetId: number): Observable<number> {
    let myHeader = new HttpHeaders().set('tweetId', tweetId.toString());
    return this.http.post<number>(this.tweetGetLikesApiURL, {}, {headers: myHeader});
  }

  checkLiked() {

  }
}
