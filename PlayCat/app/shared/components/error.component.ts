import { Component, Input } from '@angular/core';
import { ValidationModel } from '../../data/validationModel';

@Component({
    selector: 'error',
    templateUrl: './app/shared/components/error.component.html',
    styleUrls: ['./app/shared/components/error.component.css'],
})

export class ErrorComponent {
    @Input() errorObject: Map<string, ValidationModel>;
    @Input() actualErrors: any;

    public errorMessage: string;

    constructor() {        
    }

    ngOnChanges() {        
        this.errorMessage = '';

        if (this.actualErrors) {
            for (let prop of Object.keys(this.errorObject)) {
                for (let ae of Object.keys(this.actualErrors)) {
                    if (prop == ae) {
                        this.errorMessage += this.errorObject[prop].errorMessage;
                    }
                }
            }
        }
    }
}