import { Component } from '@angular/core';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';
import { AppGlobal } from './config/appglobal';
import { IdeasapiService } from './services/ideasapi.service';
import { User, SignInResponse } from './idea';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Ideas Creator';
  showHtml: boolean = true;

  constructor(private adalSvc: MsAdalAngular6Service, private appGlobal: AppGlobal, private apiService: IdeasapiService, private signInReponse: SignInResponse) {
    var token = this.adalSvc.acquireToken('https://graph.microsoft.com').subscribe((token: string) => {     
      sessionStorage.setItem("accessToken", token);
      this.apiService.SignIn().subscribe(res => {
        this.signInReponse = res as SignInResponse;
        if (this.signInReponse.isSuccess) {
          this.showHtml = true;
          sessionStorage.setItem("author", this.signInReponse.author);
        }
      });
    });;
  }

  onLogout() {
    this.adalSvc.logout();
  }
}
