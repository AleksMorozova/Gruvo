//TODO add methods to check if email/login is taken

import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators';
import { HttpResponse } from 'selenium-webdriver/http';

@Injectable()
export class LoginService {
  
  constructor(
    @Inject('LOGIN_URL') private loginApiURL: string,
    @Inject('SIGNUP_URL') private signupApiURL: string,
    @Inject('TEST_URL') private testApiURL: string,
    @Inject('CONFIRM_EMAIL_URL') private confirmEmailApiURL: string,
    @Inject('GET_VERIFICATION_CODE_URL') private getVerificationCodeApiURL: string,
    private http: HttpClient) { }

  LogIn(email: string, password: string): Observable<any> {
    let observable: Observable<any> = this.http.post(this.loginApiURL,
      { email: email, password: password },
      { responseType: 'text' });
    return observable;
  }

  SignUp(login: string, email: string, password: string, verificationCodeInput: string, sendedCode: string): Observable<any> {
    console.log(this.signupApiURL);
    let observable: Observable<any> = this.http.post(this.signupApiURL,
      { email: email, password: password, login: login, verificationCodeInput: verificationCodeInput, sendedVerificationCode: sendedCode },
      { responseType: 'text' });
    return observable;
  }

  GetVerificationCode(): Observable<any> {
    return this.http.get(this.getVerificationCodeApiURL, { responseType: 'text' });
  }

  SentVerificationCode(login: string, email: string, code: string): Observable<any> {
    let observable: Observable<any> = this.http.post(this.confirmEmailApiURL,
    { email: email, login: login, sendedVerificationCode: code},
    { responseType: 'text' });
    return observable;
  }

  Test() {
    return this.http.get(this.testApiURL, { responseType: 'text' });
  }
}
