import { Injectable } from '@angular/core';
import { Request, Response, Idea, User, SignInResponse, IdeasResponse, WatchersResponse, CommentsResponse, AlertsResponse, IdeaResponse } from '../idea';
import { HttpClient, HttpHandler, HttpHeaders, HttpParams, HttpClientModule } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { AppGlobal } from '../config/appglobal';

@Injectable({
  providedIn: 'root'
})
export class IdeasapiService {
  headers: HttpHeaders = new HttpHeaders();

  constructor(private http: HttpClient, private handler: HttpHandler, private appGlobal: AppGlobal) {
    this.headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Authorization', 'bearer ' + sessionStorage.getItem("accessToken"));
  }

  SignIn(): Observable<SignInResponse> {
    return this.http.post<SignInResponse>(this.appGlobal.SignIn, new HttpParams(), { headers: this.headers });
  }

  CreateIdea(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.CreateIdea, JSON.stringify(request), { headers: this.headers });
  }

  UpdateIdea(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.put<Response>(this.appGlobal.UpdateIdea, JSON.stringify(request), { headers: this.headers });
  }

  WithdrawIdea(request: Request, comments: string): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.WithdrawIdea, JSON.stringify(request), { headers: this.headers });
  }

  ApproveIdea(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.ApproveIdea, JSON.stringify(request), { headers: this.headers });
  }

  RejectIdea(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.RejectIdea, JSON.stringify(request), { headers: this.headers });
  }

  DeligateIdea(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.DeligateIdea, JSON.stringify(request), { headers: this.headers });
  }

  PickIdea(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.PickIdea, JSON.stringify(request), { headers: this.headers });
  }

  PickIdeaDone(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.PickIdeaDone, JSON.stringify(request), { headers: this.headers });
  }

  PickIdeaGiveUp(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.PickIdeaGiveUp, JSON.stringify(request), { headers: this.headers });
  }

  PickIdeaAccept(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.PickIdeaAccept, JSON.stringify(request), { headers: this.headers });
  }

  PickIdeaReopen(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.PickIdeaReopen, JSON.stringify(request), { headers: this.headers });
  }

  WatchIdea(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.WatchIdea, JSON.stringify(request), { headers: this.headers });
  }

  CommentIdea(request: Request): Observable<Response> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<Response>(this.appGlobal.CommentIdea, JSON.stringify(request), { headers: this.headers });
  }

  GetIdeas(request: Request): Observable<IdeasResponse> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<IdeasResponse>(this.appGlobal.GetIdeas, JSON.stringify(request), { headers: this.headers });
  }

  GetIdeaWatchers(request: Request): Observable<WatchersResponse> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<WatchersResponse>(this.appGlobal.GetIdeaWatchers, JSON.stringify(request), { headers: this.headers });
  }

  GetIdeaComments(request: Request): Observable<CommentsResponse> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<CommentsResponse>(this.appGlobal.GetIdeaComments, JSON.stringify(request), { headers: this.headers });
  }

  GetAlerts(request: Request): Observable<AlertsResponse> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<AlertsResponse>(this.appGlobal.GetIdeaComments, JSON.stringify(request), { headers: this.headers });
  }

  GetIdeaDetails(request: Request): Observable<IdeaResponse> {
    request.author = sessionStorage.getItem("author");
    return this.http.post<IdeaResponse>(this.appGlobal.GetIdeaComments, JSON.stringify(request), { headers: this.headers });
  }

}
