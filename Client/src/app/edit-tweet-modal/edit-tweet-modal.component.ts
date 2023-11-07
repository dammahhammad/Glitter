import { Component, OnInit, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuthService } from '../services/auth.service';
import { PlaygroundComponent } from '../playground/playground.component';

@Component({
  selector: 'app-edit-tweet-modal',
  templateUrl: './edit-tweet-modal.component.html',
  styleUrls: ['./edit-tweet-modal.component.css'],
})
export class EditTweetModalComponent implements OnInit {
  tweet: any = {};
  editMessage: FormGroup = new FormGroup({
    message: new FormControl('', [Validators.required]),
  });

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private authService: AuthService
  ) {
    this.tweet = data;
    this.editMessage = new FormGroup({
      message: new FormControl(this.tweet.message, [Validators.required]),
    });
  }

  ngOnInit(): void {}

  submit() {
    this.tweet.message = this.editMessage.value?.message;
    console.log(this.tweet);
    this.authService.EditTweet(this.tweet).subscribe((res) => {
      console.log(res);
    });
  }
}
