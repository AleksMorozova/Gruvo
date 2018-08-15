import { Component, OnInit } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { ProfileService } from '@app/profile/profile.service';
import { ITweet } from '@app/tweet/tweet.model';

@Component({
  selector: 'gr-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {

  user: IUser;
  userTweets: Array<ITweet>;
  subscriptions: IUser[];
  subscribers: IUser[];

  ngOnInit(): void {
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

  constructor(private profileService: ProfileService) {
   

  }
}
