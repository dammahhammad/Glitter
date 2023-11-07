import { Component, EventEmitter, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-following',
  templateUrl: './following.component.html',
  styleUrls: ['./following.component.css'],
})
export class FollowingComponent implements OnInit {
  userId: any;
  allFollowing: any;
  followingCount = new EventEmitter<number | 0>();
  unfollowModel = { FollowerID: 0, UserToFollowId: 0 };
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.userId = parseInt(localStorage.getItem('Id') || '0');
    this.getAllFollowing(this.userId);
  }

  getAllFollowing(Id: any) {
    this.authService.getAllFollowing(Id).subscribe((res) => {
      this.allFollowing = res;
      this.followingCount.emit(this.allFollowing.length)
    });
  }

  Unfollow(followerId: any) {
    this.unfollowModel.FollowerID = this.userId;
    this.unfollowModel.UserToFollowId = followerId;
    this.authService.Unfollow(this.unfollowModel).subscribe((res) => {
      console.log(res);
      this.getAllFollowing(this.userId);
    });
  }
}
