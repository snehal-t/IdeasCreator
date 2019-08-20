import { Component, OnInit } from '@angular/core';
import { AppGlobal } from '../config/appglobal';

@Component({
  selector: 'app-rejected-ideas',
  templateUrl: './rejected-ideas.component.html',
  styleUrls: ['./rejected-ideas.component.css']
})
export class RejectedIdeasComponent implements OnInit {

  title: string;
  section: string;
  constructor(private appGlobal: AppGlobal) { }

  ngOnInit() {
    this.title = this.appGlobal.rejectedIdeaTitle;
    this.section = this.appGlobal.rejectedIdeaSection;
  }

}
