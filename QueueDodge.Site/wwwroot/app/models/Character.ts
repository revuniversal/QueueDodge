import {Realm} from "./realm";
import {Race} from "./race";
import {Class} from "./class";
import {Specialization} from "./specialization";

export class Character {
    public name: string;
    public gender: number;
    public realm: Realm;
    public race: Race;
    public class: Class;
    public specialization: Specialization;
}