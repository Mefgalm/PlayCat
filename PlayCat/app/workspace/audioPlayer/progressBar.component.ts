import { Component, ViewChild, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'progressBar',
    templateUrl: './app/workspace/audioPlayer/progressBar.component.html',
    styleUrls: ['./app/workspace/audioPlayer/progressBar.component.css'],
})
export class ProgressBarComponent {
    @ViewChild('outer') outer;
    @ViewChild('inner') inner;

    @Input('min') min: number = 0;
    @Input('max') max: number = 100;

    @Input('width') width: number = 300;
    @Input('height') height: number = 20;

    @Input('horizontal-padding') horizontalPadding: number = 20;
    @Input('vertical-padding') verticalPadding: number = 5;

    @Output() onClick = new EventEmitter<number>();

    constructor() {
        
    }

    ngAfterViewInit() {
        //this.outer.nativeElement.style.width = this.width + 'px';
        //this.outer.nativeElement.style.height = this.height + 'px';

        //this.inner.nativeElement.style.width = (this.width - this.horizontalPadding * 2) + 'px';
        //this.inner.nativeElement.style.height = (this.height - this.verticalPadding * 2) + 'px';

        //this.inner.nativeElement.style.top = this.verticalPadding + 'px';
        //this.inner.nativeElement.style.left = this.horizontalPadding + 'px';

        //console.log(this.max);
    }

    onSeek(value: number) {
        
    }
}