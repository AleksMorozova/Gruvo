import { Component, OnInit } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ProfileService } from '@app/profile/profile.service';

@Component({
  selector: 'gr-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.css']
})

export class SubscriptionsComponent implements OnInit {
  subscriptions: IUser[];
  paramId: number;

  constructor(public modalRef: BsModalRef, private profileService: ProfileService) { }

  ngOnInit() {
    this.getSubscriptions();
  }

  getSubscriptions() {
    this.profileService.getSubscriptions(this.subscriptions ? this.subscriptions[this.subscriptions.length - 1].id : undefined, this.paramId)
      .subscribe((subscriptions) => {
        if (this.subscriptions) {
          this.subscriptions = this.subscriptions.concat(subscriptions);
        }
        else {
          this.subscriptions = subscriptions;
        }
      });
  }
}
