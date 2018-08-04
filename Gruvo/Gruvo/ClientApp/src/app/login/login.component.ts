import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { LoginService } from './login.service';

@Component({
  selector: 'gr-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private loginService: LoginService, formBuilder: FormBuilder) {
    this.loginForm = formBuilder.group({
      'email': ['', Validators.compose([Validators.required, Validators.email])],
      'password': ['', Validators.required]
    });

    this.loginForm.controls
  }

  LogIn(formData: object): void {
    console.log(this.loginService.LogIn(formData.email, formData.password));
  }

 }
