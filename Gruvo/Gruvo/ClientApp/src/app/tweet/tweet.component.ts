import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { ITweet } from '@app/tweet/tweet.model';
import { TweetService } from '@app/tweet/tweet.service';
import { error } from 'protractor';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { CommentsComponent } from '@app/comments/comments.component';

@Component({
  selector: 'gr-tweet',
  templateUrl: './tweet.component.html',
  styleUrls: ['./tweet.component.css']
})

export class TweetComponent implements OnInit, OnDestroy {
  @Input() tweet: ITweet;
  @Input() showComments: boolean = true;
  numOfLikes: number;
  timerSubscription: Subscription;
  modalRef: BsModalRef;
  isLikedByUser: boolean = true;

  constructor(private tweetService: TweetService, private modalService: BsModalService) {

  }


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
  
  deleteTweet(event) {
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
          this.isLikedByUser = true;
        }
        else {
          this.isLikedByUser = false;
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

  openCommentsModal() {
    let tweet = this.tweet;
    
    const initialState = {
      paramId: this.tweet.id,
      tweet: this.tweet,
      class: 'modal-sm'
    };

    this.modalRef = this.modalService.show(CommentsComponent, { initialState });
  }
}
