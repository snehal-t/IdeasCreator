import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input } from '@angular/core';
import { IdeasResponse, Request, Idea, User } from '../idea';
import { IdeasapiService } from '../services/ideasapi.service';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AppGlobal } from '../config/appglobal';
import { debug } from 'util';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
declare var $: any;
declare let self: any;
import { JustificationValidator } from './ideal-justification-validator';
import { createElementCssSelector } from '@angular/compiler';

@Component({
  selector: 'app-view-ideas',
  templateUrl: './view-ideas.component.html',
  styleUrls: ['./view-ideas.component.css']
})

export class ViewIdeasComponent implements OnInit {
  currentPage: number = 1;
  ideaResponse: IdeasResponse = new IdeasResponse();
  request: Request = new Request();
  stopCallBack: boolean = false;
  orderBy: any = "CREATED_DATE";
  sortOrder: any = "DESC";
  isSortChange: boolean = false;
  @Input() section: string;
  @Input() title: string;
  ideaDetails: Idea;
  ideaDetailsForm: Idea;
  showDetails: boolean = false;
  actionPopup: boolean = false;
  editPopup: boolean = false;
  watcherPopup: boolean = false;
  commentsPopup: boolean = false;
  editCancel: boolean = false;
  noIdeaFound: boolean = false;

  action: string;
  actionTitle: string;
  actionForm: FormGroup;
  ideaForm: FormGroup;
  ideaSubmitted = false;
  submitted = false;
  commentFormTItle = "Add";
  ideaFormTItle = "Add";

  //Roles to show icons
  isCreator: boolean = false;
  isApprover: boolean = false;
  isDelegator: boolean = false;
  isPicker: boolean = false;

  //To show email field in common box
  isDelegatorAction: boolean = false;

  //IconFlags based on idea status
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

  watchTitle: string = "Watch";
  authId: string;

  constructor(private ideaService: IdeasapiService, private toastr: ToastrService, public appGlobal: AppGlobal,
    private ref: ChangeDetectorRef, private spinner: NgxSpinnerService, private formBuilder: FormBuilder) {

  }

