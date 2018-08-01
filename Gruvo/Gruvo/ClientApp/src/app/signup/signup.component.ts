import { Component, OnInit } from '@angular/core';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'gr-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {

  login: string;
  email: string;
  password: string;

  constructor(private loginService: LoginService) { }

  SignUp(login: string, email: string, password: string): void{
    console.log(this.loginService.SignUp(login, email, password));
  }
}
