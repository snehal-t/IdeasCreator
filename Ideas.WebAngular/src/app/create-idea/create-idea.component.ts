import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { JustificationValidator } from './ideal-justification-validator';
import { Idea, Request } from '../idea';
import { IdeasapiService } from '../services/ideasapi.service';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr'; 

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

  constructor(private formBuilder: FormBuilder, private ideaService: IdeasapiService, private toastr: ToastrService) { }

  ngOnInit() {
    //Initialize form
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

    //Form Idea
    this.idea.type = formValue.category;
    this.idea.source = formValue.source;
    this.idea.title = formValue.title;
    this.idea.ideaName = formValue.title;
    this.idea.description = formValue.brief;
    this.idea.businessCase = formValue.businessCase;
    this.idea.idealTime = formValue.idealTime;
    this.idea.businessJustification = formValue.idealTimeJustification;
    this.idea.contactName = formValue.contactName;
    this.idea.contactEmail = formValue.contactEmail;
    this.idea.contactMobileNo = formValue.contactNumber;

    //Send it in Request
    this.request.idea = this.idea;

    //Call API
    this.ideaService.CreateIdea(this.request).subscribe(res => {
      if (res.isSuccess) {
        this.ideaForm.reset();
        this.submitted = false;
        this.toastr.success(res.message, "Success")
      }
      else {
        this.toastr.error(res.message, "Error");
      }
    });
  }

  onReset() {
    this.submitted = false;
    this.ideaForm.reset();
  }
   
}
