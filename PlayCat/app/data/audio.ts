import { User } from './user';

export class Audiotrack {
    public artist: string;
    public song: string;
    public id: string;
    public duration: number;
    public accessUrl: string;
    public dateAdded: Date;
    public uploader: User;
}