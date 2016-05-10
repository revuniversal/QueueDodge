import {Character} from './Character';
import {WinLoss} from './WinLoss';

export class LadderEntry {
    public character: Character;
    public bracket: string;
    public ranking: number;
    public rating: number;
    public season: WinLoss;
    public weekly: WinLoss;
}