import { BaseResult } from '../response/baseResult';
import { User } from '../user';
import { AuthToken } from '../authToken';

export class SignUpInResult extends BaseResult {
    user: User;
    authToken: AuthToken;
    errors: Map<string, string>;
}