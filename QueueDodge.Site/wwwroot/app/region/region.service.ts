import {Injectable, EventEmitter} from '@angular/core';
import {Http, Response, HTTP_PROVIDERS} from '@angular/http';

@Injectable()
export class RegionService {
    private http: Http;
    public region: string;
    public regionChanged: EventEmitter<string>;
    public regions: any;

    constructor(http: Http) {
        this.http = http;
        // TODO: default to "us".  check local storage for users default eventually.
        this.region = "us"; 
        this.regionChanged = new EventEmitter<string>();
    }

    public changeRegion(region: string) {
        this.region = region;
        this.regionChanged.emit(region);
    }
}