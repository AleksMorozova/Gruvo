import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';
import { LoginService } from '@app/login/login.service';
import { Router } from '@angular/router';
import * as crypto from "crypto-js";
import { PasswordValidation } from '@app/PasswordValidation'

@Component({
  selector: 'gr-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  signupForm: FormGroup;
  sendedCode: string;
  correctCredentials: boolean = true;
  emailSent: boolean = false;
  showErrorMessage: boolean = false;

  constructor(private loginService: LoginService, private router: Router, formBuilder: FormBuilder) {
    this.signupForm = formBuilder.group({

      //TODO: add validators to check if email/login is taken

      'login': ['', Validators.compose([Validators.required, Validators.pattern(/^[a-zA-Z0-9_]{3,50}$/)])],
      'email': ['', Validators.compose([Validators.required, Validators.email])],
      'password': ['', Validators.required],
      'verificationCode': ['', Validators.required]
    });
      'confirmPassword': ['']
    }, {
        validator: PasswordValidation.PasswordsMatch
      });
  }

  SignUp(formData: any): void {
    this.loginService.SignUp(formData.login, formData.email, crypto.MD5(formData.password).toString(), formData.verificationCode, this.sendedCode).subscribe(object => {
        this.correctCredentials = true;
      console.log('Registration completed!');
      this.router.navigate(['']);
    }, error => {
      this.correctCredentials = false;
      console.log(error);
      })
  };

  SentVerificationCode(formData: any): void {
    this.loginService.GetVerificationCode()
      .do(code => this.sendedCode = code)
      .flatMap(() => this.loginService.SentVerificationCode(formData.login, formData.email, this.sendedCode))
      .subscribe(object => {
        this.emailSent = true;
        this.showErrorMessage = false;
    }, error => {
      this.emailSent = false;
      this.showErrorMessage = true;
      console.log(error);
    })
  };

  isValidControl(controlName): boolean {
    return !this.signupForm.controls[controlName].valid && this.signupForm.controls[controlName].touched;
  }

  Cancel(): void {
    this.emailSent = false;
    this.sendedCode = undefined;
  }
}
