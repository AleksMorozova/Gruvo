import { Component, OnInit, Input } from '@angular/core';
import { ITweet } from '@app/tweet/tweet.model';
import { TweetService } from './tweet.service';

@Component({
  selector: 'gr-tweet',
  templateUrl: './tweet.component.html',
  styleUrls: ['./tweet.component.css']
})
export class TweetComponent  {
  @Input() data: ITweet;

  public deleteTweet(event) {
    this.tweetService.deleteTweet(this.data.id)
      .subscribe(
        error => console.log(error)
      );
    event.preventDefault();    
  }

  constructor(private tweetService: TweetService) {
    
  }
}
