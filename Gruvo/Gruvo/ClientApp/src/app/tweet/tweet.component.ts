import { Component, OnInit, Input } from '@angular/core';
import { ITweet } from '@app/tweet/tweet.model';
import { TweetService } from '@app/tweet/tweet.service';
import { error } from 'protractor';

@Component({
  selector: 'gr-tweet',
  templateUrl: './tweet.component.html',
  styleUrls: ['./tweet.component.css']
})

export class TweetComponent implements OnInit {
  @Input() data: ITweet;
  likeUrl: string;
  activeLikeUrl: string;
  isLiked: boolean;
  numOfLikes: number;

  constructor(private tweetService: TweetService) {
    this.likeUrl = '/assets/images/heart.png';
    this.activeLikeUrl = '/assets/images/heart_red.png';
    this.isLiked = false;
  }

  ngOnInit() {
    this.checkIfUserLiked();
    this.getNumOfLikes();
  }

  getNumOfLikes() {
    this.tweetService.getNumOfLikes(this.data.id)
      .subscribe((numOfLikes: number) => {
        this.numOfLikes = numOfLikes;
      });
  }

  like() {
    this.tweetService.like(this.data.id)
      .subscribe(object => {

      }, error => {
        console.log(error);
      });
  }
  
  checkIfUserLiked() {
    this.tweetService.checkLiked(this.data.id)
      .subscribe(res => {
        this.isLiked = res;
      });
  }
}
