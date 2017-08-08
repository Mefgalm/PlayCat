export class UploadAudioRequest {
    public artist: string;
    public song: string;
    public url: string;

    constructor(artist: string, song: string, url: string) {
        this.artist = artist;
        this.song = song;
        this.url = url;
    }
}