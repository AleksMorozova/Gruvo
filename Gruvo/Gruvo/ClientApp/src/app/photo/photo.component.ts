import { Component, OnInit } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { NgForm, FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ProfileService } from '@app/profile/profile.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'gr-photo-edit',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})

export class PhotoEditComponent implements OnInit {
  editPhotoForm: FormGroup;
  selectedFile: File;
  showMessage: boolean = false;
  img: any = true;

  constructor(private fb: FormBuilder, private http: HttpClient, private profileService: ProfileService, private sanitizer: DomSanitizer) {
    this.editPhotoForm = this.fb.group({
      'photoFile': ['', Validators.required]
    });
    
  }

  ngOnInit(): void {
    this.profileService.getPhoto().subscribe(blob => {
      let urlCreator = window.URL;
      this.img = this.sanitizer.bypassSecurityTrustUrl(
        urlCreator.createObjectURL(blob));
    }, () => { this.img = './assets/images/no_avatar_profile.png' });
  }

  onFileChanged(event) {
    this.selectedFile = event.target.files[0];
    document.getElementById("file-name").innerHTML = this.selectedFile.name;
    var image = document.getElementById("photo") as HTMLImageElement;
    image.src = URL.createObjectURL(this.selectedFile);

  }

  submit() {
    let formData = new FormData();
    formData.append("file", this.selectedFile, this.selectedFile.name);
    this.http.post('api/photo/upload', formData).subscribe(() => { });

  }

  editPhoto() {
    console.log('Photo edit completed!');
    console.log(this.selectedFile.name);
  }
}
