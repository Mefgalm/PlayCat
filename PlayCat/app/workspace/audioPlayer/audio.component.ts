﻿import { Component, ViewChild, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'audiotrack',
    templateUrl: './app/workspace/audioPlayer/audio.component.html',
    styleUrls: ['./app/workspace/audioPlayer/audio.component.css'],
})

export class AudioComponent {
    @Input() artist: string;
    @Input() song: string;
    @Input() duration: number;
}