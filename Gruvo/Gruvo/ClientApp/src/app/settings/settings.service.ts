import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpResponse } from 'selenium-webdriver/http';
import { map } from 'rxjs/operators';
import { IUser } from "@app/profile/user.model";

@Injectable()
export class SettingsService {
  constructor(
    @Inject('PROFILE_INFO_URL') private profileInfoApiURL: string,
    private http: HttpClient) { }

  //TODO: add edit api

  getUserData(): Observable<IUser> {
    return this.http.get<IUser>(this.profileInfoApiURL);
  }

  EditInfo(login: string, about: string, bday: Date): Observable<any> {
    let res: Observable<any> = new Observable<any>();
    return res;
  }

  EditPassword(oldPassw: string, newPassw: string): Observable<any> {
    let res: Observable<any> = new Observable<any>();
    return res;
  }
}
