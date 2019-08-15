import { Component, OnInit } from '@angular/core';
import { IdeasResponse, Request } from '../idea';
import { IdeasapiService } from '../services/ideasapi.service';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { AppGlobal } from '../config/appglobal';

@Component({
  selector: 'app-view-ideas',
  templateUrl: './view-ideas.component.html',
  styleUrls: ['./view-ideas.component.css']
})
export class ViewIdeasComponent implements OnInit {
  ideas: IdeasResponse = new IdeasResponse();
  request: Request = new Request();

  constructor(private ideaService: IdeasapiService, private toastr: ToastrService, private appGlobal: AppGlobal) { }

  ngOnInit() {
    //Set required parameters
    this.request.ideaPage = "VIEW_IDEAS";
    this.request.pageSize = this.appGlobal.PageSize;
    this.request.currentPage = 1;
    this.request.orderBy = "";
    this.request.order = "ASC";

    //Call API
    /*this.ideaService.GetIdeas(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.ideas.ideas = res.ideas;
      }
      else {
        this.toastr.error(res.message, "Error");
      }
    });*/
  }

}
