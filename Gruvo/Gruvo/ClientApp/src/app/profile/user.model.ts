export interface IUser {
  id:number;
  login: string;
  regDate: Date;
  about?: string;
  isSubscribed?: boolean;
  birthDay?: Date;
  }
