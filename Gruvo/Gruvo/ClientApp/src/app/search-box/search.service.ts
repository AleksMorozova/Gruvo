import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpResponse } from 'selenium-webdriver/http';
import { IUser } from '@app/profile/user.model';

@Injectable()
export class SearchService {
  constructor(
    @Inject('SEARCH_USERS_URL') private searchUsersURL: string,
    private http: HttpClient) { }

  getUsers(login: string, lastUserId: number): Observable<IUser[]> {
    let params = new HttpParams()
      .set("lastUserId", lastUserId ? lastUserId.toString() : undefined)
      .set("login", login);
    return this.http.get<IUser[]>(this.searchUsersURL, { params: params });
  }
  
}
