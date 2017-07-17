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
var http_service_1 = require("../services/http.service");
var ValidationService = (function () {
    function ValidationService(_httpService) {
        this._httpService = _httpService;
        this._validationRulesUrl = 'api/validation/validateRules/';
    }
    ValidationService.prototype.get = function (modelType) {
        return this._httpService.get(this._validationRulesUrl + modelType)
            .then(function (x) { return x.json(); });
    };
    return ValidationService;
}());
ValidationService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_service_1.HttpService])
], ValidationService);
exports.ValidationService = ValidationService;
//# sourceMappingURL=validation.service.js.map