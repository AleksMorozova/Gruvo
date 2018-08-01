import { Component, OnInit } from '@angular/core';
import { IUser } from './user.model';
import { ProfileService } from './profile.service';
import { ITweet } from '../tweet/tweet.model';

@Component({
  selector: 'gr-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent  {
  user: IUser = this.profileService.getUserData();
  tweets: ITweet[] = [    
      { id: 0, userLogin: 'Tarasoff', message: 'I watched “How It Ends” on Netflix late last night / for about an hour it’s enjoyably bad and kinda feels like “The Purge On a Highway,” and then something flips and it becomes flat-out, indefensibly and inexplicably bad. It’s really bad. It’s bad. It’s not good.', date: new Date(2018, 6, 5)},
      { id: 1, userLogin: 'Tarasoff', message: 'I was planning to organize my spice rack, but I ran out of thyme', date: new Date(2018, 6, 6) },
      { id: 2, userLogin: 'Tarasoff', message: 'What did the Buffalo say to his kid when he dropped him off at school?   Bi-son', date: new Date(2018, 6, 7) },
      { id: 3, userLogin: 'Tarasoff', message: "Why did the blind man fall in the well? Because he couldn't see that well.", date: new Date(2018, 7, 8) },
      { id: 4, userLogin: 'Tarasoff', message: 'How many tickles does it take to make an octopus laugh? Ten tickles.', date: new Date(2018, 7, 9) },
  ];

  
  constructor(private profileService: ProfileService) {  }
}
