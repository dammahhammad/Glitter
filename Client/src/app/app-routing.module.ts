import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NewaccountComponent } from './newaccount/newaccount.component';
import { PlaygroundComponent } from './playground/playground.component';
import { FollowersComponent } from './followers/followers.component';
import { FollowingComponent } from './following/following.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { SearchComponent } from './search/search.component';
import { AuthGuard } from './services/auth.guard';
import { AnalyticsComponent } from './analytics/analytics.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'newaccount',
    component: NewaccountComponent,
  },
  {
    path: 'playground/:name',
    component: PlaygroundComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'followers/:name',
    component: FollowersComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'following/:name',
    component: FollowingComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'search/:name',
    component: SearchComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'analytics/:name',
    component: AnalyticsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: '**',
    component: PageNotFoundComponent,
    // Should be the last component as ** will allow any link to reroute
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
