import { Component, OnInit, Input, Output, ChangeDetectorRef, EventEmitter, OnDestroy } from '@angular/core';
import { Idea, Request, User } from '../idea';
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

  request: Request = new Request();
  comments: string;
  componentInstace = true;

  //Roles
  isCreator: boolean = true;
  isApprover: boolean = true;
  isDelegator: boolean = true;
  isPicker: boolean = true;

  constructor(private ideaService: IdeasapiService, private toastr: ToastrService, private appGlobal: AppGlobal,
    private ref: ChangeDetectorRef, private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.ideaService.childEventCallback.pipe(takeWhile(() => this.componentInstace)).subscribe(res => {
      if (this.index === res.ideaId) {
        this.comments = res.comments;
        this.request.ideaId = res.ideaId;
        switch (res.action) {
          case "Withdraw":
            this.withdrawIdea();
            break;
          case "Approve":
            this.approveIdea();
            break;
          case "Reject":
            this.rejectIdea();
            break;
          case "Delegate":
            this.deligateIdea();
            break;
          case "Pick":
            this.pickIdea();
            break;
          case "PickIdeaDone":
            this.pickIdeaDone();
            break;
          case "PickIdeaGiveup":
            this.pickIdeaGiveUp();
            break;
          case "PickIdeaRework":
            this.pickIdeaRework();
            break;
          case "PickIdeaAccepted":
            this.pickIdeaAccept();
            break;
          case "PickIdeaReopen":
            this.pickIdeaReopen();
            break;
          case "WatchIdea":
            //TODO
            break;
          case "CommentIdea":
            //TODO
            break;
        }
      }
    })
  }

  alignIcons() {
    if (this.idea.id == sessionStorage.getItem("author") && this.idea.name == sessionStorage.getItem("name") && this.idea.email == sessionStorage.getItem("email"))
    {
      this.isCreator = true;
    }
    if (this.idea.moderatorId == sessionStorage.getItem("author") && this.idea.moderator == sessionStorage.getItem("name") && this.idea.moderatorEmail == sessionStorage.getItem("email"))
    {
      this.isApprover = true;
    }
    if (this.idea.delegatorId == sessionStorage.getItem("author") && this.idea.delegator == sessionStorage.getItem("name") && this.idea.delegatorEmail == sessionStorage.getItem("email"))
    {
      this.isDelegator = true;
    }
    if (this.idea.pickerId == sessionStorage.getItem("author") && this.idea.picker == sessionStorage.getItem("name") && this.idea.pickerEmail == sessionStorage.getItem("email"))
    {
      this.isPicker = true;
    }
  }

  parentOpenIdeaDetails() {
    this.openIdeaDetails.next(this.idea);
  }

  parentOpenIdeaAction(action: string) {
    this.idea = {
      ...this.idea,
      action: action
    }
    this.openIdeaAction.next(this.idea);
  }

  withdrawIdea() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.WithdrawIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.toastr.success(res.message, "Success");
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.spinner.hide();
    });
  }

  approveIdea() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.ApproveIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.toastr.success(res.message, "Success");
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.spinner.hide();
    });
  }

  rejectIdea() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.RejectIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.toastr.success(res.message, "Success");
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.spinner.hide();
    });
  }

  deligateIdea() {
    this.spinner.show();
    //TODO
    this.request.assignee = new User();
    this.request.assignee.email = "snehalt@quinnox.com";
    this.request.assignee.name = "Snehal Thube";

    this.request.comments = this.comments;
    this.ideaService.DeligateIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.toastr.success(res.message, "Success");
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.spinner.hide();
    });
  }

  pickIdea() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.toastr.success(res.message, "Success");
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.spinner.hide();
    });
  }

  pickIdeaDone() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdeaDone(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.toastr.success(res.message, "Success");
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.spinner.hide();
    });
  }

  pickIdeaGiveUp() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdeaGiveUp(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.toastr.success(res.message, "Success");
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.spinner.hide();
    });
  }

  pickIdeaRework() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdeaRework(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.toastr.success(res.message, "Success");
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.spinner.hide();
    });
  }

  pickIdeaAccept() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdeaAccept(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.toastr.success(res.message, "Success");
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.spinner.hide();
    });
  }

  pickIdeaReopen() {
    this.spinner.show();
    this.request.comments = this.comments;
    this.ideaService.PickIdeaReopen(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.toastr.success(res.message, "Success");
      }
      else {
        this.toastr.error(res.message, "Error");
      }
      this.spinner.hide();
    });
  }

  ngOnDestroy() {
    this.componentInstace = false;
  }

}
