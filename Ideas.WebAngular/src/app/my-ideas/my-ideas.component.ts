import { Component, OnInit } from '@angular/core';
import { AppGlobal } from '../config/appglobal';

@Component({
  selector: 'app-my-ideas',
  templateUrl: './my-ideas.component.html',
  styleUrls: ['./my-ideas.component.css']
})
export class MyIdeasComponent implements OnInit {

  title: string;
  section: string;
  constructor(private appGlobal: AppGlobal) { }

  ngOnInit() {
    this.title = this.appGlobal.myIdeaTitle;
    this.section = this.appGlobal.myIdeaSection;
  }
}
