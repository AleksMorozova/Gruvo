export interface IComment {
  commentId: number;
  tweetId: number;
  userId: number;
  userLogin: string;
  message: string;
  sendingDateTime: Date;
  isDeletable: boolean;
}
