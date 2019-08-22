import { Component, OnInit, Input, Output, ChangeDetectorRef, EventEmitter, OnDestroy } from '@angular/core';
import { Idea, Request, User, Watcher } from '../idea';
import { IdeasapiService } from '../services/ideasapi.service';
import { Observable, Subscription } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AppGlobal } from '../config/appglobal';
import { NgxSpinnerService } from 'ngx-spinner';
import { takeWhile } from 'rxjs/operators';
declare var $: any;
declare let self: any;

@Component({
  selector: 'app-idea-list-item',
  templateUrl: './idea-list-item.component.html',
  styleUrls: ['./idea-list-item.component.css']
})
export class IdeaListItemComponent implements OnInit, OnDestroy {
  @Input() idea: Idea;
  @Input() section: string;
  @Input() index: number;

  @Output() openIdeaDetails = new EventEmitter<Idea>();
  @Output() openIdeaAction = new EventEmitter<Idea>();
  @Output() openEditAction = new EventEmitter<Idea>();
  @Output() openWatcherAction = new EventEmitter<Idea>();
  @Output() openCommentAction = new EventEmitter<Idea>();
  @Output() refreshList = new EventEmitter<Idea>();

  request: Request = new Request();
  comments: string;
  componentInstace = true;
  authId: string;
  truncateDesc: string;
  isIdea: boolean;
  isInit: boolean = true;

  //Roles
  isCreator: boolean = false;
  isApprover: boolean = false;
  isDelegator: boolean = false;
  isPicker: boolean = false;
  
  //IconFlags
  isEdit = false;
  isWithdraw = false;
  isHold = false;
  isDelegate = false;
  isApprove = false;
  isReject = false;
  isPick = false;
  isWatch = false;
  isComment = false;
  isDone = false;
  isGiveUp = false;
  isRework = false;
  isAccept = false;

  constructor(private ideaService: IdeasapiService, private toastr: ToastrService, public appGlobal: AppGlobal,
    private ref: ChangeDetectorRef, private spinner: NgxSpinnerService) { }

  ngOnInit() {
    if (this.idea.type == this.appGlobal.Idea) {
      this.isIdea = true;
    }
    else {
      this.isIdea = false;
    }
    if (this.idea.description.length > 150) {
      this.truncateDesc = this.idea.description.substr(0, 150) + '...';
    }
    else {
      this.truncateDesc = this.idea.description;
    }
    this.alignIcons();
    this.setIconsAsPerStatus();
    this.authId = sessionStorage.getItem(this.appGlobal.Author);
    this.ideaService.childEventCallback.pipe(takeWhile(() => this.componentInstace)).subscribe(res => {
      if (this.index === res.ideaId && this.section == res.section && !this.isInit) {
        this.request.assignee = null;
        this.comments = res.comments;
        this.request.ideaId = res.ideaId;
        switch (res.action) {
          case this.appGlobal.Idea_Withdraw:
            this.withdrawIdea();
            break;
          case this.appGlobal.Idea_Approved:
            this.approveIdea();
            break;
          case this.appGlobal.Idea_Reject:
            this.rejectIdea();
            break;
          case this.appGlobal.Idea_Delegate:
            this.request.assignee = res.assignee;
            this.deligateIdea();
            break;
          case this.appGlobal.Idea_Pick:
            this.pickIdea();
            break;
          case this.appGlobal.Idea_Done:
            this.pickIdeaDone();
            break;
          case this.appGlobal.Idea_GiveUp:
            this.pickIdeaGiveUp();
            break;
          case this.appGlobal.Idea_Rework:
            this.pickIdeaRework();
            break;
          case this.appGlobal.Idea_Accept:
            this.pickIdeaAccept();
            break;
          case this.appGlobal.Idea_Reopen:
            this.pickIdeaReopen();
            break;
          case this.appGlobal.Idea_Watch:
            this.watchIdea();
            break;
          case this.appGlobal.Idea_Comment:
            this.commentIdea();
            break;
          case this.appGlobal.Idea_Delete_Comment:
            this.request.commentId = res.commentId;
            this.deleteComment();
            break;
          case this.appGlobal.Idea_Edit_Comment:
            this.request.commentId = res.commentId;
            this.editComment();
            break;
          case this.appGlobal.Idea_Get_Comments:
            this.parentOpenCommentAction();
            break;
          case this.appGlobal.Idea_Get_Watchers:
            this.parentOpenWatcherAction();
            break;
        }
      }
      else {
        this.isInit = false;
      }
    })
  }