  ngOnInit() {
    this.authId = sessionStorage.getItem(this.appGlobal.Author);
    //Initialize common form
    this.actionForm = this.formBuilder.group({
      comments: ['', [Validators.required, Validators.maxLength(4000), Validators.minLength(5)]],
      email: ['', [Validators.required, Validators.email]]
    });

    //Initialize idea edit form
    this.ideaForm = this.formBuilder.group({
      category: ['', [Validators.required]],
      source: ['', [Validators.required]],
      title: ['', [Validators.required, Validators.maxLength(200), Validators.minLength(2)]],
      brief: ['', [Validators.required, Validators.maxLength(4000), Validators.minLength(2)]],
      businessCase: ['', [Validators.required, Validators.maxLength(4000), Validators.minLength(2)]],
      idealTime: ['', [Validators.required]],
      idealTimeJustification: ['', [Validators.maxLength(4000)]],
      contactName: ['', [Validators.required, Validators.maxLength(100), Validators.minLength(2)]],
      contactEmail: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
      contactNumber: ['', [Validators.required]]
    }, {
        validator: JustificationValidator('idealTime', 'idealTimeJustification')
      });

    //Set required parameters for API Call
    this.request.ideaPage = this.section;
    this.request.pageSize = this.appGlobal.PageSize;
    this.request.currentPage = 1;
    this.request.orderBy = this.orderBy;
    this.request.order = this.sortOrder;

    //Load initial ideas
    this.getStories();

    self = this;

    //Lazy loading ideas based on pagination
    $(document).ready(function () {
      window.onscroll = function (ev) {
        if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
          //Lazy loading
          if (!self.getCallBackFlag()) {
            self.getStories();
          }
        }
      };
    });
  }

  get f() { return this.actionForm.controls; }

  get i() { return this.ideaForm.controls; }

  //Common form for all actions
  onSubmit() {
    this.submitted = true;
    const formValue = this.actionForm.value;
    this.ideaDetails.comments = formValue.comments;
    this.ideaDetails.assignee = formValue.email;

    if (this.actionForm.invalid && this.ideaDetails.comments == "") {
      return;
    }

    if (this.ideaDetails.action == this.appGlobal.Idea_Delegate && this.ideaDetails.assignee == "") {
      return;
    }

    this.ideaService.childEventMethod(this.ideaDetails, this.ideaDetails.action);
    this.actionPopup = false;
    this.commentsPopup = false;
    this.actionForm.reset();
  }

  //Submit form for edit idea
  onIdeaSubmit() {
    this.ideaSubmitted = true;
    if (this.ideaForm.invalid) {
      return;
    }
    const formValue = this.ideaForm.value;

    //Form Idea
    this.ideaDetailsForm.type = formValue.category;
    this.ideaDetailsForm.source = formValue.source;
    this.ideaDetailsForm.title = formValue.title;
    this.ideaDetailsForm.ideaName = formValue.title;
    this.ideaDetailsForm.description = formValue.brief;
    this.ideaDetailsForm.businessCase = formValue.businessCase;
    this.ideaDetailsForm.idealTime = formValue.idealTime;
    this.ideaDetailsForm.businessJustification = formValue.idealTimeJustification;
    this.ideaDetailsForm.contactName = formValue.contactName;
    this.ideaDetailsForm.contactEmail = formValue.contactEmail;
    this.ideaDetailsForm.contactMobileNo = formValue.contactNumber;

    //Send it in Request
    this.request.idea = this.ideaDetailsForm;
    if (this.ideaDetailsForm.action == this.appGlobal.Idea_Edit) {
      //Call edit idea API
      this.spinner.show();
      this.ideaService.UpdateIdea(this.request).subscribe(res => {
        if (res.isSuccess) {
          this.ideaForm.reset();
          this.ideaSubmitted = false;
          this.editPopup = false;
          this.ideaDetails = this.ideaDetailsForm;
          this.spinner.hide();
          this.toastr.success(res.message, this.appGlobal.Success)
        }
        else {
          this.toastr.error(res.message, this.appGlobal.Error);
        }
      });
    }
    else if (this.ideaDetailsForm.action == this.appGlobal.Idea_New) {
      //Call create idea API
      this.spinner.show();
      this.ideaService.CreateIdea(this.request).subscribe(res => {
        if (res.isSuccess) {
          this.ideaForm.reset();
          this.ideaSubmitted = false;
          this.editPopup = false;
          this.spinner.hide();
          this.toastr.success(res.message, this.appGlobal.Success)
        }
        else {
          this.toastr.error(res.message, this.appGlobal.Error);
        }
      });
    } 
  }

  //View ideas API
  getStories() {
    this.spinner.show();
    return this.ideaService.GetIdeas(this.request).subscribe(res => {

      if (res.isSuccess && res.ideas != null) {
        this.processData(res.ideas);
        this.noIdeaFound = false;
      }
      else if (this.currentPage == 1) {
        this.noIdeaFound = true;
      }
      else if (this.currentPage > 1 && res.ideas == null) {
        this.stopCallBack = true;
      }
      this.spinner.hide();
    });
  }

  //Append Data to existing list
  processData(ideaList: Idea[]) {
    if (ideaList.length < this.appGlobal.PageSize) {
      this.stopCallBack = true;
    }
    this.currentPage++;
    this.request.currentPage = this.currentPage;
    this.ideaResponse.ideas = this.ideaResponse.ideas ? this.ideaResponse.ideas.concat(ideaList) : ideaList;
    this.ref.detectChanges();
    if (this.isSortChange && ideaList.length == this.appGlobal.PageSize) {
      this.stopCallBack = false;
      this.isSortChange = false;
    }
  }

  refreshList(idea: Idea) {
    this.request.currentPage = 1;
    this.ideaResponse.ideas = [];
    this.getStories();
  }

  //To check whether to call API post last page
  getCallBackFlag() {
    return this.stopCallBack;
  }

  //Sort change
  OnSortChange() {
    this.isSortChange = true;
    this.stopCallBack = true;
    this.ideaResponse = new IdeasResponse();
    this.request.orderBy = this.orderBy;
    this.request.order = this.sortOrder;
    this.currentPage = 1;
    this.request.currentPage = 1;
    this.getStories();
  }

  //To be called from child
  openIdeaDetails(idea: Idea) {
    this.ideaDetails = idea;
    this.showDetails = true;
    this.alignIcons();
    this.setIconsAsPerStatus();
    this.ref.detectChanges();
  }

  //To be called from child on click of Withdraw, Pick, Done, Accpet etc.
  openIdeaAction(idea: Idea) {
    this.ideaDetails = idea;
    this.actionPopup = true;
    this.showDetails = false;
    this.getActionTitle(idea.action);
    this.alignIcons();
    this.setIconsAsPerStatus();
    this.ref.detectChanges();
  }

  //To be called from child on click of Edit idea
  openEditAction(idea: Idea) {
    this.ideaDetailsForm = idea;
    this.editPopup = true;
    this.getActionTitle(idea.action);
    this.ref.detectChanges();
  }

  closeAll() {
    this.showDetails = false;
    this.actionPopup = false;
    this.watcherPopup = false;
    this.commentsPopup = false;
    this.editPopup = false;
    this.commentFormTItle = "Add";
    this.actionForm.reset();
    this.ideaForm.reset();
  }

  //Open form from create/edit
  openEditActionPopup(action: string) {
    debugger
    if (action == this.appGlobal.Idea_New) {
      this.ideaDetailsForm = new Idea();
    }
    else if (action == this.appGlobal.Idea_Edit) {
      this.ideaDetailsForm = this.ideaDetails;
    }
    this.editPopup = true;
    this.ideaDetailsForm.action = action;
    this.ideaDetailsForm.isChild = false;
    this.getActionTitle(action);
    this.ref.detectChanges();
  }

  //Open commmon box for Withdraw, Pick, Done, Accept etc.
  openCommentPopup(action: string) {
    this.actionPopup = true;
    this.showDetails = false;
    this.ideaDetails.action = action;
    this.ideaDetails.isChild = false;
    this.getActionTitle(action);
    this.ref.detectChanges();
  }

  //To be called from child on click of watchers
  openWatcherAction(idea: Idea) {
    if (idea.isWatching) {
      this.watchTitle = "Unwatch";
    }
    else {
      this.watchTitle = "Watch";
    }
    this.ideaDetails = idea;
    this.watcherPopup = true;
    this.showDetails = false;
    this.getActionTitle(this.appGlobal.Idea_Get_Watchers);
    this.alignIcons();
    this.setIconsAsPerStatus();
    this.ref.detectChanges();
  }

  //Open watchers popup
  openWatcherPopup(action: string) {
    if (this.ideaDetails.isWatching) {
      this.watchTitle = "Unwatch";
    }
    else {
      this.watchTitle = "Watch";
    }
    this.ideaDetails.isChild = false;
    this.ideaService.childEventMethod(this.ideaDetails, this.appGlobal.Idea_Get_Watchers);
  }

  //To be called from child on click of comments
  openCommentAction(idea: Idea) {
    this.ideaDetails = idea;
    this.commentsPopup = true;
    this.showDetails = false;
    this.getActionTitle(this.appGlobal.Idea_Get_Comments);
    this.ideaDetails.action = this.appGlobal.Idea_Comment;
    this.alignIcons();
    this.setIconsAsPerStatus();
    this.ref.detectChanges();
  }

  //Open comments popup
  openCommentsPopup(action: string) {
    this.ideaService.childEventMethod(this.ideaDetails, this.appGlobal.Idea_Get_Comments);
  }

  //Delete comment
  deleteComment(commentId: string) {
    var result = confirm("Are you sure you want to delete this comment?");
    if (result) {
      this.ideaDetails.commentId = commentId;
      this.ideaService.childEventMethod(this.ideaDetails, this.appGlobal.Idea_Delete_Comment);
      this.openCommentsPopup(this.appGlobal.Idea_Get_Comments);
    }
  }

  //Edit comment in comment popup
  editComment(commentId: string, comments: string) {
    this.ideaDetails.commentId = commentId;
    this.ideaDetails.comments = comments;
    $("#commentBox").val(comments);
    this.commentFormTItle = "Edit";
    this.ideaDetails.action = this.appGlobal.Idea_Edit_Comment;
    this.editCancel = true;
  }

  //Cancel edit comment
  CancelEditComment() {
    this.actionForm.reset();
    this.ideaDetails.commentId = "";
    this.ideaDetails.comments = "";
    this.commentFormTItle = "Add";
    this.editCancel = false;
    this.ideaDetails.action = "";
  }

  //Based on role align icons
  alignIcons() {
    if (this.ideaDetails.id == sessionStorage.getItem(this.appGlobal.Author) && this.ideaDetails.name == sessionStorage.getItem(this.appGlobal.Name) && this.ideaDetails.email == sessionStorage.getItem(this.appGlobal.Email)) {
      this.isCreator = true;
    }
    if (sessionStorage.getItem(this.appGlobal.Moderator) == "true") {
      this.isApprover = true;
    }
    if (this.ideaDetails.delegatorId == sessionStorage.getItem(this.appGlobal.Author) && this.ideaDetails.delegator == sessionStorage.getItem(this.appGlobal.Name) && this.ideaDetails.delegatorEmail == sessionStorage.getItem(this.appGlobal.Email)) {
      this.isDelegator = true;
    }
    if (this.ideaDetails.pickerId == sessionStorage.getItem(this.appGlobal.Author) && this.ideaDetails.picker == sessionStorage.getItem(this.appGlobal.Name) && this.ideaDetails.pickerEmail == sessionStorage.getItem(this.appGlobal.Email)) {
      this.isPicker = true;
    }
  }

  //Based on idea status align icons
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
    switch (this.ideaDetails.ideaStatus) {
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

  //Get popup heading
  getActionTitle(action: string) {
    this.isDelegatorAction = false;
    switch (action) {
      case this.appGlobal.Idea_New:
        this.actionTitle = "Create Idea";
        break;
      case this.appGlobal.Idea_Edit:
        this.actionTitle = "Edit Idea";
        break;
      case this.appGlobal.Idea_Withdraw:
        this.actionTitle = "Withdraw Idea";
        break;
      case this.appGlobal.Idea_Approved:
        this.actionTitle = "Approve Idea";
        break;
      case this.appGlobal.Idea_Reject:
        this.actionTitle = "Reject Idea";
        break;
      case this.appGlobal.Idea_Delegate:
        this.isDelegatorAction = true;
        this.actionTitle = "Delegate Idea";
        break;
      case this.appGlobal.Idea_Pick:
        this.actionTitle = "Pick Idea";
        break;
      case this.appGlobal.Idea_Done:
        this.actionTitle = "Picked Idea - Done";
        break;
      case this.appGlobal.Idea_GiveUp:
        this.actionTitle = "Picked Idea - Giveup";
        break;
      case this.appGlobal.Idea_Rework:
        this.actionTitle = "Picked Idea - Rework";
        break;
      case this.appGlobal.Idea_Accept:
        this.actionTitle = "Picked Idea - Accepted";
        break;
      case this.appGlobal.Idea_Reopen:
        this.actionTitle = "Picked Idea - Re-open";
        break;
      case this.appGlobal.Idea_Watch:
        this.actionPopup = false;
        this.watcherPopup = false;
        this.ideaService.childEventMethod(this.ideaDetails, this.appGlobal.Idea_Watch);
        break;
      case this.appGlobal.Idea_Comment:
        this.actionPopup = false;
        this.watcherPopup = false;
        this.ideaService.childEventMethod(this.ideaDetails, this.appGlobal.Idea_Comment);
        break;
      case this.appGlobal.Idea_Get_Watchers:
        this.actionTitle = "Idea Watchers";
        break;
      case this.appGlobal.Idea_Get_Comments:
        this.actionTitle = "Idea Comments";
        break;
    }
  }

  ngOnDestroy() {
    // this.componentInstace = false;
  }

}
