import { Component, OnInit, Input, OnDestroy, Output, EventEmitter } from '@angular/core';
import { ITweet } from '@app/tweet/tweet.model';
import { TweetService } from '@app/tweet/tweet.service';
import { error } from 'protractor';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { CommentsComponent } from '@app/comments/comments.component';
import { ProfileService } from '@app/profile/profile.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'gr-tweet',
  templateUrl: './tweet.component.html',
  styleUrls: ['./tweet.component.css']
})

export class TweetComponent implements OnInit, OnDestroy {
  @Input() tweet: ITweet;
  @Input() showComments: boolean = true;
  @Output() deleted: EventEmitter<any> = new EventEmitter();

  likeImgUrl: string;
  numOfLikes: number;
  timerSubscription: Subscription;
  modalRef: BsModalRef;
  isDeleted: boolean = false;
  img : any;
  
  constructor(private tweetService: TweetService, private modalService: BsModalService, private profileService: ProfileService, private sanitizer: DomSanitizer) { }


  ngOnInit() {
    this.checkIfUserLiked();
    this.profileService.getPhoto(this.tweet.userId).subscribe(blob => {
      let urlCreator = window.URL;
      this.img = this.sanitizer.bypassSecurityTrustUrl(
        urlCreator.createObjectURL(blob));
    }, () => { this.img = './assets/images/no_avatar_profile.png' });

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
        () => {
          this.isDeleted = true;
          this.deleted.emit();
        },
        () => {
          this.isDeleted = true;
          this.deleted.emit();
        }
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
      .subscribe(() => {this.refreshData();this.checkIfUserLiked();});
  }

  openCommentsModal() {
   
    const initialState = {
      paramId: this.tweet.id,
      tweetComp: this,
      class: 'modal-sm'
    };

    this.modalRef = this.modalService.show(CommentsComponent, { initialState });
  }
}
