import { Component, OnInit } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { ProfileService } from '@app/profile/profile.service';
import { ITweet } from '@app/tweet/tweet.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'gr-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent {
  user: IUser;
  userTweets: ITweet[] = [];
  subscriptions: IUser[];
  subscribers: IUser[];
  paramId: number;

  constructor(private profileService: ProfileService, route: ActivatedRoute,private router:Router) {
    route.params.subscribe(
      params => {
        this.paramId = +params['id'];

        this.profileService.getUserData(this.paramId)
          .subscribe((user) => {
            this.user = user;
          },err => router.navigate(['profile']));
        this.profileService.getUserTweets(this.paramId)
          .subscribe((tweets) => {
            this.userTweets = tweets;
          });
        this.profileService.getSubscriptions(this.paramId)
          .subscribe((subscriptions) => {
            this.subscriptions = subscriptions;
          });
        this.profileService.getSubscribers(this.paramId)
          .subscribe((subscribers) => {
            this.subscribers = subscribers;
          });
      }
    );
  }

  subscribe() {
    this.profileService.subscribe(this.paramId).subscribe(
      () => this.user.IsSubscribed = true
    );
  }

  unsubscribe() {
    this.profileService.unsubscribe(this.paramId).subscribe(
      () => this.user.IsSubscribed = false
    );
  }

}
