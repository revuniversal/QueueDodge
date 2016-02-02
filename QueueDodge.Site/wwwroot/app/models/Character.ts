import {Realm} from './Realm';
import {Race} from './Race';
import {Class} from './Class';
import {Specialization} from './Specialization';

export class Character{
    public name: string;
    public gender: number;
    public realm: Realm;
    public race: Race;
    public class: Class;
    public specialization: Specialization;
}