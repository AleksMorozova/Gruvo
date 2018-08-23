import { Component, OnInit, OnDestroy } from '@angular/core';
import { ITweet } from '@app/tweet/tweet.model';
import { IUser } from '@app/profile/user.model';
import { FeedService } from '@app/feed/feed.service';
import { Observable, Subscriber } from 'rxjs/Rx';
import { Subscription } from 'rxjs';

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
    this.feedService.getTweets()
      .subscribe((tweets) => {
        this.tweets = tweets;
      });

    this.feedService.getRecommendations()
      .subscribe((recommendations) => {
        this.recommendations = recommendations;
      });

    this.refreshData();
  }

  ngOnDestroy() {
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }

  refreshData() {
    this.feedService.getTweets()
      .subscribe((tweets) => {
        this.tweets = tweets;
      });

    this.feedService.getRecommendations()
      .subscribe((recommendations) => {
        this.recommendations = recommendations;
      });

    this.subscribeToData();
  }

  subscribeToData() {
    this.timerSubscription = Observable.timer(5000)
      .first()
      .subscribe(() => this.refreshData());
  }
}
