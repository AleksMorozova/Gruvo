import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpResponse } from 'selenium-webdriver/http';
import { map } from 'rxjs/operators';
import { IUserEdit } from "@app/settings/user-edit.model";

@Injectable()
export class SettingsService {
  constructor(
    @Inject('PROFILE_EDIT_GET_INFO_URL') private profileInfoApiURL: string,
    @Inject('PROFILE_EDIT_INFO_URL') private editInfoApiURL: string,
    @Inject('PROFILE_EDIT_PASSWORD_URL') private editPasswordApiURL: string,
    private http: HttpClient) { }

  
  getUserData(): Observable<IUserEdit> {
    return this.http.get<IUserEdit>(this.profileInfoApiURL);
  }

  EditInfo(login: string, email: string, about: string, bday: Date): Observable<any> {
    let res: Observable<any> = this.http.post(this.editInfoApiURL,
      { Login: login, Email: email, About: about, Bday: bday},
      { responseType: 'text' });
    return res;
  }

  EditPassword(oldPassw: string, newPassw: string): Observable<any> {
    let res: Observable<any> = this.http.post(this.editPasswordApiURL,
      { oldpassword: oldPassw, newpassword: newPassw },
      { responseType: 'text' });
    return res;
  }
}
