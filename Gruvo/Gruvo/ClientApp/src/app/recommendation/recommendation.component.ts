import { Component, OnInit, Input } from '@angular/core';
import { IUser } from '@app/profile/user.model';

@Component({
  selector: 'gr-recommendation',
  templateUrl: './recommendation.component.html',
  styleUrls: ['./recommendation.component.css']
})

export class RecommendationComponent {
  @Input() data: IUser;
}
