"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var SecondToTimePipe = (function () {
    function SecondToTimePipe() {
    }
    SecondToTimePipe.prototype.transform = function (seconds) {
        if (isNaN(seconds)) {
            seconds = 0;
        }
        var h = Math.floor(seconds / 3600);
        var m = Math.floor(seconds % 3600 / 60);
        var s = Math.floor(seconds % 3600 % 60);
        var time = '';
        var timeArray = new Array();
        if (h > 0) {
            timeArray.push(h.toString());
        }
        timeArray.push(m.toString());
        timeArray.push(s < 10 ? '0' + s : s.toString());
        return timeArray.join(':');
    };
    return SecondToTimePipe;
}());
SecondToTimePipe = __decorate([
    core_1.Pipe({
        name: 'secondToTime'
    })
], SecondToTimePipe);
exports.SecondToTimePipe = SecondToTimePipe;
//# sourceMappingURL=secondToTime.pipe.js.map