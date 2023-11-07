import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-analytics',
  templateUrl: './analytics.component.html',
  styleUrls: ['./analytics.component.css']
})
export class AnalyticsComponent implements OnInit {


  constructor(private authService: AuthService) { }
  public analytics :any;
  ngOnInit(): void {
    this.authService.Analytics().subscribe((res) => {
      console.log(res);
      this.analytics = res;
    })
  }

}
