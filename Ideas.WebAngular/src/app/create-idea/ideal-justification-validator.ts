import { FormGroup } from '@angular/forms';

// custom validator to check that two fields match
export function JustificationValidator(idealTime: string, idealTimeJustification: string) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[idealTime];
    const matchingControl = formGroup.controls[idealTimeJustification];

    if (matchingControl.errors && !matchingControl.errors.mustMatch) {
      // return if another validator has already found an error on the matchingControl
      return;
    }

    // set error on matchingControl if validation fails
    if (control.value == "Less than 2 weeks" && matchingControl.value == "") {
      matchingControl.setErrors({ justificationValidator: true });
    } else {
      matchingControl.setErrors(null);
    }
  }
}
