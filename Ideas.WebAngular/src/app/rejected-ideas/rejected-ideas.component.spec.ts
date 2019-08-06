import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RejectedIdeasComponent } from './rejected-ideas.component';

describe('RejectedIdeasComponent', () => {
  let component: RejectedIdeasComponent;
  let fixture: ComponentFixture<RejectedIdeasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RejectedIdeasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RejectedIdeasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
