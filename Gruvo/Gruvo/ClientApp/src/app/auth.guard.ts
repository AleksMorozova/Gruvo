import { CanActivate, Router } from "@angular/router";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable, Inject } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { LoginService } from "./login/login.service";
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/map';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private loginService: LoginService, private router: Router) { }

  canActivate() {
    return this.loginService.Test()
      .map(res => {
        console.log('Welcome');
        return true;
      })
      .catch(
      (err: HttpErrorResponse) => {
        console.log('You have no access. you should sign up/ log in first.');
        this.router.navigate(['']);
        return Observable.of(false);
      }
    );
  }
}
