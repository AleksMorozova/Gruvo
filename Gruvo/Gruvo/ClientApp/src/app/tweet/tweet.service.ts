import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpResponse } from 'selenium-webdriver/http';

import { map } from 'rxjs/operators';

@Injectable()
export class TweetService {
  constructor(
    @Inject('TWEET_DELETE_TWEET_URL') private tweetDeleteTweetApiURL: string,
    private http: HttpClient) { }

  deleteTweet(id: number) {
    console.log(id);
    const headers = new HttpHeaders().set('Content-Type', 'application/json');  
    return this.http.post(this.tweetDeleteTweetApiURL,
      JSON.stringify( id ), { headers: headers });    
  }
  
}
