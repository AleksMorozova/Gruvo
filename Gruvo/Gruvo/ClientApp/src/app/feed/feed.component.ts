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
  newTweets: boolean = false;

  constructor(private feedService: FeedService) {
  }

  ngOnInit() {
    this.loadMoreTweets();

    this.feedService.getRecommendations()
      .subscribe((recommendations) => {
        this.recommendations = recommendations;
      });


    this.subscribeToData();
  }

  ngOnDestroy() {
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }

  checkForNewTweets() {
    this.feedService.getTweetsBatch(new Date())
      .subscribe((tweets) => {
        for (var i = tweets.length - 1; i >= 0; i--) {
          if (this.tweets.every(x => x.id != tweets[i].id)) {
            this.tweets.unshift(tweets[i]);
          }
        }
      });
    this.subscribeToData();
  }

  loadMoreTweets() {
    if (this.lastdate) { 
      this.newTweets=true;  
      this.feedService.getTweetsBatch(this.lastdate)
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

  subscribeToData() {
    this.timerSubscription = Observable.timer(7000)
      .first()
      .subscribe(() => this.checkForNewTweets());
  }

  onScroll() {
    console.log("Scrolled");
    this.loadMoreTweets();
  }

  onTweet(){
    this.feedService.getTweetsBatch(new Date())
    .subscribe((tweets) => {
      for (var i = tweets.length - 1; i >= 0; i--) {
        if (this.tweets.every(x => x.id != tweets[i].id)) {
          this.tweets.unshift(tweets[i]);
        }
      }
    });

  }
}
