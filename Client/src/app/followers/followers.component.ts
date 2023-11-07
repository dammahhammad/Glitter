import { Component, EventEmitter, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-followers',
  templateUrl: './followers.component.html',
  styleUrls: ['./followers.component.css'],
})
export class FollowersComponent implements OnInit {
  Id: any;
  allFollowers: any;
  followerCount = new EventEmitter<number | 0>();
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.Id = parseInt(localStorage.getItem('Id') || '0');
    this.getAllFollowers(this.Id);
  }

  getAllFollowers(Id: any) {
    this.authService.getAllFollowers(Id).subscribe((res) => {
      this.allFollowers = res;
      this.followerCount.emit(this.allFollowers.length)
    });
  }
}
