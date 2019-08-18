import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, Input } from '@angular/core';
import { IdeasResponse, Request, Idea } from '../idea';
import { IdeasapiService } from '../services/ideasapi.service';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AppGlobal } from '../config/appglobal';
import { debug } from 'util';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
declare var $: any;
declare let self: any;


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
  showDetails: boolean = false;
  actionPopup: boolean = false;
  action: string;
  actionTitle: string;
  actionForm: FormGroup;
  submitted = false;

  //Roles
  isCreator: boolean = true;
  isApprover: boolean = true;
  isDelegator: boolean = true;
  isPicker: boolean = true;

  constructor(private ideaService: IdeasapiService, private toastr: ToastrService, private appGlobal: AppGlobal,
    private ref: ChangeDetectorRef, private spinner: NgxSpinnerService, private formBuilder: FormBuilder) {

  }

  ngOnInit() {
    //Initialize form
    this.actionForm = this.formBuilder.group({
      comments: ['', [Validators.required, Validators.maxLength(4000), Validators.minLength(5)]],
    });

    //Set required parameters
    this.request.ideaPage = "List";
    this.request.pageSize = this.appGlobal.PageSize;
    this.request.currentPage = 1;
    this.request.orderBy = this.orderBy;
    this.request.order = this.sortOrder;
    this.getStories();
    self = this;

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

  onSubmit() {
    this.submitted = true;
    if (this.actionForm.invalid) {
      return;
    }
    const formValue = this.actionForm.value;
    this.ideaDetails.comments = formValue.comments;
    this.ideaService.childEventMethod(this.ideaDetails, this.ideaDetails.action);
    this.actionPopup = false;
  }

  //View ideas API
  getStories() {
    //this.spinner.show();
    return this.ideaService.GetIdeas(this.request).subscribe(res => {

      if (res.isSuccess) {
        this.processData(res.ideas);
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      //this.spinner.hide();
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

  //Open idea details
  openIdeaDetails(idea: Idea) {
    this.ideaDetails = idea;
    this.showDetails = true;
    this.ref.detectChanges();
  }

  openIdeaAction(idea: Idea) {
    this.ideaDetails = idea;
    this.actionPopup = true;
    this.showDetails = false;
    this.getActionTitle(idea.action);
    this.ref.detectChanges();
  }

  openCommentPopup(action: string) {
    this.actionPopup = true;
    this.showDetails = false;
    this.ideaDetails.action = action;
    this.getActionTitle(action);
    this.ref.detectChanges();
  }

  alignIcons() {
    if (this.ideaDetails.id == sessionStorage.getItem("author") && this.ideaDetails.name == sessionStorage.getItem("name") && this.ideaDetails.email == sessionStorage.getItem("email")) {
      this.isCreator = true;
    }
    if (this.ideaDetails.moderatorId == sessionStorage.getItem("author") && this.ideaDetails.moderator == sessionStorage.getItem("name") && this.ideaDetails.moderatorEmail == sessionStorage.getItem("email")) {
      this.isApprover = true;
    }
    if (this.ideaDetails.delegatorId == sessionStorage.getItem("author") && this.ideaDetails.delegator == sessionStorage.getItem("name") && this.ideaDetails.delegatorEmail == sessionStorage.getItem("email")) {
      this.isDelegator = true;
    }
    if (this.ideaDetails.pickerId == sessionStorage.getItem("author") && this.ideaDetails.picker == sessionStorage.getItem("name") && this.ideaDetails.pickerEmail == sessionStorage.getItem("email")) {
      this.isPicker = true;
    }
  }

  getActionTitle(action: string) {
    switch (action) {
      case "Edit":
        this.actionTitle = "Edit Idea";
        break;
      case "Withdraw":
        this.actionTitle = "Withdraw Idea";
        break;
      case "Approve":
        this.actionTitle = "Approve Idea";
        break;
      case "Reject":
        this.actionTitle = "Reject Idea";
        break;
      case "Delegate":
        this.actionTitle = "Delegate Idea";
        break;
      case "Pick":
        this.actionTitle = "Pick Idea";
        break;
      case "PickIdeaDone":
        this.actionTitle = "Picked Idea - Done";
        break;
      case "PickIdeaGiveup":
        this.actionTitle = "Picked Idea - Giveup";
        break;
      case "PickIdeaRework":
        this.actionTitle = "Picked Idea - Rework";
        break;
      case "PickIdeaAccepted":
        this.actionTitle = "Picked Idea - Accepted";
        break;
      case "PickIdeaReopen":
        this.actionTitle = "Picked Idea - Re-open";
        break;
      case "WatchIdea":
        this.ideaService.childEventMethod(this.ideaDetails, this.ideaDetails.action);
        break;
      case "CommentIdea":
        this.actionTitle = "Comments";
        break;
    }
  }

}
