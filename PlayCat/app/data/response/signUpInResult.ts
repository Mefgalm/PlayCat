import { BaseResult } from '../response/baseResult';
import { User } from '../user';
import { AuthToken } from '../authToken';

export class SignUpInResult extends BaseResult {
    public user: User;
    public authToken: AuthToken;
    public errors: Map<string, string>;
}