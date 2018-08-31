import { Component, OnInit, OnDestroy } from '@angular/core';
import { ITweet } from '@app/tweet/tweet.model';
import { IUser } from '@app/profile/user.model';
import { FeedService } from '@app/feed/feed.service';
import { Observable, Subscriber } from 'rxjs/Rx';
import { Subscription } from 'rxjs';
import { ProfileComponent } from '@app/profile/profile.component';

@Component({
  selector: 'gr-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})

export class FeedComponent implements OnInit, OnDestroy {
  tweets: ITweet[] = [];
  recommendations: IUser[] = [];
  timerSubscription: Subscription;

  constructor(private feedService: FeedService) {

  }

  ngOnInit() {
    this.refreshData();

    this.feedService.getRecommendations()
      .subscribe((recommendations) => {
        this.recommendations = recommendations;
      });
  }

  ngOnDestroy() {
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }

  refreshData() {
    this.feedService.getTweets()
      .subscribe((tweets) => {
        try {
          if (this.tweets[0]) {
            if ((this.tweets[0].id != tweets[0].id) ||
               (tweets.length < this.tweets.length) ||
               (this.tweets[this.tweets.length - 1].id != tweets[this.tweets.length - 1].id)) {
              this.tweets = tweets;
            }
          }
          else {
            this.tweets = tweets;
          }
        }
        catch (e) {
          this.tweets = tweets;
        }
      });

    this.subscribeToData();
  }

  subscribeToData() {
    this.timerSubscription = Observable.timer(2000)
      .first()
      .subscribe(() => this.refreshData());
  }
}
