import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';
import { LoginService } from '../login/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'gr-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  signupForm: FormGroup;
  correctCredentials: boolean = true;

  constructor(private loginService: LoginService, private router: Router, formBuilder: FormBuilder) {
    this.signupForm = formBuilder.group({

      //TODO: add validators to check if email/login is taken

      'login': ['', Validators.compose([Validators.required, Validators.pattern(/^[a-zA-Z0-9_]{3,50}$/)])],
      'email': ['', Validators.compose([Validators.required, Validators.email])],
      'password': ['', Validators.required]
    });
  }

  SignUp(formData: {login:string, email:string, password:string}): void {
    this.loginService.SignUp(formData.login, formData.email, formData.password).subscribe(object => {
      this.correctCredentials = true;
      console.log('Registration completed!');
      this.router.navigate(['']);
    }, error => {
      this.correctCredentials = false;
      console.log(error);
    })};

  isValidControl(controlName): boolean {
    return !this.signupForm.controls[controlName].valid && this.signupForm.controls[controlName].touched;
  }
}
