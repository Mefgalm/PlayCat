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
var ErrorComponent = (function () {
    function ErrorComponent() {
    }
    ErrorComponent.prototype.ngOnChanges = function () {
        this.errorMessage = '';
        if (this.actualErrors) {
            for (var _i = 0, _a = Object.keys(this.errorObject); _i < _a.length; _i++) {
                var prop = _a[_i];
                for (var _b = 0, _c = Object.keys(this.actualErrors); _b < _c.length; _b++) {
                    var ae = _c[_b];
                    if (prop == ae) {
                        this.errorMessage += this.errorObject[prop].errorMessage;
                    }
                }
            }
        }
    };
    return ErrorComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", Map)
], ErrorComponent.prototype, "errorObject", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", Object)
], ErrorComponent.prototype, "actualErrors", void 0);
ErrorComponent = __decorate([
    core_1.Component({
        selector: 'error',
        templateUrl: './app/shared/components/error.component.html',
        styleUrls: ['./app/shared/components/error.component.css'],
    }),
    __metadata("design:paramtypes", [])
], ErrorComponent);
exports.ErrorComponent = ErrorComponent;
//# sourceMappingURL=error.component.js.map