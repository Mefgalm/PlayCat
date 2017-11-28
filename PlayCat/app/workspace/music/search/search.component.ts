import { Component } from '@angular/core';
import { SearchService } from './search.service';
import { Audiotrack } from "../../../data/audio";

@Component({
    selector: 'search',
    templateUrl: './app/workspace/music/search/search.component.html',
    styleUrls: ['./app/workspace/music/search/search.component.css'],
})
export class SearchComponent {
    private skip: number;
    private take: number;

    public search: string;
    private audios: Audiotrack[];

    constructor(
        private _searchService: SearchService) {
        this.skip = 0;
        this.take = 50;
    }    

    searchChanged(value: any) {
        this._searchService.search(value, this.skip, this.take)
            .then(audioResult => {
                if (audioResult.ok) {
                    this.audios = audioResult.audios;
                    this.skip = this.audios.length;
                }
            });
    }
}