import { Component, OnInit } from '@angular/core';
import { LoginService } from '@app/login/login.service';

@Component({
  selector: 'gr-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean;

  constructor(public loginService: LoginService) {
    this.isLoggedIn = false;

    this.loginService.isLoggedInSubject.subscribe((val) => {
      this.isLoggedIn = val;
    });
  }

  ngOnInit() {
    this.loginService.Test().subscribe(res => {
      this.isLoggedIn = true;
    }, err => {
      this.isLoggedIn = false;
      });


  }
 }
