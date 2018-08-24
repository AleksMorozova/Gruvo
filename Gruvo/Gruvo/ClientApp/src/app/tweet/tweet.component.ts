import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { ITweet } from '@app/tweet/tweet.model';
import { TweetService } from '@app/tweet/tweet.service';
import { error } from 'protractor';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'gr-tweet',
  templateUrl: './tweet.component.html',
  styleUrls: ['./tweet.component.css']
})

export class TweetComponent implements OnInit {
  @Input() data: ITweet;
  likeImgUrl: string;
  numOfLikes: number;

  constructor(private tweetService: TweetService) {

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
        this.checkIfUserLiked();
        this.getNumOfLikes();
      }, error => {
        console.log(error);
      });
  }
  
  checkIfUserLiked() {
    this.tweetService.checkLiked(this.data.id)
      .subscribe((res : boolean) => {
        if (res) {
          this.likeImgUrl = '/assets/images/heart_red.png';
        }
        else {
          this.likeImgUrl = '/assets/images/heart.png';
        }
      });
  }

}
