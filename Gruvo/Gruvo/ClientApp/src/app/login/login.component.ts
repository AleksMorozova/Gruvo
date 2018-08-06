import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { LoginService } from './login.service';
import { Router } from '@angular/router';

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

  LogIn(formData: { email: string, password: string }): void {
    this.loginService.LogIn(formData.email, formData.password).subscribe(object => {
        this.correctCredentials = true;
        console.log('Logged in!!');
        //TODO: Redirect to 'stream' page
    }, error => {
      console.log(error);
      this.correctCredentials = false;
    }
      );
  }

 }
