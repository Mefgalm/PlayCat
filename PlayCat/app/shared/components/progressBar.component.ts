import { Component, ViewChild, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'progressBar',
    templateUrl: './app/shared/components/progressBar.component.html',
    styleUrls: ['./app/shared/components/progressBar.component.css'],
})
export class ProgressBarComponent {
    @ViewChild('outer') outer;
    @ViewChild('innerWrapper') innerWrapper;
    @ViewChild('inner') inner;

    @Input('min') min: number = 0;
    @Input('max') max: number = 100;

    @Input('width') width: number = 300;
    @Input('height') height: number = 20;

    @Input('currentValue') currentValue: number = 0;

    @Input('horizontal-padding') horizontalPadding: number = 0;
    @Input('vertical-padding') verticalPadding: number = 0;

    @Output() onClick = new EventEmitter<number>();

    constructor() {        
    }

    private setElementSize(element: any, width: number, height: number, top: number, left: number) {
        element.style.width = width + 'px';
        element.style.height = height + 'px';
        element.style.top = top + 'px';
        element.style.left = left + 'px';
    }

    ngOnChanges() {
        this.inner.nativeElement.style.width = (this.width / this.max) * this.currentValue + 'px';        
    }   

    ngAfterViewInit() {
        this.setElementSize(this.outer.nativeElement,
            this.width + this.horizontalPadding * 2,
            this.height + this.verticalPadding * 2,
            0,
            0);

        this.innerWrapper.nativeElement.style.width = 'calc(100% - ' + this.horizontalPadding * 2 + 'px)';
        this.innerWrapper.nativeElement.style.height = 'calc(100% - ' + this.verticalPadding * 2 + 'px)';

        this.innerWrapper.nativeElement.style['margin-top'] = this.verticalPadding + 'px';
        this.innerWrapper.nativeElement.style['margin-bottom'] = this.verticalPadding + 'px';

        this.innerWrapper.nativeElement.style['margin-left'] = this.horizontalPadding + 'px';
        this.innerWrapper.nativeElement.style['margin-right'] = this.horizontalPadding + 'px';

        this.innerWrapper.nativeElement.style['top'] = this.verticalPadding + 'px';

        this.inner.nativeElement.style.height = '100%';
    }

    onSeek(offsetX: number) {
        this.inner.nativeElement.style.width = offsetX + 'px';
        this.onClick.emit((offsetX / this.width) * (this.max - this.min));
    }
}