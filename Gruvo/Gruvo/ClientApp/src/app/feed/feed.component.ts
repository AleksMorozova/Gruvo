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
  lastdate: Date = new Date();

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
    if (!this.lastdate) return;
    this.feedService.getTweetsBatch(this.lastdate)
      .subscribe((tweets) => {
        //this.tweets = this.tweets.concat(tweets);
        for (var i = 0; i < tweets.length; i++) {
          if (this.tweets.indexOf(tweets[i]) == -1) {
            console.log(tweets[i]);
            this.tweets.push(tweets[i]);
          }
        }
        this.lastdate = tweets.length ? new Date(tweets[tweets.length - 1].sendingDateTime) : undefined;
        //this.lastdate = new Date(tweets[tweets.length - 1].sendingDateTime);
      }
      );
    /*
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
          this.tweets = [];
        }
      });
      */
    //this.subscribeToData();

  }

  subscribeToData() {
    this.timerSubscription = Observable.timer(2000)
      .first()
      .subscribe(() => this.refreshData());
  }

  onScroll() {
    console.log("Scroll");
    this.refreshData();
  }

}
