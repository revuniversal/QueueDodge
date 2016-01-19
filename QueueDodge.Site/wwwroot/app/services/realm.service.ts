import {Injectable} from 'angular2/core';
import {Http, Response, HTTP_PROVIDERS} from 'angular2/http';

@Injectable()
export class RealmService {
    private http: Http;

    constructor(http: Http) {
        this.http = http;
    }

    getRealms(region:string) {
        return this
            .http
            .get("api/region/" + region + "/realm")
            .map((res: Response) => res.json());
    }
}