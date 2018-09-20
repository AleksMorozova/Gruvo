import { Component } from '@angular/core';
import { IUser } from '@app/profile/user.model';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { SearchService } from '@app/search-box/search.service';
import { SearchResultsComponent } from '@app/search-results/search-results.component';

@Component({
  selector: 'gr-search-box',
  templateUrl: './search-box.component.html',
  styleUrls: ['./search-box.component.css']
})

export class SearchBoxComponent {
  modalRef: BsModalRef;

  constructor(private searchService: SearchService, private modalService: BsModalService) { }

  openSearchResultsModal(value: string) {

    const initialState = {
      login: value,
      class: 'modal-sm'
    };

    this.modalRef = this.modalService.show(SearchResultsComponent, { initialState });
  }
}
