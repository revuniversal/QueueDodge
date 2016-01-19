import {Injectable} from 'angular2/core';
import {Http, Response, HTTP_PROVIDERS} from 'angular2/http';

@Injectable()
export class CharacterService {
    private http: Http;

    constructor(http: Http) {
        this.http = http;
    }

    getCharacter(region:string, realm:string, name:string) {
        return this
            .http
            .get("api/region/" + region + "/realm/" + realm + "/" + name)
            .map((res: Response) => res.json());
    }
}