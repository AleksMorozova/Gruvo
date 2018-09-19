import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { ProfileService } from '@app/profile/profile.service';
import { ITweet } from '@app/tweet/tweet.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Observable } from 'rxjs/Observable';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { SubscriptionsComponent } from '@app/subscriptions/subscriptions.component';
import { SubscribersComponent } from '@app/subscribers/subscribers.component';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'gr-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit, OnDestroy {

  user: IUser;
  tweets: ITweet[] = [];
  paramId: number;
  button: any;
  numOfSubscriptions: number;
  numOfSubscribers: number;
  timerSubscription: Subscription;
  modalRef: BsModalRef;
  lastdate: Date = new Date();
  newTweets: boolean = false;
  photosPath: string = '';
  imgData: any;

  constructor(private profileService: ProfileService,
    route: ActivatedRoute,
    private router: Router,
    private modalService: BsModalService,
    private sanitizer: DomSanitizer) {
    route.params.subscribe(
      params => this.paramId = +params['id']
    );
  }

  ngOnInit() {    
    this.profileService.getUserData(this.paramId)
      .subscribe((user) => {
        this.user = user;
        if(this.button = document.getElementById('sbscrbtn')){        
        if (this.user.isSubscribed) {
          this.button.classList.add('btn-primary');
          this.button.innerHTML = 'Unsubscribe';
        } else {
          this.button.classList.add('btn-success');
          this.button.innerHTML = 'Subscribe';
        }
        this.button.classList.remove('hidden');
      }
      }, err => this.router.navigate(['profile']));
    this.profileService.getPhoto(this.paramId).subscribe(blob => {
      let urlCreator = window.URL;
      this.imgData = this.sanitizer.bypassSecurityTrustUrl(
        urlCreator.createObjectURL(blob));
    }, () => { this.imgData ='./assets/images/no_avatar_profile.png' });

    this.loadMoreTweets();
    this.subscribeToData();
  }

  ngOnDestroy() {
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }

  subscribeToData() {
    this.timerSubscription = Observable.timer(10000)
      .first()
      .subscribe(() => this.checkForNewTweets());
  }

  checkForNewTweets() {
    this.profileService.getTweetsBatch(new Date(), this.paramId)
      .subscribe((tweets) => {
        console.log(tweets);
        for (var i = tweets.length - 1; i >= 0; i--) {
          if (this.tweets.every(x => x.id != tweets[i].id)) {
            this.tweets.unshift(tweets[i]);
          }
        }
      });
    this.subscribeToData();
  }

  onScroll() {
    console.log("Scrolled");
    this.loadMoreTweets();
  }

  loadMoreTweets() {
    if (this.lastdate){
      this.newTweets=true;
      this.profileService.getTweetsBatch(this.lastdate, this.paramId)
        .subscribe((tweets) => {
          for (var i = 0; i < tweets.length; i++) {
            if (this.tweets.every(x => x.id != tweets[i].id)) {
              this.tweets.push(tweets[i]);
            }        
          }
          this.newTweets=false;
          this.lastdate = tweets.length ? new Date(tweets[tweets.length - 1].sendingDateTime) : undefined;
        }
      );
    }
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

  openFollowingModal() {
    const initialState = {
      paramId: this.paramId,
      class: 'modal-sm'
    };

    this.modalRef = this.modalService.show(SubscriptionsComponent, { initialState });
  }

  openFollowersModal() {
    const initialState = {
      paramId: this.paramId,
      class: 'modal-sm'
    };

    this.modalRef = this.modalService.show(SubscribersComponent, { initialState });
  }

}
