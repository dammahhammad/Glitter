import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router, UrlSerializer } from '@angular/router';
import { NgxUiLoaderService } from "ngx-ui-loader";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  displayMsg: string = '';
  isUserValid: boolean = false;

  constructor(private authService: AuthService, private router: Router, private loader: NgxUiLoaderService) {}

  ngOnInit(): void {}

  loginForm = new FormGroup({
    email: new FormControl('', [
      Validators.required,
      Validators.pattern('^[a-zA-Z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.pattern(
        '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{8,}'
      ),
    ]),
  });

  get p() {
    return this.loginForm.controls;
  }

  clearDisplayMessage() {
    setTimeout(() => {
      this.displayMsg = '';
    }, 2000);
  }

  signin() {
    this.loader.start();
    setTimeout(() => {
      this.loader.stop(); // stop foreground spinner of the master loader with 'default' taskId
    }, 2000);
    this.authService.login(this.loginForm.value).subscribe((res) => {
      console.log(this.loginForm.value);
      console.log(res);
      if (res == 'Failure') {
        this.isUserValid = false;
        this.displayMsg = 'Email and password do not match';
        this.clearDisplayMessage();
        this.loginForm.reset();
      } else if (res == 'Email not found') {
        this.isUserValid = false;
        this.displayMsg = 'Invalid Email!';
        this.clearDisplayMessage();
        this.loginForm.reset();
      } else {
        this.isUserValid = true;
        this.authService.setToken(res);
        const userInfo = this.authService.loadCurrentUser();
        localStorage.setItem('Id', userInfo.id);
        this.router.navigate(['playground/' + userInfo.name]);
        console.log('success');
      }
    });
  }
}
