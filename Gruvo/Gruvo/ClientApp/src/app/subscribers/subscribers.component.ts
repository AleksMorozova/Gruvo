import { Component, OnInit } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ProfileService } from '@app/profile/profile.service';

@Component({
  selector: 'gr-subscribers',
  templateUrl: './subscribers.component.html',
  styleUrls: ['./subscribers.component.css']
})

export class SubscribersComponent {
  subscribers: IUser[];
  paramId: number;

  constructor(public modalRef: BsModalRef, private profileService: ProfileService) { }

  getSubscribers() {
    this.profileService.getSubscribers(this.paramId)
      .subscribe((followers) => {
        this.subscribers = followers;
      });
  }
}
