import { User } from './user';

export class Audio {
    public artist: string;
    public song: string;
    public id: string;
    public accessUrl: string;
    public dateAdded: Date;
    public uploader: User;
}