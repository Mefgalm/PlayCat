export class UrlInfo {
    public contentLength: number;
    public artist: string;
    public song: string;

    constructor(contentLength: number, artist: string, song: string) {
        this.contentLength = contentLength;
        this.artist = artist;
        this.song = song;
    }
}