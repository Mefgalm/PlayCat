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
var ProgressBarComponent = (function () {
    function ProgressBarComponent() {
        this.min = 0;
        this.max = 100;
        this.width = 300;
        this.height = 20;
        this.currentValue = 0;
        this.horizontalPadding = 0;
        this.verticalPadding = 0;
        this.onClick = new core_1.EventEmitter();
    }
    ProgressBarComponent.prototype.setElementSize = function (element, width, height, top, left) {
        element.style.width = width + 'px';
        element.style.height = height + 'px';
        element.style.top = top + 'px';
        element.style.left = left + 'px';
    };
    ProgressBarComponent.prototype.ngOnChanges = function () {
        this.inner.nativeElement.style.width = (this.width / this.max) * this.currentValue + 'px';
    };
    ProgressBarComponent.prototype.ngAfterViewInit = function () {
        this.setElementSize(this.outer.nativeElement, this.width + this.horizontalPadding * 2, this.height + this.verticalPadding * 2, 0, 0);
        this.innerWrapper.nativeElement.style.width = 'calc(100% - ' + this.horizontalPadding * 2 + 'px)';
        this.innerWrapper.nativeElement.style.height = 'calc(100% - ' + this.verticalPadding * 2 + 'px)';
        this.innerWrapper.nativeElement.style['margin-top'] = this.verticalPadding + 'px';
        this.innerWrapper.nativeElement.style['margin-bottom'] = this.verticalPadding + 'px';
        this.innerWrapper.nativeElement.style['margin-left'] = this.horizontalPadding + 'px';
        this.innerWrapper.nativeElement.style['margin-right'] = this.horizontalPadding + 'px';
        this.innerWrapper.nativeElement.style['top'] = this.verticalPadding + 'px';
        this.inner.nativeElement.style.height = '100%';
    };
    ProgressBarComponent.prototype.onSeek = function (offsetX) {
        this.inner.nativeElement.style.width = offsetX + 'px';
        this.onClick.emit((offsetX / this.width) * (this.max - this.min));
    };
    return ProgressBarComponent;
}());
__decorate([
    core_1.ViewChild('outer'),
    __metadata("design:type", Object)
], ProgressBarComponent.prototype, "outer", void 0);
__decorate([
    core_1.ViewChild('innerWrapper'),
    __metadata("design:type", Object)
], ProgressBarComponent.prototype, "innerWrapper", void 0);
__decorate([
    core_1.ViewChild('inner'),
    __metadata("design:type", Object)
], ProgressBarComponent.prototype, "inner", void 0);
__decorate([
    core_1.Input('min'),
    __metadata("design:type", Number)
], ProgressBarComponent.prototype, "min", void 0);
__decorate([
    core_1.Input('max'),
    __metadata("design:type", Number)
], ProgressBarComponent.prototype, "max", void 0);
__decorate([
    core_1.Input('width'),
    __metadata("design:type", Number)
], ProgressBarComponent.prototype, "width", void 0);
__decorate([
    core_1.Input('height'),
    __metadata("design:type", Number)
], ProgressBarComponent.prototype, "height", void 0);
__decorate([
    core_1.Input('currentValue'),
    __metadata("design:type", Number)
], ProgressBarComponent.prototype, "currentValue", void 0);
__decorate([
    core_1.Input('horizontal-padding'),
    __metadata("design:type", Number)
], ProgressBarComponent.prototype, "horizontalPadding", void 0);
__decorate([
    core_1.Input('vertical-padding'),
    __metadata("design:type", Number)
], ProgressBarComponent.prototype, "verticalPadding", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], ProgressBarComponent.prototype, "onClick", void 0);
ProgressBarComponent = __decorate([
    core_1.Component({
        selector: 'progressBar',
        templateUrl: './app/shared/components/progressBar.component.html',
        styleUrls: ['./app/shared/components/progressBar.component.css'],
    }),
    __metadata("design:paramtypes", [])
], ProgressBarComponent);
exports.ProgressBarComponent = ProgressBarComponent;
//# sourceMappingURL=progressBar.component.js.map