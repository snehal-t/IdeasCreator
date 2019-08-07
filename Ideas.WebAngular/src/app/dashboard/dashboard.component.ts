import { Component, OnInit } from '@angular/core';
declare var $: any;

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor() { }

  ngOnInit() {
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
