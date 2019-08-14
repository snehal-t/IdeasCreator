import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthenticationGuard, MsAdalAngular6Module } from 'microsoft-adal-angular6';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateIdeaComponent } from './create-idea/create-idea.component';
import { ViewIdeasComponent } from './view-ideas/view-ideas.component';
import { MyIdeasComponent } from './my-ideas/my-ideas.component';
import { OpenIdeasComponent } from './open-ideas/open-ideas.component';
import { ApprovedIdeasComponent } from './approved-ideas/approved-ideas.component';
import { RejectedIdeasComponent } from './rejected-ideas/rejected-ideas.component';
import { IdeaListItemComponent } from './idea-list-item/idea-list-item.component';
import { AppGlobal } from './config/appglobal';
import { IdeasapiService } from './services/ideasapi.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { User, SignInResponse, Response, IdeasResponse, WatchersResponse, CommentsResponse, AlertsResponse, IdeaResponse } from './idea';
import { HttpErrorInterceptor } from './ErrorHandler/http-error-interceptor';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    CreateIdeaComponent,
    ViewIdeasComponent,
    MyIdeasComponent,
    OpenIdeasComponent,
    ApprovedIdeasComponent,
    RejectedIdeasComponent,
    IdeaListItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    MsAdalAngular6Module.forRoot({
      tenant: '21212548-dd86-4f27-a1fa-faf16eedb7c3',
      clientId: '520e43d8-b87b-457d-8bd6-e5fa53494354',
      redirectUri: window.location.origin,
      endpoints: {
        "https://172.23.231.149/api/values": ""
      },
      navigateToLoginRequestUrl: false,
      cacheLocation: 'localStorage',
    })
  ],
  providers: [AuthenticationGuard, AppGlobal, IdeasapiService, User, SignInResponse, Response, IdeasResponse, WatchersResponse, CommentsResponse, AlertsResponse, IdeaResponse,  {
    provide: HTTP_INTERCEPTORS,
    useClass: HttpErrorInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
