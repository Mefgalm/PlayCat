import { BaseResult } from './baseResult';
import { Audiotrack } from '../audio';

export class UploadResult extends BaseResult {
    public audio: Audiotrack;
}