export class AddRemovePlaylistRequest {
    constructor(
        public playlistId: string,
        public audioId: string
    ) {}
}