import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { DashboardResponse, Request } from '../idea';
import { IdeasapiService } from '../services/ideasapi.service';
import { Observable } from 'rxjs';
import { AppGlobal } from '../config/appglobal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

declare var $: any;
declare function drawDashboard(any): any;

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  dashboardResponse: DashboardResponse = new DashboardResponse();
  request: Request = new Request();

  constructor(private ideaService: IdeasapiService, public appGlobal: AppGlobal,
    private ref: ChangeDetectorRef, private spinner: NgxSpinnerService, private toastr: ToastrService) { }

  ngOnInit() {
    //Call API to load dashboard
    this.spinner.show();
    this.ideaService.GetDashboard(this.request).subscribe(res => {
      if (res.isSuccess) {
        drawDashboard(res.dashboard);
      }
      else {
        this.toastr.error(res.message, this.appGlobal.Error);
      }
    });

    //Load text animation
    $('#main-flexslider').flexslider({
      animation: "swing",
      direction: "vertical",
      slideshow: true,
      slideshowSpeed: 3500,
      animationDuration: 1000,
      directionNav: true,
      prevText: '<i class="icon-angle-up icon-2x"></i>',
      nextText: '<i class="icon-angle-down icon-2x active"></i>',
      controlNav: false,
      smootheHeight: true,
      useCSS: false
    });
  }

}
