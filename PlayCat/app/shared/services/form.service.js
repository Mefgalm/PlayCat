"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var FormService = (function () {
    function FormService(_fb) {
        this._fb = _fb;
        this.Required = 'required';
        this.Pattern = 'pattern';
        this.DefaultValidations = new Array(this.Required, this.Pattern);
        this.Compare = 'compare';
        this.Validator = 'validator';
    }
    FormService.prototype.markControlsAsDirty = function (formGroup) {
        for (var _i = 0, _a = Object.keys(formGroup.controls); _i < _a.length; _i++) {
            var control = _a[_i];
            var formControl = formGroup.controls[control];
            if (formControl !== null) {
                formControl.markAsDirty();
            }
        }
    };
    FormService.prototype.getValidation = function (key, value) {
        if (key === this.Required) {
            return forms_1.Validators.required;
        }
        if (key === this.Pattern) {
            return forms_1.Validators.pattern(value.validationValue);
        }
        return null;
    };
    FormService.prototype.isDefaultValidation = function (key) {
        return !!this.DefaultValidations.find(function (x) { return x === key; });
    };
    FormService.prototype.isNotDefaultValidation = function (key) {
        return !this.isDefaultValidation(key);
    };
    FormService.prototype.matchingPasswords = function (passwordKey, passwordConfirmationKey) {
        return function (group) {
            var passwordInput = group.controls[passwordKey];
            var passwordConfirmationInput = group.controls[passwordConfirmationKey];
            if (passwordInput.value !== passwordConfirmationInput.value) {
                return passwordConfirmationInput.setErrors({ compare: true });
            }
        };
    };
    FormService.prototype.buildFormGroup = function (errorsValidation) {
        var formGroup = {};
        var additionalGroupValidation = {};
        for (var key in errorsValidation) {
            var value = errorsValidation[key];
            var properties = Object.keys(value);
            if (properties.length == 0) {
                formGroup[key] = [null];
            }
            else {
                var validationArray = new Array();
                for (var index in properties) {
                    var propKey = properties[index];
                    if (this.isDefaultValidation(propKey)) {
                        validationArray.push(this.getValidation(propKey, value[propKey]));
                    }
                    else {
                        additionalGroupValidation[this.Validator] = this.matchingPasswords(value[propKey].validationValue, key);
                    }
                }
                if (validationArray.length > 1) {
                    formGroup[key] = [null, forms_1.Validators.compose(validationArray)];
                }
                else {
                    formGroup[key] = [null, validationArray[0]];
                }
            }
        }
        return this._fb.group(formGroup, additionalGroupValidation);
    };
    return FormService;
}());
FormService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [forms_1.FormBuilder])
], FormService);
exports.FormService = FormService;
//# sourceMappingURL=form.service.js.map