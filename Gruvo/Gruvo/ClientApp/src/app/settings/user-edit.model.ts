export interface IUserEdit {
  id: number;
  login: string;
  email: string;
  about?: string;
  birthDay?: Date;
}
