import {Character} from './character';
import {WinLoss} from './winloss';

export class LadderEntry {
    public character: Character;
    public bracket: string;
    public ranking: number;
    public rating: number;
    public season: WinLoss;
    public weekly: WinLoss;
}