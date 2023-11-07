import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { SearchComponent } from '../search/search.component';
import { FollowersComponent } from '../followers/followers.component';
import { FollowingComponent } from '../following/following.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  Followers: any;
  Following: any;
  constructor(
    private authService: AuthService, 
    private router: Router, 
    private search1: SearchComponent,
    private followercount: FollowersComponent,
    private followingcount: FollowingComponent) {}
    userInfo = this.authService.loadCurrentUser();

  ngOnInit(): void {
    const userId = parseInt(localStorage.getItem('Id') || '0');

    this.search1.followingCount.subscribe((items)=> {
      this.Following = items
    })

    this.followercount.followerCount.subscribe((items) =>{
      this.Followers = items
    })

    this.followingcount.followingCount.subscribe((items) =>{
      this.Following = items
    })

    this.authService.getAllFollowing(userId).subscribe((data: any) => {
      this.Following = data.length
    });

    this.authService.getAllFollowers(userId).subscribe((data: any) => {
      this.Followers = data.length;
    });
  }

  logout() {
    this.authService.removeToken();
    this.router.navigate(['']);
  }

  followers() {
    this.router.navigate(['followers/' + this.userInfo.name]);
  }
  following() {
    this.router.navigate(['following/' + this.userInfo.name]);
  }
  playgorund() {
    this.router.navigate(['playground/' + this.userInfo.name]);
  }

  search() {
    this.router.navigate(['search/' + this.userInfo.name]);
  }
  analytics(){
    this.router.navigate(['analytics/' + this.userInfo.name]);
  }
}
