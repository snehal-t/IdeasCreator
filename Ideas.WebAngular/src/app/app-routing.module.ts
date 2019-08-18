import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthenticationGuard } from 'microsoft-adal-angular6';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateIdeaComponent } from './create-idea/create-idea.component';
import { ViewIdeasComponent } from './view-ideas/view-ideas.component';
import { MyIdeasComponent } from './my-ideas/my-ideas.component';
import { OpenIdeasComponent } from './open-ideas/open-ideas.component';
import { ApprovedIdeasComponent } from './approved-ideas/approved-ideas.component';
import { RejectedIdeasComponent } from './rejected-ideas/rejected-ideas.component';
import { ListIdeasComponent } from './list-ideas/list-ideas.component';

const routes: Routes = [
  { path: 'Dashboard', component: DashboardComponent, canActivate: [AuthenticationGuard] },
  { path: 'CreateIdea', component: CreateIdeaComponent, canActivate: [AuthenticationGuard] },
  { path: 'ViewIdeas', component: ListIdeasComponent, canActivate: [AuthenticationGuard] },
  { path: 'MyIdeas', component: MyIdeasComponent, canActivate: [AuthenticationGuard] },
  { path: 'OpenIdeas', component: OpenIdeasComponent, canActivate: [AuthenticationGuard] },
  { path: 'ApprovedIdeas', component: ApprovedIdeasComponent, canActivate: [AuthenticationGuard] },
  { path: 'RejectedIdeas', component: RejectedIdeasComponent, canActivate: [AuthenticationGuard] },
  { path: '**', component: DashboardComponent, pathMatch: 'full', canActivate: [AuthenticationGuard] }
];
@NgModule({
  imports: [
    RouterModule.forRoot(routes),
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
