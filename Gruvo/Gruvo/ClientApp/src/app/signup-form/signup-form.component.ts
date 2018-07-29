import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'gr-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.css']
})
export class SignupFormComponent implements OnInit {

  login: string;
  email: string;
  pass: string;

  constructor() {
  }
  ngOnInit() {
  }
}
