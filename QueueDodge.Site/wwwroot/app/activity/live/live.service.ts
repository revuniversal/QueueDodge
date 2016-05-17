import {Injectable, EventEmitter} from '@angular/core';
import {Http, Response, HTTP_PROVIDERS} from '@angular/http';

import {LadderChange} from '../../models/ladder-change';

@Injectable()
export class LiveService {
    public activityDetected: EventEmitter<any>;
    private socket: WebSocket;
    private http: Http;

    constructor(http: Http) {
        this.http = http;
        this.activityDetected = new EventEmitter<any>();
    }

    public connect(bracket: string, region: string) {
        this.socket = new WebSocket("wss://localhost/ws/" + region + "/" + bracket);

        this.socket.onopen = event => this.onConnect(event, region, bracket);
        this.socket.onmessage = event => this.onMessage(event, this);
        this.socket.onclose = event => this.onClose(event, region, bracket);
        this.socket.onerror = event => this.onError(event, region, bracket);
    }

    public disconnect() {
        this.socket.close();
    }

    public onConnect(ev: Event, region: string, bracket: string) {
        console.log("connected " + region + " " + bracket);
    }
    
    public onMessage(ev: MessageEvent, service: LiveService) {
        let message: any;
        if (ev.data === "clear") {
            message = "clear"
        }
        else {
            message = JSON.parse(ev.data);
        }
        service.activityDetected.emit(message);
    }
    
    public onClose(ev: CloseEvent, region: string, bracket: string) {
        console.log("closed " + region + " " + bracket);
    }
    
    public onError(ev: Event, region: string, bracket: string) {
        console.log("error " + region + " " + bracket);
    }
    
    public getActivity(region: string, bracket: string){
        return this
            .http
            .get("api/region/" + region + "/bracket/" + bracket + "/recent")
            .map((res: Response) => res.json());
    }
}