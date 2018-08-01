import { Component, OnInit, HostBinding } from '@angular/core';

@Component({
  selector: 'gr-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string;
  password: string; 
 }
