import { Audiotrack } from './audio';
import { User } from './user';

export class Playlist {
    public id:string;
    public title: string;
    public isGeneral: boolean;
    public owner: User;
    public audios: Audiotrack[];
}