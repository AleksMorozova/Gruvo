import { Component } from '@angular/core';
import { ITweet } from '../tweet/tweet.model';
import { FeedService } from './feed.service';
import { Observable } from 'rxjs/Rx';

@Component({
  selector: 'gr-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})

export class FeedComponent {
  tweets: ITweet[] = new Array(100);

  constructor(private feedService: FeedService) {
    this.feedService.getTweets()
      .subscribe((tweets) => {
        this.tweets.concat(tweets);
        console.log(this.tweets);
      });
  }
}
