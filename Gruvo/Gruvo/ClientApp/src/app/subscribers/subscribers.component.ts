import { Component, OnInit } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

@Component({
  selector: 'gr-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.css']
})

export class SubscribersComponent {
  constructor(public modalRef: BsModalRef) {}
}
