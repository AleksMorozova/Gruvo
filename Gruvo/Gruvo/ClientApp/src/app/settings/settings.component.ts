import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { IUserEdit } from "@app/settings/user-edit.model";
import { SettingsService } from '@app/settings/settings.service';

@Component({
  selector: 'gr-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})

export class SettingsComponent implements OnInit {
  editForm: FormGroup;
  user: IUserEdit;
  successfulResult: boolean = false;
  showMessage: boolean = false;

  constructor(private fb: FormBuilder, private settingsService: SettingsService) {
    this.editForm = this.fb.group({
      'login': ['', Validators.compose([Validators.required, Validators.pattern(/^[a-zA-Z0-9_]{3,50}$/)])],
      'email': ['', Validators.compose([Validators.required, Validators.email])],
      'about': [''],
      'bday': ['']
    });
  }

  ngOnInit() {
    this.settingsService.getUserData()
      .subscribe((user) => {
        this.user = user;
        this.setInitialValuesOfControls();
      });
  }

  setInitialValuesOfControls() {
    this.editForm.controls['login'].setValue(this.user.login)
    this.editForm.controls['email'].setValue(this.user.email)
    this.editForm.controls['about'].setValue(this.user.about)
    this.editForm.controls['bday'].setValue(this.user.birthDay)
  }

  editInfo(formData: any) {
    this.settingsService.EditInfo(formData.login, formData.email, formData.about, formData.bday).subscribe(object => {
      this.showMessage = true;
      this.successfulResult = true;
      console.log('Info edit completed!');
    }, error => {
      this.showMessage = true;
      this.successfulResult = false;
      console.log(error);
    });
  }

  isValidControl(controlName): boolean {
    return this.editForm.controls[controlName].valid || this.editForm.controls[controlName].untouched;
  }
}
