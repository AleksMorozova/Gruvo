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
  userTweetsCount: number;
  subscriptionsCount: number;
  subscribersCount: number;

  ngOnInit(): void {    
    this.loadUserData();
    this.loadUserTweets();
    this.loadSubscribersCount();
    this.loadSubscriptionsCount();
    this.loadUserPostsCount();
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

  loadSubscriptionsCount() {
    this.profileService.getSubscriptionsCount()
      .subscribe((subscriptionsCount) => {
        this.subscriptionsCount = subscriptionsCount;
      });
  }

  loadSubscribersCount() {
    this.profileService.getSubscribersCount()
      .subscribe((subscribersCount) => {
        this.subscribersCount = subscribersCount;
      });
  }

  loadUserPostsCount() {
    this.profileService.getUserPostsCount()
      .subscribe((postsCount) => {
        this.userTweetsCount = postsCount;
      });
  }
}
