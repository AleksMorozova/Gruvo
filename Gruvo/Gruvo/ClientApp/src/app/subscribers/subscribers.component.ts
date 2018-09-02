import { Component, OnInit } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

@Component({
  selector: 'gr-subscribers',
  templateUrl: './subscribers.component.html',
  styleUrls: ['./subscribers.component.css']
})

export class SubscribersComponent {
  constructor(public modalRef: BsModalRef) {}
}
