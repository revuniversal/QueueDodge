import {Injectable, EventEmitter} from 'angular2/core';
import {Http, Response, HTTP_PROVIDERS} from 'angular2/http';

@Injectable()
export class ActivityService {
    public activityDetected: EventEmitter<any> = new EventEmitter<any>();
    private socket: WebSocket;
    private http: Http;

    constructor(http: Http) {
        this.http = http;
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
    public onMessage(ev: MessageEvent, service: ActivityService) {
        service.activityDetected.emit(JSON.parse(ev.data));
    }
    public onClose(ev: CloseEvent, region: string, bracket: string) {
        console.log("closed " + region + " " + bracket);
    }
    public onError(ev: Event, region: string, bracket: string) {
        console.log("error " + region + " " + bracket);
    }

    public getActivity(region: string, bracket: string) {
        return this
            .http
            .get("api/leaderboard/activity?region=" + region + "&bracket=" + bracket + "&locale=en_us")
            .map((res: Response) => res.json());
    }
}