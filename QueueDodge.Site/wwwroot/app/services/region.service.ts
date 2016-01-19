import {Injectable} from 'angular2/core';
import {Http, Response, HTTP_PROVIDERS} from 'angular2/http';

@Injectable()
export class RegionService {
    private http: Http;

    constructor(http: Http) {
        this.http = http;
    }

    getRegions() {
        return this
            .http
            .get("api/region")
            .map((res: Response) => res.json());
    }
}