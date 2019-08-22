import { Injectable } from "@angular/core";

@Injectable()
export class AppGlobal {
  //API Base URL
  readonly ApiBaseUrl: string = 'https://localhost:44320/api';
  //readonly ApiBaseUrl: string = 'https://ideasapi.azurewebsites.net/api';

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
  readonly DeleteComment: string = this.ApiBaseUrl + '/DeleteComment';
  readonly EditComment: string = this.ApiBaseUrl + '/EditComment';
  readonly GetDashboard: string = this.ApiBaseUrl + '/GetDashboard';

  //Lazy loading page size
  readonly PageSize: number = 4;

  //Title of pages
  readonly viewIdeaTitle: string = "View Ideas";
  readonly myIdeaTitle: string = "My Ideas";
  readonly newIdeaTitle: string = "New Ideas";
  readonly approvedIdeaTitle: string = "Approved Ideas";
  readonly rejectedIdeaTitle: string = "Rejected Ideas";

  //DB parameters for pages
  readonly viewIdeaSection: string = "VIEW_IDEAS";
  readonly myIdeaSection: string = "MY_IDEAS";
  readonly newIdeaSection: string = "NEW_IDEAS  ";
  readonly approvedIdeaSection: string = "APPROVED_IDEAS";
  readonly rejectedIdeaSection: string = "REJECTED_IDEAS";

  //Idea status
  readonly Idea_New = "New";
  readonly Idea_Edit = "Edit";
  readonly Idea_Approved = "Approved";
  readonly Idea_Reject = "Reject";
  readonly Idea_Delegate = "Delegate";
  readonly Idea_Pick = "Pick";
  readonly Idea_Done = "Done";
  readonly Idea_Rework = "Rework";
  readonly Idea_Withdraw = "Withdraw";
  readonly Idea_GiveUp = "GiveUp";
  readonly Idea_Accept = "Accept";
  readonly Idea_Reopen = "Reopen";
  readonly Idea_Watch = "Watch";
  readonly Idea_Comment = "Comment";
  readonly Idea_Delete_Comment = "DeleteComment";
  readonly Idea_Edit_Comment = "EditComment";
  readonly Idea_Get_Comments = "GetComments";
  readonly Idea_Get_Watchers = "GetWatchers";

  //User Keys
  readonly Author = "author";
  readonly Name = "name";
  readonly Email = "email";
  readonly Moderator = "moderator";
  readonly AccessToken = "accessToken";
  readonly Role = "role";

  //Roles
  readonly Approver = "Approver";
  readonly Creator = "Creator";

  //Toaster titles
  readonly Error = "Error";
  readonly Success = "Success";

  //Idea Status
  readonly Idea = "Idea";
  readonly POC = "POC";

}
