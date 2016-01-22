import {Injectable} from 'angular2/core';
import {Http, Response, HTTP_PROVIDERS} from 'angular2/http';

@Injectable()
export class LeaderboardService {
    private http: Http;
    
    constructor(http: Http) {
        this.http = http;
    }
}