import { Component } from '@angular/core';
import { ITweet } from '@app/tweet/tweet.model';
import { IUser } from '@app/profile/user.model';
import { FeedService } from '@app/feed/feed.service';
import { Observable } from 'rxjs/Rx';

@Component({
  selector: 'gr-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})

export class FeedComponent {
  tweets: ITweet[] = [];
  recommendations: IUser[] = [];

  constructor(private feedService: FeedService) {

    this.feedService.getTweets()
      .subscribe((tweets) => {
        this.tweets = tweets;
      });

    this.feedService.getRecommendations()
      .subscribe((recommendations) => {
        this.recommendations = recommendations;
      });
  }

  openNav() {
    console.log("Openning sidenav");
    var elem = document.getElementById("sidenav")
    x.style.width = "100%";
    x.style.margin = "0px";
    x.style.left = "0px";
    x.style.borderRadius = "0px";
  } 

  closeNav() {
    var elem = document.getElementById("sidenav")
    x.style.width = "0px";
  }
}
