import { Component } from '@angular/core';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';
import { AppGlobal } from './config/appglobal';
import { IdeasapiService } from './services/ideasapi.service';
import { User, SignInResponse } from './idea';
import { debug } from 'util';
import { debounce } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Ideas Creator';
  showHtml: boolean = false;
  IsApprover: boolean = false;
  IsCreator: boolean = false;
  loading: boolean = true;
  
  constructor(private adalSvc: MsAdalAngular6Service, private appGlobal: AppGlobal, private apiService: IdeasapiService, private signInReponse: SignInResponse, private spinner: NgxSpinnerService) {
    spinner.show();
    var token = this.adalSvc.acquireToken('https://graph.microsoft.com').subscribe((token: string) => {
      sessionStorage.setItem(this.appGlobal.AccessToken, token);
      this.apiService.SignIn().subscribe(res => {
        this.signInReponse = res as SignInResponse;
        if (this.signInReponse.isSuccess) {
          this.showHtml = true;
          sessionStorage.setItem(this.appGlobal.Name, this.adalSvc.userInfo.profile.name);
          sessionStorage.setItem(this.appGlobal.Email, this.adalSvc.userInfo.userName);
          sessionStorage.setItem(this.appGlobal.Role, this.signInReponse.role);
          if (this.signInReponse.role.indexOf(this.appGlobal.Approver) != -1) {
            this.IsApprover = true;
            sessionStorage.setItem(this.appGlobal.Moderator, "true");
          }
          else {
            sessionStorage.setItem(this.appGlobal.Moderator, "false");
          }
          if (this.signInReponse.role.indexOf(this.appGlobal.Creator) != -1) {
            this.IsCreator = true;
          }
          sessionStorage.setItem(this.appGlobal.Author, this.signInReponse.author);
          spinner.hide();
          this.loading = false;
        }
      });
    });
  }

  onLogout() {
    this.adalSvc.logout();
  }
}
