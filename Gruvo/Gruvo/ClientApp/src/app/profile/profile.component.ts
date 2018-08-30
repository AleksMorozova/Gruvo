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
  button: any;

  constructor(private profileService: ProfileService, route: ActivatedRoute,private router:Router) {
    route.params.subscribe(
      params => {
        this.paramId = +params['id'];

        this.profileService.getUserData(this.paramId)
          .subscribe((user) => {
            this.user = user;
            this.button = document.getElementById('sbscrbtn');
            if (this.user.isSubscribed)
            {
              this.button.classList.add('btn-primary');
              this.button.innerHTML = 'Unsubscribe';
            } else {
              this.button.classList.add('btn-success');
              this.button.innerHTML = 'Subscribe';
            }
            this.button.classList.remove('hidden');
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

  subfunc() {
    if (this.button) {
      this.button.setAttribute("disabled", "disabled");
      if (this.user.isSubscribed) {
        this.profileService.unsubscribe(this.paramId).subscribe(
          () => {
            this.user.isSubscribed = false;
            this.button.classList.replace('btn-primary', 'btn-success');
            this.button.innerHTML = 'Subscribe';
            this.button.removeAttribute('disabled');
          }
        );
      }
      else {
        this.profileService.subscribe(this.paramId).subscribe(
          () => {
            this.user.isSubscribed = true;
            this.button.classList.replace('btn-success', 'btn-primary');
            this.button.innerHTML = 'Unsubscribe';
            this.button.removeAttribute('disabled');
          }
        );
      }
    }    
  }

  unsubscribe() {
    this.profileService.unsubscribe(this.paramId).subscribe(
      () => this.user.isSubscribed = false
    );
  }

}
