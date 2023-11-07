import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService implements OnInit {
  jwtHelperService = new JwtHelperService();
  baseurl = 'https://glitterapi.azurewebsites.net/api/';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  newaccount(user: any) {
    return this.http.post(this.baseurl + 'User/CreateUser', user, {
      responseType: 'text',
    });
  }

  login(user: any) {
    return this.http.post(this.baseurl + 'User/Login', user, {
      responseType: 'text',
    });
  }

  setToken(token: string) {
    localStorage.setItem('access_token', token);
    //this.loadCurrentUser();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('access_token');
    const userInfo =
      token != null ? this.jwtHelperService.decodeToken(token) : null;
    return userInfo;
  }

  isLoggedin(): boolean {
    return localStorage.getItem('access_token') ? true : false;
  }

  removeToken() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('Id');
  }

  newTweet(newTweet: any) {
    return this.http.post(this.baseurl + 'Tweet/newTweet', newTweet, {
      responseType: 'text',
    });
  }

  GetAllTweets(userId:any) {
    return this.http.get(this.baseurl + 'Tweet/allTweet',{
      params:{
        userId:userId
      }
    });
  }

  getAllUsers() {
    return this.http.get(this.baseurl + 'User/getAllUsers');
  }

  DeleteTweet(Tweet: any) {
    return this.http.delete(this.baseurl + 'Tweet/deleteTweet', {
      body: Tweet,
      responseType: 'text',
    });
  }

  EditTweet(tweet: any) {
    return this.http.put(this.baseurl + 'Tweet/editTweet', tweet, {
      responseType: 'text',
    });
  }

  LikeTweet(likeModel:any){
    return this.http.post(this.baseurl + 'Tweet/liketweet', likeModel, {
      responseType: 'text',
    });
  }

  DislikeTweet(dislikeModel:any){
    return this.http.delete(this.baseurl + 'Tweet/disliketweet', {
      body: dislikeModel,
      responseType: 'text',
    });
  }

  image(fileData: any) {
    return this.http.post(this.baseurl + 'User/image', fileData);
  }

  getAllFollowers(Id: any) {
    return this.http.get(this.baseurl + 'User/followers', {
      params: {
        Id: Id,
      },
    });
  }

  getAllFollowing(Id: any) {
    return this.http.get(this.baseurl + 'User/following', {
      params: {
        Id: Id,
      },
    });
  }

  Unfollow(unfollow: any) {
    return this.http.delete(this.baseurl + 'User/unfollow', {
      body: unfollow,
      responseType: 'text',
    });
  }

  Follow(Follow: any) {
    return this.http.post(this.baseurl + 'User/follow', Follow, {
      responseType: 'text',
    });
  }

  SearchPeople(Search: any){
    return this.http.post(this.baseurl + 'search/searchbyuser', Search);
  }

  SearchTag(Search: any){
    return this.http.post(this.baseurl + 'search/searchbytag', Search);
  }

  Analytics(){
    return this.http.get(this.baseurl + 'analytics');
  }
}
