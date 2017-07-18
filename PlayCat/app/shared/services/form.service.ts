import { Injectable } from '@angular/core';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { ValidationModel } from '../../data/validationModel';

@Injectable()
export class FormService {
    private Required = 'required';
    private Pattern = 'pattern';

    private DefaultValidations = new Array<string>(this.Required, this.Pattern);

    private Compare = 'compare';
    private Validator = 'validator';

    constructor(private _fb: FormBuilder) {
    }

    public markControlsAsDirty(formGroup: FormGroup) {        
        for (let control of Object.keys(formGroup.controls)) {
            let formControl = formGroup.controls[control] as FormControl;
            if (formControl !== null) {
                formControl.markAsDirty();
            }
        }
    }

    private getValidation(key: string, value: ValidationModel ): any {
        if (key === this.Required) {
            return Validators.required;
        }
        if (key === this.Pattern) {
            return Validators.pattern(value.validationValue);
        }
        return null;
    }

    private isDefaultValidation(key: string): boolean {
        return !!this.DefaultValidations.find(x => x === key);
    }

    private isNotDefaultValidation(key: string): boolean {
        return !this.isDefaultValidation(key);
    }

    private matchingPasswords(passwordKey: string, passwordConfirmationKey: string) {
        return (group: FormGroup) => {
            let passwordInput = group.controls[passwordKey];
            let passwordConfirmationInput = group.controls[passwordConfirmationKey];

            if (passwordInput.value !== passwordConfirmationInput.value) {
                return passwordConfirmationInput.setErrors({ compare: true });
            }
        }
    }

    public buildFormGroup(errorsValidation: Map<string, Map<string, ValidationModel>>): any {
        var formGroup = {};
        var additionalGroupValidation = {};
        for (let key in errorsValidation) {
            let value = errorsValidation[key];  
            
            let properties = Object.keys(value);
            if (properties.length == 0) {
                formGroup[key] = [null];
            } else {
                let validationArray = new Array();
                for (let index in properties) {
                    let propKey = properties[index];
                    if (this.isDefaultValidation(propKey)) {
                        validationArray.push(this.getValidation(propKey, value[propKey]));
                    } else {
                        additionalGroupValidation[this.Validator] = this.matchingPasswords(value[propKey].validationValue, key);                   
                    }                    
                }

                if (validationArray.length > 1) {
                    formGroup[key] = [null, Validators.compose(validationArray)];
                } else {
                    formGroup[key] = [null, validationArray[0]];
                }
            }
            
        }

        return this._fb.group(formGroup, additionalGroupValidation);
    }
}