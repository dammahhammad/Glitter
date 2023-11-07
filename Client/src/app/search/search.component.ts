import { Component, EventEmitter, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { MatTabsModule } from '@angular/material/tabs';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
})
export class SearchComponent implements OnInit {
  followingCount = new EventEmitter<number | 0>();
  userId: any;
  filteredUsers: any;
  allFollowing: any;
  allTweets: any;
  selectTab = "Posts";
  followModel = { FollowerID: 0, UserToFollowId: 0 };
  searchModel = { UserId: 0, SearchString: "" };
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.userId = parseInt(localStorage.getItem('Id') || '0');
    this.getAllFollowing();
  }

  getAllFollowing() {
    this.authService.getAllFollowing(this.userId).subscribe((res) => {
      this.allFollowing = res;
      this.followingCount.emit(this.allFollowing.length)
    });
  }

  follow(followerId: any) {
    this.followModel.FollowerID = this.userId;
    this.followModel.UserToFollowId = followerId;
    this.authService.Follow(this.followModel).subscribe((res) => {
      this.getAllFollowing();
    });
  }

  isUserFollowed(userId: any) {
    if (
      this.allFollowing.some((follower: { id: any }) => follower.id == userId)
    ) {
      return true;
    } else if (userId == this.userId) {
      return true;
    }
    return false;
  }

  selectedTab(tabLabel: string) {
    this.selectTab = tabLabel;
    console.log(tabLabel);
    // You can also add additional logic if needed
}

  performSearch(filterValue: string) {
    if (this.selectTab === 'Posts') {
      this.searchPosts(filterValue);
    } else if (this.selectTab === 'People') {
      this.searchPeople(filterValue);
    }
  }

  onTabChange(event: any) {
    this.selectedTab = event.tab.textLabel;
  }

  searchPeople(text:string){
    if(text == ""){return }
    this.searchModel.UserId = this.userId
    this.searchModel.SearchString = text;
    this.authService.SearchPeople(this.searchModel).subscribe((res) => {
      this.filteredUsers = res;
    })
  }

  searchPosts(text: string) {
    if(text == ""){return }
    this.searchModel.UserId = this.userId
    this.searchModel.SearchString = '#'+text;
    this.authService.SearchTag(this.searchModel).subscribe((res) => {
      this.allTweets = res;
    })
  }
}
