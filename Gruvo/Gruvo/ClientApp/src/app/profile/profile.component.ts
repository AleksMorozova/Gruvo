import { Component, OnInit } from '@angular/core';
import { IUser } from './user.model';
import { ProfileService } from './profile.service';
import { ITweet } from '../tweet/tweet.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'gr-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {

  constructor(private profileService: ProfileService) {
  }

  user: IUser;
  userTweets: Array<ITweet>;
  userTweetsQlt: number;
  subscriptionsQlt: number;
  subscribersQlt: number;

  ngOnInit(): void {    
    this.loadUserData();
    this.loadUserTweets();
    this.loadSubscribersQuality();
    this.loadSubscriptionsQuality();
    this.loadUserPostsQuality();
  }
  scrollToTop() {
    window.scrollTo(0,0);
  }

  loadUserData() {
    this.profileService.getUserData()
      .subscribe((user) => {
        this.user = user;
      });
  }
  loadUserTweets() {
    this.profileService.getUserTweets()
      .subscribe((tweets) => {
        this.userTweets = tweets;
      });
  }
  loadSubscriptionsQuality() {
    this.profileService.getSubscriptionsQuality()
      .subscribe((subscriptionsQlt) => {
        this.subscriptionsQlt = subscriptionsQlt;
      });
  }
  loadSubscribersQuality() {
    this.profileService.getSubscribersQuality()
      .subscribe((subscribersQlt) => {
        this.subscribersQlt = subscribersQlt;
      });
  }
  loadUserPostsQuality() {
    this.profileService.getUserPostsQuality()
      .subscribe((postsQlt) => {
        this.userTweetsQlt = postsQlt;
      });
  }
}
