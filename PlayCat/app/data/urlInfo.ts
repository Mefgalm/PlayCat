export class UrlInfo {
    public contentLenght: number;
    public artist: string;
    public song: string;

    constructor(contentLenght: number, artist: string, song: string) {
        this.contentLenght = contentLenght;
        this.artist = artist;
        this.song = song;
    }
}