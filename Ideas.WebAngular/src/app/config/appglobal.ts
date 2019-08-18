import { Injectable } from "@angular/core";

@Injectable()
export class AppGlobal {
  //API Base URL
  readonly ApiBaseUrl: string = 'https://172.23.238.51/api';

  //API URL
  readonly SignIn: string = this.ApiBaseUrl + '/SignIn';
  readonly CreateIdea: string = this.ApiBaseUrl + '/CreateIdea';
  readonly UpdateIdea: string = this.ApiBaseUrl + '/UpdateIdea';
  readonly WithdrawIdea: string = this.ApiBaseUrl + '/WithdrawIdea';
  readonly ApproveIdea: string = this.ApiBaseUrl + '/ApproveIdea';
  readonly RejectIdea: string = this.ApiBaseUrl + '/RejectIdea';
  readonly DeligateIdea: string = this.ApiBaseUrl + '/DeligateIdea';
  readonly PickIdea: string = this.ApiBaseUrl + '/PickIdea';
  readonly PickIdeaDone: string = this.ApiBaseUrl + '/PickIdeaDone';
  readonly PickIdeaGiveUp: string = this.ApiBaseUrl + '/PickIdeaGiveUp';
  readonly PickIdeaRework: string = this.ApiBaseUrl + '/PickIdeaRework';
  readonly PickIdeaAccept: string = this.ApiBaseUrl + '/PickIdeaAccept';
  readonly PickIdeaReopen: string = this.ApiBaseUrl + '/PickIdeaReopen';
  readonly WatchIdea: string = this.ApiBaseUrl + '/WatchIdea';
  readonly CommentIdea: string = this.ApiBaseUrl + '/CommentIdea';
  readonly GetIdeas: string = this.ApiBaseUrl + '/GetIdeas';
  readonly GetIdeaWatchers: string = this.ApiBaseUrl + '/GetIdeaWatchers';
  readonly GetIdeaComments: string = this.ApiBaseUrl + '/GetIdeaComments';
  readonly GetAlerts: string = this.ApiBaseUrl + '/GetAlerts';
  readonly GetIdeaDetails: string = this.ApiBaseUrl + '/GetIdeaDetails';

  //Lazy loading page size
  readonly PageSize: number = 4;

  readonly viewIdeaTitle: string = "View Ideas";
  readonly myIdeaTitle: string = "My Ideas";
  readonly newIdeaTitle: string = "New Ideas";
  readonly approvedIdeaTitle: string = "Approved Ideas";
  readonly rejectedIdeaTitle: string = "Rejected Ideas";

  readonly viewIdeaSection: string = "VIEW_IDEAS";
  readonly myIdeaSection: string = "MY_IDEAS";
  readonly newIdeaSection: string = "NEW_IDEAS  ";
  readonly approvedIdeaSection: string = "APPROVED_IDEAS";
  readonly rejectedIdeaSection: string = "REJECTED_IDEAS";
}
