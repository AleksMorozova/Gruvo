import { Component, OnInit } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ProfileService } from '@app/profile/profile.service';

@Component({
  selector: 'gr-subscribers',
  templateUrl: './subscribers.component.html',
  styleUrls: ['./subscribers.component.css']
})

export class SubscribersComponent implements OnInit {
  subscribers: IUser[];
  paramId: number;

  constructor(public modalRef: BsModalRef, private profileService: ProfileService) { }

  ngOnInit() {
    this.getSubscribers();
  }

  getSubscribers() {
    this.profileService.getSubscribers(this.subscribers ? this.subscribers[this.subscribers.length - 1].id : undefined, this.paramId)
      .subscribe((followers) => {
        if (this.subscribers) {
          this.subscribers = this.subscribers.concat(followers);
        }
        else {
          this.subscribers = followers;
        }
      });
  }
}
