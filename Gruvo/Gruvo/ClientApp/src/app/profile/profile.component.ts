import { Component, OnInit, OnDestroy } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { ProfileService } from '@app/profile/profile.service';
import { ITweet } from '@app/tweet/tweet.model';
import { Subscription } from 'rxjs';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'gr-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit, OnDestroy {

  user: IUser;
  userTweets: ITweet[] = [];
  subscriptions: IUser[] = [];
  subscribers: IUser[] = [];
  timerSubscription: Subscription;

  ngOnInit(): void {
    this.profileService.getUserData()
      .subscribe((user) => {
        this.user = user;
      });

    this.refreshData();
  }

  ngOnDestroy() {
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }

  refreshData() {
    this.profileService.getUserTweets()
      .subscribe((tweets) => {
        if (this.userTweets[0]) {
          if (this.userTweets[0].id != tweets[0].id) {
            this.userTweets = tweets;
          }
        }
        else {
          this.userTweets = tweets;
        }
      });

    this.profileService.getSubscriptions()
      .subscribe((subscriptions) => {
        this.subscriptions = subscriptions;
      });

    this.profileService.getSubscribers()
      .subscribe((subscribers) => {
        this.subscribers = subscribers;
      });

    this.subscribeToData();
  }

  subscribeToData() {
    this.timerSubscription = Observable.timer(5000)
      .first()
      .subscribe(() => this.refreshData());
  }

  constructor(private profileService: ProfileService) {
   

  }
}
