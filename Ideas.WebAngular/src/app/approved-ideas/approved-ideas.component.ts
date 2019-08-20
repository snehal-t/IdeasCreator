import { Component, OnInit } from '@angular/core';
import { AppGlobal } from '../config/appglobal';

@Component({
  selector: 'app-approved-ideas',
  templateUrl: './approved-ideas.component.html',
  styleUrls: ['./approved-ideas.component.css']
})
export class ApprovedIdeasComponent implements OnInit {

  title: string;
  section: string;
  constructor(private appGlobal: AppGlobal) { }

  ngOnInit() {
    this.title = this.appGlobal.approvedIdeaTitle;
    this.section = this.appGlobal.approvedIdeaSection;
  }

}
