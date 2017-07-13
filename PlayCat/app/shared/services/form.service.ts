import { Injectable } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Injectable()
export class FormService {
    public markControlsAsDirty(formGroup: FormGroup) {        
        for (let control of Object.keys(formGroup.controls)) {
            let formControl = formGroup.controls[control] as FormControl;
            if (formControl !== null) {
                formControl.markAsDirty();
            }
        }
    }
}