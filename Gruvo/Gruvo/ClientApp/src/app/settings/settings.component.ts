import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { IUser } from "@app/profile/user.model";
import { SettingsService } from '@app/settings/settings.service';

@Component({
  selector: 'gr-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  editForm: FormGroup;
  user: IUser;

  constructor(private _fb: FormBuilder, private _router: Router, private settingsService: SettingsService) {
    this.editForm = this._fb.group({
      'login': ['', [Validators.pattern(/^[a-zA-Z0-9_]{3,50}$/)]],
      'email': ['', [Validators.email]],
      'about': [''],
      'bday': ['']
    });
  }
  ngOnInit() {
    this.settingsService.getUserData()
      .subscribe((user) => {
        this.user = user;
      });
  }

  editInfo(formData: any): void {
    this.settingsService.EditInfo(formData.login, formData.about, formData.bday).subscribe(object => {
      console.log('Info edit completed!');
    }, error => {
      console.log(error);
    });
  }

  isValidControl(controlName): boolean {
    return !this.editForm.controls[controlName].valid && this.editForm.controls[controlName].touched;
  }
}
