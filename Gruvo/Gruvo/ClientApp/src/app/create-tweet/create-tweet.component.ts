import { Component, OnInit } from '@angular/core';
import { CreateTweetService } from './create-tweet.service';
import { ITweet } from '../tweet/tweet.model';
import { MessageBundle } from '@angular/compiler';

@Component({
  selector: 'gr-create-tweet',
  templateUrl: './create-tweet.component.html',
  styleUrls: ['./create-tweet.component.css']
})

export class CreateTweetComponent {

  message: string;

  public postTweet(event) {
    this.createTweetService.postTweet(this.message)
      .subscribe(
        tweet => console.log(tweet),
        error => console.log(error)
    );
    event.preventDefault();
    this.message = '';
  }

  isMessageEmpty() {
    return !this.message.trim();
  }

  constructor(private createTweetService: CreateTweetService) {
    this.message = '';
  }
}
