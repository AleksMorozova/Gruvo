import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { ITweet } from '@app/tweet/tweet.model';
import { TweetService } from '@app/tweet/tweet.service';
import { error } from 'protractor';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs';

@Component({
  selector: 'gr-tweet',
  templateUrl: './tweet.component.html',
  styleUrls: ['./tweet.component.css']
})

export class TweetComponent implements OnInit, OnDestroy {
  @Input() tweet: ITweet;
  likeImgUrl: string;
  numOfLikes: number;
  timerSubscription: Subscription;

  constructor(private tweetService: TweetService) { }

  ngOnInit() {
    this.checkIfUserLiked();

    this.refreshData();
  }

  ngOnDestroy() {
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }

  like() {
    this.tweetService.like(this.tweet.id)
      .subscribe(object => {
        this.checkIfUserLiked();
        this.refreshData();
      }, error => {
        console.log(error);
      });
  }
  
  public deleteTweet(event) {
    this.tweetService.deleteTweet(this.tweet.id)
      .subscribe(
        deleted => {},
        error => console.log(error)
      );
    event.preventDefault();    
  }
  
  checkIfUserLiked() {
    this.tweetService.checkLiked(this.tweet.id)
      .subscribe((res : boolean) => {
        if (res) {
          this.likeImgUrl = '/assets/images/heart_red.png';
        }
        else {
          this.likeImgUrl = '/assets/images/heart.png';
        }
      });
  }

  refreshData() {
    this.tweetService.getNumOfLikes(this.tweet.id)
      .subscribe((numOfLikes: number) => {
        this.numOfLikes = numOfLikes;
      });

    this.subscribeToData();
  }

  subscribeToData() {
    this.timerSubscription = Observable.timer(10000)
      .first()
      .subscribe(() => this.refreshData());
  }
}
