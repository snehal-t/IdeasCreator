import { Component, OnInit } from '@angular/core';
import { AppGlobal } from '../config/appglobal';

@Component({
  selector: 'app-open-ideas',
  templateUrl: './open-ideas.component.html',
  styleUrls: ['./open-ideas.component.css']
})
export class OpenIdeasComponent implements OnInit {

  title: string;
  section: string;
  constructor(private appGlobal: AppGlobal) { }

  ngOnInit() {
    this.title = this.appGlobal.newIdeaTitle;
    this.section = this.appGlobal.newIdeaSection;
  }

}
