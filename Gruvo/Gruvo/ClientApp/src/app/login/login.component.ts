import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { LoginService } from './login.service';
import { Router } from '@angular/router';
import * as crypto from "crypto-js";

@Component({
  selector: 'gr-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  correctCredentials: boolean = true;

  constructor(private loginService: LoginService, private router: Router, formBuilder: FormBuilder) {
    this.loginForm = formBuilder.group({
      'email': ['', Validators.compose([Validators.required, Validators.email])],
      'password': ['', Validators.required]
    });

    this.loginForm.controls
  }

  LogIn(formData: any): void {
    this.loginService.LogIn(formData.email, crypto.MD5(formData.password).toString()).subscribe(object => {
      this.correctCredentials = true;
      console.log('Logged in!!');
      this.router.navigate(['/feed']);
    }, error => {
      console.log(error);
      this.correctCredentials = false;
    }
      );
  }

 }
