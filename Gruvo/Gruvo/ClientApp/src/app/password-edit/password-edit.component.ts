import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import * as crypto from "crypto-js";
import { SettingsService } from '@app/settings/settings.service';

@Component({
  selector: 'gr-password-edit',
  templateUrl: './password-edit.component.html',
  styleUrls: ['./password-edit.component.css']
})

export class PasswordEditComponent {
  editPasswordForm: FormGroup;
  correctPassword: boolean = true;
  showMessage: boolean = false;

  constructor(private fb: FormBuilder, private settingsService: SettingsService) {
    this.editPasswordForm = this.fb.group({
      'oldPassw': ['', Validators.required],
      'newPassw': ['', Validators.required]
    });
  }
  
  isValidControl(controlName): boolean {
    return this.editPasswordForm.controls[controlName].valid || this.editPasswordForm.controls[controlName].untouched;
  }

  editPassword(formData: any) {
    this.settingsService.EditPassword(crypto.MD5(formData.oldPassw).toString(), crypto.MD5(formData.newPassw).toString()).subscribe(object => {
      this.showMessage = true;
      this.correctPassword = true;
      console.log('Password edit completed!');
    }, error => {
      this.showMessage = true;
      this.correctPassword = false;
      console.log(error);
    })
  }
}
