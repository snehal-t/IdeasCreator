import { Component } from '@angular/core';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Ideas Creator';
  constructor(private adalSvc: MsAdalAngular6Service) {
    console.log(this.adalSvc);
    console.log(this.adalSvc.userInfo);
    debugger
    var token = this.adalSvc.acquireToken('https://graph.microsoft.com').subscribe((token: string) => {

      console.log(token);
      //Call Web API here
      //this.adalSvc.logout();
    });;
  }
}
