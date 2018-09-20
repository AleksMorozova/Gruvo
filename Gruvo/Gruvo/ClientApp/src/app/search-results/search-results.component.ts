import { Component, OnInit } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { SearchService } from '@app/search-box/search.service'; 

@Component({
  selector: 'gr-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css']
})

export class SearchResultsComponent implements OnInit {
  users: IUser[];
  login: string;

  constructor(public modalRef: BsModalRef, private searchService: SearchService) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.searchService.getUsers(this.login, this.users ? this.users[this.users.length - 1].id : undefined)
      .subscribe((users) => {
        if (this.users) {
          this.users = this.users.concat(users);
        }
        else {
          this.users = users;
        }
      });
  }
}
