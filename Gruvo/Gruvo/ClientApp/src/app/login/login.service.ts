import { Injectable } from '@angular/core';

@Injectable()
export class LoginService {

  LogIn(email: string, password: string): boolean {
    if (email == "test@mail.com" && password == 'test') return true;
    return false;
  }

  SignUp(Login: string, email: string, password: string): boolean {
    if (Login && email && password) return true;
    return false;   
  }
 }
