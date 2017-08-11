export class UrlInfo {
    public contentLength: number;
    public artist: string;
    public song: string;
    public videoId: string;

    constructor(contentLength: number, artist: string, song: string, videoId: string) {
        this.contentLength = contentLength;
        this.artist = artist;
        this.song = song;
        this.videoId = videoId;
    }
}