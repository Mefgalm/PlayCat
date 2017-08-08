import { BaseResult } from '../response/baseResult';
import { UrlInfo } from '../urlInfo';

export class GetInfoResult extends BaseResult {
    public urlInfo: UrlInfo;
}