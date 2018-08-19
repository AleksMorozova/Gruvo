import { Component, OnInit, Input } from '@angular/core';
import { FeedService } from '@app/feed/feed.service';
import { IUser } from '@app/profile/user.model';

@Component({
  selector: 'gr-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  recommendations: IUser[] = [];
  mobileIcon: HTMLElement;
  sidenav: HTMLElement;
  background: HTMLElement;
  isOpened: boolean = false;

  constructor(private feedService: FeedService) {
    this.feedService.getRecommendations()
      .subscribe((recommendations) => {
        this.recommendations = recommendations;
      });
  }

  ngOnInit(): void {
    this.mobileIcon = document.getElementById("mobile-menu-icon");
    this.sidenav = document.getElementById("sidenav");
    this.background = document.getElementById("bg");

    this.mobileIcon.classList.add("show");
    this.background.classList.add("hidden");
  }

  moveSidebar() {
    if (this.isOpened) {
      this.sidenav.style.left = "-300px";
      this.mobileIcon.classList.replace("glyphicon-chevron-left", "glyphicon-align-justify");
      this.background.classList.replace("show", "hidden");      
    } else {
      this.sidenav.style.left = "0px";
      this.mobileIcon.classList.replace("glyphicon-align-justify", "glyphicon-chevron-left");
      this.background.classList.replace("hidden", "show");
    }
    this.isOpened = !this.isOpened;
  }
}


