import { Injectable } from "@angular/core";
import { IUser } from "./user.model";

@Injectable()
export class ProfileService {

  getUserData(): IUser {

    let user: IUser = {
      id: 0,
      login: 'Tarasoff',
      followers: 159,
      followings: 855,
      posts: 5,
      about: 'bla bla bla bla bla',
      regDate: new Date(1999, 1, 1)
    };
    return user;

  }

  constructor(){}

}
