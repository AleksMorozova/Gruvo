import { Component, OnInit, HostBinding } from '@angular/core';
import { LoginService } from './login.service';

@Component({
  selector: 'gr-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string;
  password: string;

  constructor(private loginService: LoginService) { }

  LogIn(email: string, password: string): void {
    console.log(this.loginService.LogIn(email, password));
  }

 }
