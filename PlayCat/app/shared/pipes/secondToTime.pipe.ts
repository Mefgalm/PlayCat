import { Pipe } from "@angular/core";

@Pipe({
    name: 'secondToTime'
})
export class SecondToTimePipe {
    transform(seconds: number) {
        if (isNaN(seconds)) {
            seconds = 0;
        }
        let h = Math.floor(seconds / 3600);
        let m = Math.floor(seconds % 3600 / 60);
        let s = Math.floor(seconds % 3600 % 60);

        let time = '';

        let timeArray = new Array<string>();

        if (h > 0) {
            timeArray.push(h.toString());
        }
        timeArray.push(m.toString());
        timeArray.push(s < 10 ? '0' + s : s.toString());

        return timeArray.join(':');
    }
}