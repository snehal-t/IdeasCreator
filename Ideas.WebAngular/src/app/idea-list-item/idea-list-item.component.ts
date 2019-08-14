import { Component, OnInit, Input } from '@angular/core';
import { Idea } from '../idea';
@Component({
  selector: 'app-idea-list-item',
  templateUrl: './idea-list-item.component.html',
  styleUrls: ['./idea-list-item.component.css']
})
export class IdeaListItemComponent implements OnInit {
  @Input() Idea: Idea;
  constructor() { }

  ngOnInit() {
  }

}
