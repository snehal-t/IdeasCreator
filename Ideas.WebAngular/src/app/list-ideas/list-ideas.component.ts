import { Component, OnInit } from '@angular/core';
import { AppGlobal } from '../config/appglobal';

@Component({
  selector: 'app-list-ideas',
  templateUrl: './list-ideas.component.html',
  styleUrls: ['./list-ideas.component.css']
})
export class ListIdeasComponent implements OnInit {
  title: string;
  section: string;
  constructor(private appGlobal: AppGlobal) { }

  ngOnInit() {
    this.title = this.appGlobal.viewIdeaTitle;
    this.section = this.appGlobal.viewIdeaSection;
  }

}
