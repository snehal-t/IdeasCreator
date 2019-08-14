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
      .set('Authorization', 'bearer ' + this.appGlobal.AccessToken);
  }

  SignIn(): Observable<SignInResponse> {
    return this.http.post<SignInResponse>(this.appGlobal.SignIn, new HttpParams(), { headers: this.headers });
  }

  CreateIdea(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.CreateIdea, params, { headers: this.headers });
  }

  UpdateIdea(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.put<Response>(this.appGlobal.UpdateIdea, params, { headers: this.headers });
  }

  WithdrawIdea(request: Request, comments: string): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.WithdrawIdea, params, { headers: this.headers });
  }

  ApproveIdea(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.ApproveIdea, params, { headers: this.headers });
  }

  RejectIdea(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.RejectIdea, params, { headers: this.headers });
  }

  DeligateIdea(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.DeligateIdea, params, { headers: this.headers });
  }

  PickIdea(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.PickIdea, params, { headers: this.headers });
  }

  PickIdeaDone(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.PickIdeaDone, params, { headers: this.headers });
  }

  PickIdeaGiveUp(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.PickIdeaGiveUp, params, { headers: this.headers });
  }

  PickIdeaAccept(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.PickIdeaAccept, params, { headers: this.headers });
  }

  PickIdeaReopen(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.PickIdeaReopen, params, { headers: this.headers });
  }

  WatchIdea(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.WatchIdea, params, { headers: this.headers });
  }

  CommentIdea(request: Request): Observable<Response> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<Response>(this.appGlobal.CommentIdea, params, { headers: this.headers });
  }

  GetIdeas(request: Request): Observable<IdeasResponse> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<IdeasResponse>(this.appGlobal.GetIdeas, params, { headers: this.headers });
  }

  GetIdeaWatchers(request: Request): Observable<WatchersResponse> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<WatchersResponse>(this.appGlobal.GetIdeaWatchers, params, { headers: this.headers });
  }

  GetIdeaComments(request: Request): Observable<CommentsResponse> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<CommentsResponse>(this.appGlobal.GetIdeaComments, params, { headers: this.headers });
  }

  GetAlerts(request: Request): Observable<AlertsResponse> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<AlertsResponse>(this.appGlobal.GetIdeaComments, params, { headers: this.headers });
  }

  GetIdeaDetails(request: Request): Observable<IdeaResponse> {
    request.Author = this.appGlobal.Author;
    let params = new HttpParams()
      .set("request", JSON.stringify(request))
    return this.http.post<IdeaResponse>(this.appGlobal.GetIdeaComments, params, { headers: this.headers });
  }

}
