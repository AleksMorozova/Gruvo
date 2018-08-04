//TODO: add validators to check if email/login is taken

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl, ValidationErrors } from '@angular/forms';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'gr-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  signupForm: FormGroup;

  constructor(private loginService: LoginService, formBuilder: FormBuilder) {
    this.signupForm = formBuilder.group({
      'login': ['', Validators.compose([Validators.required])],
      'email': ['', Validators.compose([Validators.required, Validators.email])],
      'password': ['', Validators.required]
    });
  }

  SignUp(formData: object): void{
    console.log(this.loginService.SignUp(formData.login, formData.email, formData.password));
  }
}
