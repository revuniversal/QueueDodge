import {Realm} from "./Realm";
import {LadderEntry} from "./ladder-entry";

export class LadderChange {
    public id: number;
    public previous: LadderEntry;
    public current: LadderEntry;
}