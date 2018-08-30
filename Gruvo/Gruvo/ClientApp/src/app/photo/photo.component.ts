import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'gr-photo-edit',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})

export class PhotoEditComponent {
  editPhotoForm: FormGroup;
  selectedFile: File;
  showMessage: boolean = false;

  constructor(private fb: FormBuilder) {
    this.editPhotoForm = this.fb.group({
      'photoFile': ['', Validators.required]
    });
    
  }

  onFileChanged(event) {
    this.selectedFile = event.target.files[0];
    document.getElementById("file-name").innerHTML = this.selectedFile.name;
    var image = document.getElementById("photo") as HTMLImageElement;
    image.src = URL.createObjectURL(this.selectedFile);
    console.log(event);
  }
  
  editPhoto() {
    console.log('Photo edit completed!');
    console.log(this.selectedFile.name);
  }
}
