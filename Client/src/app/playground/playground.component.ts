import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { EditTweetModalComponent } from '../edit-tweet-modal/edit-tweet-modal.component';

@Component({
  selector: 'app-playground',
  templateUrl: './playground.component.html',
  styleUrls: ['./playground.component.css'],
})
export class PlaygroundComponent implements OnInit {
  openinput: boolean = false;
  closebutton: boolean = true;
  userInfo: any;
  allTweets: any;
  loggedIn: any;

  newPost = new FormGroup({
    Message: new FormControl('', [Validators.required]),
    UserId: new FormControl(),
    Name: new FormControl(''),
  });

  constructor(private authService: AuthService, public dialog: MatDialog) {
    
  }

  openDialog(tweet: any) {
    this.dialog.open(EditTweetModalComponent, {
      width: '500px',
      data: tweet,
    });
  }

  ngOnInit(): void {
    this.userInfo = this.authService.loadCurrentUser();
    this.getAllTweets(parseInt(this.userInfo.id));
    this.loggedIn = this.authService.isLoggedin();
  }

  getAllTweets(userId:any) {
    this.authService.GetAllTweets(userId).subscribe((data) => {
      this.allTweets = data;
      this.allTweets = this.allTweets.reverse();
      console.log(this.allTweets);
    });
  }

  savePost() {
    this.newPost.value.UserId = parseInt(this.userInfo.id);
    this.newPost.value.Name = this.userInfo.name;
    this.authService.newTweet(this.newPost.value).subscribe((res) => {
      this.newPost.reset({});
      this.closeTweet();
      this.getAllTweets(parseInt(this.userInfo.id));
    });
  }

  openTweet() {
    this.openinput = true;
    this.closebutton = false;
  }
  closeTweet() {
    this.openinput = false;
    this.closebutton = true;
  }

  deleteTweet(tweet: any) {
    this.authService.DeleteTweet(tweet).subscribe((res) => {
      this.getAllTweets(parseInt(this.userInfo.id));
    });
  }

  liketweet(tweetId:any){
    const likeModel = { UserId: parseInt(this.userInfo.id), TweetId: tweetId };
    this.authService.LikeTweet(likeModel).subscribe((res) => {
      this.getAllTweets(parseInt(this.userInfo.id));
    });
  }

  disliketweet(tweetId:any){
    const dislikeModel = { UserId: parseInt(this.userInfo.id), TweetId: tweetId };
    this.authService.DislikeTweet(dislikeModel).subscribe((res) => {
      this.getAllTweets(parseInt(this.userInfo.id));
    });
  }
}
