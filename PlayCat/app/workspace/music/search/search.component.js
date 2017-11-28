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
var search_service_1 = require("./search.service");
var SearchComponent = (function () {
    function SearchComponent(_searchService) {
        this._searchService = _searchService;
        this.skip = 0;
        this.take = 50;
    }
    SearchComponent.prototype.searchChanged = function (value) {
        var _this = this;
        this._searchService.search(value, this.skip, this.take)
            .then(function (audioResult) {
            if (audioResult.ok) {
                _this.audios = audioResult.audios;
                _this.skip = _this.audios.length;
            }
        });
    };
    return SearchComponent;
}());
SearchComponent = __decorate([
    core_1.Component({
        selector: 'search',
        templateUrl: './app/workspace/music/search/search.component.html',
        styleUrls: ['./app/workspace/music/search/search.component.css'],
    }),
    __metadata("design:paramtypes", [search_service_1.SearchService])
], SearchComponent);
exports.SearchComponent = SearchComponent;
//# sourceMappingURL=search.component.js.map