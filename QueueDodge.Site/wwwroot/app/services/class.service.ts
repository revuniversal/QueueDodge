import {Injectable} from 'angular2/core';
import {Http, Response, HTTP_PROVIDERS} from 'angular2/http';

@Injectable()
export class ClassService {
    private http: Http;

    constructor(http: Http) {
        this.http = http;
    }

    getClasses() {
        return this
            .http
            .get("api/class")
            .map((res: Response) => res.json());
    }
}