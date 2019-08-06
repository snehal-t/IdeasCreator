import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprovedIdeasComponent } from './approved-ideas.component';

describe('ApprovedIdeasComponent', () => {
  let component: ApprovedIdeasComponent;
  let fixture: ComponentFixture<ApprovedIdeasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApprovedIdeasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApprovedIdeasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
