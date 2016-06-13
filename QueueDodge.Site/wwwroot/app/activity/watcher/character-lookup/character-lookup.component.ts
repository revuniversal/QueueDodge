import {Component, Input, OnInit} from "@angular/core";
import {CORE_DIRECTIVES} from "@angular/common";
import {Http, Response, HTTP_PROVIDERS} from "@angular/http";
import {Observable} from "rxjs";
import {Character} from '../../../models/Character';
import {Realm} from '../../../models/Realm';

@Component({
    selector: "character-lookup",
    templateUrl: "../app/activity/watcher/character-lookup/character-lookup.component.html",
    directives: [CORE_DIRECTIVES],
    styles: [`
    input{
        color:black;
    } 
    ul{
        list-style-type:none;
    }
    ul > li{
        background-color:#393F4C;
    }
   ul > li:hover{
        background-color:#262626;
    }
    `]
})
export class CharacterLookupComponent implements OnInit {
    @Input() region: string;

    private http: Http;

    public characters: Observable<any>;
    public realms: Observable<any>;
    public realm: string;

    public constructor(http: Http) {
        this.http = http;
        this.realm = "All";
    }

    public ngOnInit() {
        this.realms = this.getRealms();
        this.characters = this.getCharacters();
    }

    // TODO:  put http call in realm service.
    public getRealms() {
        return this.http
            .get("api/region/" + this.region + "/realm")
            .map((response: Response) => response.json());
    }
    public getCharacters(){
        const txt = document.getElementById("character");
        let keyups$ = Observable.fromEvent(txt, "keyup");
        let letters$ = keyups$.map((ev:any) => ev.target.value);
        return letters$.switchMap(p => this.http
        .get("api/region/" + this.region + "/realm/" + this.realm + "/character/" + p))
        .map((res:Response) => res.json())
        .catch((err:any, caught:Observable<any>) => caught);
    }
    public selectCharacter(character: Character){
        alert(character.name);
    }
    public onRealmChange(realm:string){
        this.realm=realm;
    }
}