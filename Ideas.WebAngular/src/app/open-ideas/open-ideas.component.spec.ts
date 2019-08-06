import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpenIdeasComponent } from './open-ideas.component';

describe('OpenIdeasComponent', () => {
  let component: OpenIdeasComponent;
  let fixture: ComponentFixture<OpenIdeasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpenIdeasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpenIdeasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
