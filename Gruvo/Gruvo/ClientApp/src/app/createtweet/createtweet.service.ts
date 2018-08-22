import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpResponse } from 'selenium-webdriver/http';

import { map } from 'rxjs/operators';

import { IUser } from "@app/profile/user.model";
import { ITweet } from "@app/tweet/tweet.model";

@Injectable()
export class CreateTweetService {
  constructor(
    @Inject('CREATETWEET_POST_TWEET_URL') private createtweetPostTweetApiURL: string,
    private http: HttpClient) { }

  postTweet(message: string) {
    console.log(message);
    const headers = new HttpHeaders().set('Content-Type', 'application/json');  
    return this.http.post(this.createtweetPostTweetApiURL,
      JSON.stringify( message ), { headers: headers });    
  }
  
}
