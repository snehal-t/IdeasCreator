import { Component } from '@angular/core';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';
import { AppGlobal } from './config/appglobal';
import { IdeasapiService } from './services/ideasapi.service';
import { User, SignInResponse } from './idea';
import { debug } from 'util';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Ideas Creator';
  showHtml: boolean = true;
  IsApprover: boolean = false;
  
  constructor(private adalSvc: MsAdalAngular6Service, private appGlobal: AppGlobal, private apiService: IdeasapiService, private signInReponse: SignInResponse) {
    var token = this.adalSvc.acquireToken('https://graph.microsoft.com').subscribe((token: string) => {
      sessionStorage.setItem("accessToken", token);
      console.log(this.adalSvc.userInfo.profile);
      this.apiService.SignIn().subscribe(res => {
        this.signInReponse = res as SignInResponse;
        if (this.signInReponse.isSuccess) {
          this.showHtml = true;
          sessionStorage.setItem("name", this.adalSvc.userInfo.userName);
          sessionStorage.setItem("email", this.adalSvc.userInfo.userName);
          sessionStorage.setItem("role", this.signInReponse.role);
          if (this.signInReponse.role.indexOf('Approver') != -1) {
            this.IsApprover = true;
            sessionStorage.setItem("moderator", "true");
          }
          else {
            sessionStorage.setItem("moderator", "false");
          }
          sessionStorage.setItem("author", this.signInReponse.author);
        }
      });
    });;
  }

  onLogout() {
    this.adalSvc.logout();
  }
}
