import { Component, OnInit } from '@angular/core';
import { CreateTweetService } from './createtweet.service';
import { ITweet } from '../tweet/tweet.model';

@Component({
  selector: 'gr-createtweet',
  templateUrl: './createtweet.component.html',
  styleUrls: ['./createtweet.component.css']
})

export class CreateTweetComponent {

  message = '';

  public postTweet() {
    this.createTweetService.postTweet(this.message)
      .subscribe(
        error => console.log(error)
      );
   }

  constructor(private createTweetService: CreateTweetService) {
   

  }
}