  alignIcons() {
    if (this.idea.id == sessionStorage.getItem(this.appGlobal.Author) && this.idea.name == sessionStorage.getItem(this.appGlobal.Name) && this.idea.email == sessionStorage.getItem(this.appGlobal.Email)) {
      this.isCreator = true;
    }
    if (sessionStorage.getItem(this.appGlobal.Moderator) == "true") {
      this.isApprover = true;
    }
    if (this.idea.delegatorId == sessionStorage.getItem(this.appGlobal.Author) && this.idea.delegator == sessionStorage.getItem(this.appGlobal.Name) && this.idea.delegatorEmail == sessionStorage.getItem(this.appGlobal.Email)) {
      this.isDelegator = true;
    }
    if (this.idea.pickerId == sessionStorage.getItem(this.appGlobal.Author) && this.idea.picker == sessionStorage.getItem(this.appGlobal.Name) && this.idea.pickerEmail == sessionStorage.getItem(this.appGlobal.Email)) {
      this.isPicker = true;
    }
  }

  setIconsAsPerStatus() {
    this.isEdit = false;
    this.isWithdraw = false;
    this.isHold = false;
    this.isDelegate = false;
    this.isApprove = false;
    this.isReject = false;
    this.isPick = false;
    this.isWatch = false;
    this.isComment = false;
    this.isDone = false;
    this.isGiveUp = false;
    this.isRework = false;
    this.isAccept = false;
    switch (this.idea.ideaStatus) {
      case this.appGlobal.Idea_New:
        //Creator(Edit, Withdraw, Hold)
        this.isEdit = true;
        this.isWithdraw = true;
        this.isHold = true;
        //Moderator(Delegate, Approve, Reject)
        this.isDelegate = true;
        this.isApprove = true;
        this.isReject = true;
        break;
      case this.appGlobal.Idea_Approved:
        //Creator(Edit, Withdraw, Hold)
        this.isEdit = true;
        this.isWithdraw = true;
        this.isHold = true;
        //Moderator(Reject)
        this.isReject = true;
        //EndUser(Pick, Watch, Comment)
        this.isPick = true;
        this.isWatch = true;
        this.isComment = true;
        break;
      case this.appGlobal.Idea_GiveUp:
        //Creator(Edit, Withdraw, Hold)
        this.isEdit = true;
        this.isWithdraw = true;
        this.isHold = true;
        //Moderator(Reject)
        this.isReject = true;
        //EndUser(Pick, Watch, Comment)
        this.isPick = true;
        this.isWatch = true;
        this.isComment = true;
        break;
      case this.appGlobal.Idea_Reject:
        //Creator(Edit, Withdraw, Hold)
        this.isEdit = true;
        this.isWithdraw = true;
        this.isHold = true;
        //Moderator(Approve)
        this.isApprove = true;
        break;
      case this.appGlobal.Idea_Delegate:
        //Creator(Edit, Withdraw, Hold)
        this.isEdit = true;
        this.isWithdraw = true;
        this.isHold = true;
        //Moderator / Delegator(Approve, Reject, Delegate)
        this.isDelegate = true;
        this.isApprove = true;
        this.isReject = true;
        break;
      case this.appGlobal.Idea_Pick:
        //Creator(Edit, Withdraw, Hold)
        this.isEdit = true;
        this.isWithdraw = true;
        this.isHold = true;
        //Moderator(Reject)
        this.isReject = true;
        //EndUser(Watch, Comment)
        this.isWatch = true;
        this.isComment = true;
        //Picker(Done, GiveUp)
        this.isDone = true;
        this.isGiveUp = true;
        break;
      case this.appGlobal.Idea_Done:
        //Creator(Edit, Withdraw, Hold, Rework, Accept)
        this.isEdit = true;
        this.isWithdraw = true;
        this.isHold = true;
        this.isRework = true;
        this.isAccept = true;
        //Moderator(Reject)
        this.isReject = true;
        //EndUser(Watch, Comment)
        this.isWatch = true;
        this.isComment = true;
        break;
      case this.appGlobal.Idea_Rework:
        //Creator(Edit, Withdraw, Hold)
        this.isEdit = true;
        this.isWithdraw = true;
        this.isHold = true;
        //Moderator(Reject)
        this.isReject = true;
        //EndUser(Watch, Comment)
        this.isWatch = true;
        this.isComment = true;
        //Picker(Done, GiveUp)
        this.isDone = true;
        this.isGiveUp = true;
        break;
    }
  }

  parentOpenIdeaDetails() {
    this.alignIcons();
    this.setIconsAsPerStatus();
    this.idea.isChild = true;
    this.idea = {
      ...this.idea,
      section: this.section
    }
    this.openIdeaDetails.emit(this.idea);
  }

  parentOpenIdeaAction(action: string) {
    this.alignIcons();
    this.setIconsAsPerStatus();
    this.idea.isChild = true;
    this.idea = {
      ...this.idea,
      action: action,
      section: this.section
    }
    this.openIdeaAction.emit(this.idea);
  }

