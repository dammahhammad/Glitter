import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { countries } from '../shared/components/country-data-store';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { NgxUiLoaderService } from 'ngx-ui-loader';

@Component({
  selector: 'app-newaccount',
  templateUrl: './newaccount.component.html',
  styleUrls: ['./newaccount.component.css'],
})
export class NewaccountComponent implements OnInit {

  constructor(private authServices: AuthService, private http: HttpClient, private loader:NgxUiLoaderService) {}
  ngOnInit(): void {}


  public country = countries;
  accountcreated: boolean = false;
  alreadyexist: boolean = false;
  imageUrl = '';
  selectedFile: File | any = null;
  displayMsg: string = '';
  

  newaccount = new FormGroup({
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [
      Validators.required,
      Validators.pattern('^[a-zA-Z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
    ]),
    image: new FormControl(),
    contact: new FormControl('', [
      Validators.required,
      Validators.pattern('^((\\+91-?)|0)?[0-9]{10}$'),
    ]),
    country: new FormControl('', [Validators.required]),
    password: new FormControl('', [
      Validators.required,
      Validators.pattern(
        '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{8,}'
      ),
    ]),
  });

  handleFileInput(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      const file = <File>event.target.files[0];
      this.selectedFile = file;
    } else {
      this.selectedFile = null;
    }
  }

  get p() {
    return this.newaccount.controls;
  }
  
  clear() {
    this.newaccount.reset({});
  }

  clearDisplayMessage() {
    setTimeout(() => {
      this.displayMsg = '';
    }, 3000);
  }

  newuser() {
    this.loader.start();
    setTimeout(() => {
      this.loader.stop(); // stop foreground spinner of the master loader with 'default' taskId
    }, 4000);
    const fileData = new FormData();
    fileData.append('image', this.selectedFile, this.selectedFile.name);
    this.http
      .post('http://localhost:44347/api/User/image', fileData, {
        responseType: 'text',
      })
      .subscribe((res) => {
        this.newaccount.value.image = res;
        this.authServices.newaccount(this.newaccount.value).subscribe((res) => {
          console.log(this.newaccount.value);
          if (res == 'Email Already Exists!') {
            this.alreadyexist = true;
            this.displayMsg = "Email Already Exists!";
            this.clearDisplayMessage();
          } else if (res == 'Success') {
            this.accountcreated = true;
            this.displayMsg = "Account Created Successfully!";
            this.clearDisplayMessage();
            this.clear();
          }
          console.log(res);
        });
      });

    // this.authServices.newaccount(this.newaccount.value).subscribe((res) => {
    //   console.log(this.newaccount.value);
    //   if (res == 'Email Already Exists!') {
    //     this.alreadyexist = true;
    //   } else if (res == 'Success') {
    //     this.accountcreated = true;
    //     this.clear();
    //   }
    //   console.log(res);
    // });
  }

}
