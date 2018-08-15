import { Component, OnInit, Input } from '@angular/core';
import { ITweet } from '@app/tweet/tweet.model';

@Component({
  selector: 'gr-tweet',
  templateUrl: './tweet.component.html',
  styleUrls: ['./tweet.component.css']
})
export class TweetComponent  {
  @Input() data: ITweet;
}
