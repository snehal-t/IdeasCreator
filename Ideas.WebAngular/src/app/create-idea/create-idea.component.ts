import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JustificationValidator } from './ideal-justification-validator';
import { Idea, Request } from '../idea';
import { IdeasapiService } from '../services/ideasapi.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-create-idea',
  templateUrl: './create-idea.component.html',
  styleUrls: ['./create-idea.component.css']
})
export class CreateIdeaComponent implements OnInit {
  idea: Idea = new Idea();
  request: Request = new Request();
  ideaForm: FormGroup;
  submitted = false;
  
  constructor(private formBuilder: FormBuilder, private ideaService: IdeasapiService) { }

  ngOnInit() {
    this.ideaForm = this.formBuilder.group({
      category: ['', [Validators.required]],
      source: ['', [Validators.required]],
      title: ['', [Validators.required, Validators.maxLength(200), Validators.minLength(2)]],
      brief: ['', [Validators.required, Validators.maxLength(4000), Validators.minLength(2)]],
      businessCase: ['', [Validators.required, Validators.maxLength(4000), Validators.minLength(2)]],
      idealTime: ['', [Validators.required]],
      idealTimeJustification: ['', [Validators.maxLength(4000)]],
      contactName: ['', [Validators.required, Validators.maxLength(100), Validators.minLength(2)]],
      contactEmail: ['', [Validators.required, Validators.email, Validators.maxLength(100)]],
      contactNumber: ['', [Validators.required]]
    }, {
        validator: JustificationValidator('idealTime', 'idealTimeJustification')
      });
  }

  get f() { return this.ideaForm.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.ideaForm.invalid) {
      return;
    }
    const formValue = this.ideaForm.value;

    //Set values in Idea object
    this.idea.Type = formValue.category;
    this.idea.Source = formValue.source;
    this.idea.Title = formValue.title;
    this.idea.Description = formValue.brief;
    this.idea.BusinessCase = formValue.businessCase;
    this.idea.IdealTime = formValue.idealTime;
    this.idea.BusinessJustification = formValue.idealTimeJustification;
    this.idea.ContactName = formValue.contactName;
    this.idea.ContactEmail = formValue.contactEmail;
    this.idea.ContactMobileNo = formValue.contactNumber;

    //Send it in Request
    this.request.Idea = this.idea;

    //Call API
    this.ideaService.CreateIdea(this.request).subscribe(res => {
      if (res.IsSuccess) {
        
      }
    });

    // display form values on success
    alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.ideaForm.value, null, 4));
  }

  onReset() {
    this.submitted = false;
    this.ideaForm.reset();
  }
   
}
