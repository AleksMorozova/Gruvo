import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'gr-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {

  login: string;
  email: string;
  password: string; 
}
