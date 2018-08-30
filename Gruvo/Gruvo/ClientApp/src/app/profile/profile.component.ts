import { Component, OnInit, OnDestroy } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { ProfileService } from '@app/profile/profile.service';
import { ITweet } from '@app/tweet/tweet.model';
import { ActivatedRoute, Router } from '@angular/router';
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
    paramId: number;
    button: any;
    subscriptions: IUser[] = [];
    subscribers: IUser[] = [];
    timerSubscription: Subscription;


    ngOnInit(): void {
        this.profileService.getUserData(this.paramId)
            .subscribe((user) => {
                this.user = user;
                this.button = document.getElementById('sbscrbtn');
                if (this.user.isSubscribed) {
                    this.button.classList.add('btn-primary');
                    this.button.innerHTML = 'Unsubscribe';
                } else {
                    this.button.classList.add('btn-success');
                    this.button.innerHTML = 'Subscribe';
                }
                this.button.classList.remove('hidden');
            }, err => this.router.navigate(['profile']));

        this.refreshData();
    }

    ngOnDestroy() {
        if (this.timerSubscription) {
            this.timerSubscription.unsubscribe();
        }
    }

    refreshData() {
        this.profileService.getUserTweets(this.paramId)
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

        this.profileService.getSubscriptions(this.paramId)
            .subscribe((subscriptions) => {
                this.subscriptions = subscriptions;
            });

        this.profileService.getSubscribers(this.paramId)
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
    constructor(private profileService: ProfileService, route: ActivatedRoute, private router: Router) {
        route.params.subscribe(
            params =>  this.paramId = +params['id']   
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

}
