import { Component, OnInit } from '@angular/core';
import { IUser } from './user.model';
import { ProfileService } from './profile.service';
import { ITweet } from '../tweet/tweet.model';

@Component({
  selector: 'gr-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {

  user: IUser;
  userTweets: Array<ITweet>;
  userTweetsQlt: number;
  subscriptionsQlt: number;
  subscribersQlt: number;

  ngOnInit(): void {
    this.profileService.getUserData()
      .subscribe((user) => {
        this.user = user;
      });
    this.profileService.getUserTweets()
      .subscribe((tweets) => {
        this.userTweets = tweets;
      });
    this.profileService.getSubscriptionsQuality()
      .subscribe((subscriptionsQlt) => {
        this.subscriptionsQlt = subscriptionsQlt;
      });
    this.profileService.getSubscribersQuality()
      .subscribe((subscribersQlt) => {
        this.subscribersQlt = subscribersQlt;
      });
    this.profileService.getUserPostsQuality()
      .subscribe((postsQlt) => {
        this.userTweetsQlt = postsQlt;
      });
  }

  constructor(private profileService: ProfileService) {
   

  }
}
