import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpResponse } from 'selenium-webdriver/http';

import { map } from 'rxjs/operators';

import { IUser } from "./user.model";
import { ITweet } from "../tweet/tweet.model";


@Injectable()
export class ProfileService {
  constructor(@Inject('PROFILE_INFO_URL') private profileInfoApiURL: string,
    @Inject('PROFILE_TWEETS_URL') private profileTweetsApiURL: string,
    private http: HttpClient) { }

  getSampleUserData(): IUser {

    let user: IUser = {
      id: 0,
      login: 'Tarasoff',
      followers: 159,
      followings: 855,
      posts: 5,
      about: 'bla bla bla bla bla',
      regDate: new Date(1999, 1, 1)
    };
    return user;

  }
  getUserData(): Observable<IUser> {
    return  this.http.get<IUser> (this.profileInfoApiURL);
  }
  getUserTweets(): Observable<ITweet[]> {
    return this.http.get<ITweet[]>(this.profileTweetsApiURL);
  }
}
