import {Realm, LadderEntry} from '../index';

export class LadderChange {
    public id: number;
    public previous: LadderEntry;
    public current: LadderEntry;
}