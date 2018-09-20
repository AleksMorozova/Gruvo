import { Component } from '@angular/core';

@Component({
  selector: 'gr-search-box',
  templateUrl: './search-box.component.html',
  styleUrls: ['./search-box.component.css']
})

export class SearchBoxComponent {
  onFormEnter() {
    console.log('form changed');
  }
}
