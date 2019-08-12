import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-idea-list-item',
  templateUrl: './idea-list-item.component.html',
  styleUrls: ['./idea-list-item.component.css']
})
export class IdeaListItemComponent implements OnInit {
  @Input() Id: string;
  @Input() Type: string;
  @Input() IdeaName: string;
  @Input() Title: string;
  @Input() Name: string;
  @Input() Moderator: string;
  @Input() Picker: string;
  @Input() CreatedDate: string;
  @Input() UpdatedDate: string;
  @Input() WatchCount: number;
  @Input() CommentCount: number;
  @Input() IdeaStatus: string;
  constructor() { }

  ngOnInit() {
  }

}
