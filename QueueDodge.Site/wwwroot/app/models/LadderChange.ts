import {Realm} from './Realm';
import {LadderEntry} from './LadderEntry';
export class LadderChange {
    public id: number;
    public previous: LadderEntry;
    public current: LadderEntry;
}