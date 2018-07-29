import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'gr-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {

  email: string;
  pass: string;

  constructor() {
  }
  ngOnInit() {
  }
}
