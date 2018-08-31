import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'gr-scroll-to-top',
  templateUrl: './scroll-to-top.component.html',
  styleUrls: ['./scroll-to-top.component.css']
})
export class ScrollToTopComponent {
  scrollToTop(){ 
     window.scrollTo(0, 0);
  }
 }

