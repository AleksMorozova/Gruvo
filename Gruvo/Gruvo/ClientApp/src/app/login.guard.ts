import { CanActivate, Router } from "@angular/router";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable, Inject } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { LoginService } from "./login/login.service";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/map';

@Injectable()
export class LoginGuard implements CanActivate {

  constructor(private loginService: LoginService, private router: Router) { }

  canActivate() {
    return this.loginService.Test()
      .map(res => {
        console.log('You already have cookie, Proceed onto your page...');
        this.router.navigate(['profile']);
        return false;
      })
      .catch(
      (err: HttpErrorResponse) => {
        console.log('You have no cookie. you should sign up/ log in first.');
        console.log(err);
        return Observable.of(true);
      }
    );
  }
}
