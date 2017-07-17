export class ValidationModel {
    public errorMessage: string;
    public validationValue: string;

    constructor(errorMessage: string, validationValue: string) {
        this.errorMessage = errorMessage;
        this.validationValue = validationValue;
    }
}