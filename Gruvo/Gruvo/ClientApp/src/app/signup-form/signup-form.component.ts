import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'gr-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.css']
})
export class SignupFormComponent {

  login: string;
  email: string;
  password: string; 
}
