import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import * as crypto from "crypto-js";
import { IUser } from "@app/profile/user.model";
import { SettingsService } from '@app/settings/settings.service';

@Component({
  selector: 'gr-password-edit',
  templateUrl: './password-edit.component.html',
  styleUrls: ['./password-edit.component.css']
})
export class PasswordEditComponent {
  editPasswordForm: FormGroup;
  correctPassw: boolean = true;

  constructor(private _fb: FormBuilder, private _router: Router, private settingsService: SettingsService) {
    this.editPasswordForm = this._fb.group({
      'oldPassw': ['', Validators.required],
      'newPassw': ['']
    });
  }
  
  isValidControl(controlName): boolean {
    return !this.editPasswordForm.controls[controlName].valid && this.editPasswordForm.controls[controlName].touched;
  }

  editPassw(formData: any): void {
    this.settingsService.EditPassword(crypto.MD5(formData.oldPassw).toString(), crypto.MD5(formData.newPassw).toString()).subscribe(object => {
      this.correctPassw = true;
      console.log('Password edit completed!');
    }, error => {
      this.correctPassw = false;
      console.log(error);
    })
  }
}
