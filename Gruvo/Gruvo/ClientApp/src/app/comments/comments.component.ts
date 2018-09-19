import { Component, OnInit, Input } from '@angular/core';
import { TweetService } from '@app/tweet/tweet.service';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { IComment } from '@app/comments/comment.model';
import { ITweet } from '@app/tweet/tweet.model';
import { TweetComponent } from '@app/tweet/tweet.component';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {
  comments: IComment[];
  paramId: number;
  tweetComp: TweetComponent;
  message: string;

  constructor(public modalRef: BsModalRef,private tweetService: TweetService) { }

  ngOnInit() {
    this.getComments();    
  }

  getComments() {
    this.tweetService.getComments(this.paramId)
      .subscribe((comments) => {
        this.comments = comments;       
      });
  }

  addComment() {
    this.tweetService.addComment(this.tweetComp.tweet.id, this.message).subscribe(_ => {
      this.message = '';
      this.getComments();
    });
  }

  deleteComment(comment: IComment) {
    this.tweetService.deleteComment(comment.commentId).subscribe(_ => {
      this.message = '';
      this.getComments();
    });
  }

  onDeleted(){
    this.tweetComp.isDeleted=true;
    this.modalRef.hide();
  }
}
