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

    this.loadMoreTweets();

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

  checkForNewTweets() {
    this.feedService.getTweetsBatch(new Date())
      .subscribe((tweets) => {
        for (var i = tweets.length - 1; i > 0; i--) {
          if (this.tweets.every(x => x.id != tweets[i].id)) {
            this.tweets.unshift(tweets[i]);
          }
        }
      });
  }

  loadMoreTweets() {
    if (!this.lastdate) return;
    this.feedService.getTweetsBatch(this.lastdate)
      .subscribe((tweets) => {
        for (var i = 0; i < tweets.length; i++) {
          if (this.tweets.every(x => x.id != tweets[i].id)) {
            this.tweets.push(tweets[i]);
          }        
        }
        this.lastdate = tweets.length ? new Date(tweets[tweets.length - 1].sendingDateTime) : undefined;
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

    this.subscribeToData();

  }

  subscribeToData() {
    this.timerSubscription = Observable.timer(2000)
      .first()
      .subscribe(() => this.checkForNewTweets());
  }

  onScroll() {
    console.log("Scroll");
    this.loadMoreTweets();
  }

}