  parentOpenEditAction(action: string) {
    this.alignIcons();
    this.setIconsAsPerStatus();
    this.idea.isChild = true;
    this.idea = {
      ...this.idea,
      action: action,
      section: this.section
    }
    this.openEditAction.emit(this.idea);
  }

  parentOpenWatcherAction() {
    this.request.ideaId = this.idea.ideaId;
    this.spinner.show();
    this.idea.isChild = true;
    this.idea = {
      ...this.idea,
      section: this.section
    }
    this.ideaService.GetIdeaWatchers(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.idea.watchers = res.watchers;
        this.openWatcherAction.emit(this.idea);
      }
      else {
        this.openWatcherAction.emit(this.idea);
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  parentOpenCommentAction() {
    this.idea.isChild = true;
    this.request.ideaId = this.idea.ideaId;
    this.idea = {
      ...this.idea,
      section: this.section
    }
    this.spinner.show();
    this.ideaService.GetIdeaComments(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.idea.commentList = res.comments;
        this.openCommentAction.emit(this.idea);
      }
      else {
        this.openCommentAction.emit(this.idea);
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  parentRefreshList() {
    this.idea = {
      ...this.idea,
      section: this.section
    }
    this.refreshList.emit(this.idea);
  }

  withdrawIdea() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.WithdrawIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.parentRefreshList();
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  approveIdea() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.ApproveIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.idea.ideaStatus = this.appGlobal.Idea_Approved;
        if (this.section == this.appGlobal.rejectedIdeaSection || this.section == this.appGlobal.newIdeaSection) {
          this.parentRefreshList();
        }
        if (!this.idea.isChild) {
          this.parentOpenIdeaDetails();
        }
        else {
          this.alignIcons();
          this.setIconsAsPerStatus();
        }
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  rejectIdea() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.RejectIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.idea.ideaStatus = this.appGlobal.Idea_Reject;
        //if (this.section == this.appGlobal.approvedIdeaSection) {
          this.parentRefreshList();
        //}
        if (!this.idea.isChild) {
          this.parentOpenIdeaDetails();
        }
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  deligateIdea() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.DeligateIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.idea.ideaStatus = this.appGlobal.Idea_Delegate;
        if (!this.idea.isChild) {
          this.parentOpenIdeaDetails();
        }
        else {
          this.alignIcons();
          this.setIconsAsPerStatus();
        }
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  pickIdea() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.idea.ideaStatus = this.appGlobal.Idea_Pick;
        if (!this.idea.isChild) {
          this.parentOpenIdeaDetails();
        }
        else {
          this.alignIcons();
          this.setIconsAsPerStatus();
        }
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  pickIdeaDone() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdeaDone(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.idea.ideaStatus = this.appGlobal.Idea_Done;
        if (!this.idea.isChild) {
          this.parentOpenIdeaDetails();
        }
        else {
          this.alignIcons();
          this.setIconsAsPerStatus();
        }
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  pickIdeaGiveUp() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdeaGiveUp(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.idea.ideaStatus = this.appGlobal.Idea_Approved;
        if (!this.idea.isChild) {
          this.parentOpenIdeaDetails();
        }
        else {
          this.alignIcons();
          this.setIconsAsPerStatus();
        }
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  pickIdeaRework() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdeaRework(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.idea.ideaStatus = this.appGlobal.Idea_Pick;
        if (!this.idea.isChild) {
          this.parentOpenIdeaDetails();
        }
        else {
          this.alignIcons();
          this.setIconsAsPerStatus();
        }
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  pickIdeaAccept() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdeaAccept(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.idea.ideaStatus = this.appGlobal.Idea_Accept;
        if (!this.idea.isChild) {
          this.parentOpenIdeaDetails();
        }
        else {
          this.alignIcons();
          this.setIconsAsPerStatus();
        }
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  pickIdeaReopen() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdeaReopen(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.idea.ideaStatus = this.appGlobal.Idea_Approved;
        if (!this.idea.isChild) {
          this.parentOpenIdeaDetails();
        }
        else {
          this.alignIcons();
          this.setIconsAsPerStatus();
        }
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  watchIdea() {
    this.spinner.show();
    this.request.isWatching = !this.idea.isWatching;
    this.idea.isWatching = !this.idea.isWatching;
    
    this.ideaService.WatchIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        //this.parentOpenWatcherAction();
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  commentIdea() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.CommentIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.parentOpenCommentAction();
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  deleteComment() {
    this.spinner.show();
    this.ideaService.DeleteComment(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.parentOpenCommentAction();
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  editComment() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.EditComment(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.parentOpenCommentAction();
        this.toastr.success(res.message, this.appGlobal.Success);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
      this.spinner.hide();
    });
  }

  ngOnDestroy() {
    this.componentInstace = false;
  }
}
