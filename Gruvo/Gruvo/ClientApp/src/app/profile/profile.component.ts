import { Component } from '@angular/core';
import { IUser } from './user.model';
import { ProfileService } from './profile.service';
import { ITweet } from '../tweet/tweet.model';

@Component({
  selector: 'gr-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent  {
  user: IUser;
  userTweets: ITweet[];
  subscriptions: IUser[];
  subscribers: IUser[];

  constructor(private profileService: ProfileService) {
    this.profileService.getUserData()
      .subscribe((user) => {
        this.user = user;
      });
    this.profileService.getUserTweets()
      .subscribe((tweets) => {
        this.userTweets = tweets;
      });
    this.profileService.getSubscriptions()
      .subscribe((subscriptions) => {
        this.subscriptions = subscriptions;
      });
    this.profileService.getSubscribers()
      .subscribe((subscribers) => {
        this.subscribers = subscribers;
      });

  }
}
