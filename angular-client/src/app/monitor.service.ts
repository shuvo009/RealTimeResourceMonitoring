import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { IRealTimeUpdate } from "./IRealTimeUpdate";
@Injectable()
export class MonitorService {

    private _hubConnection: HubConnection;

    constructor() {
        this.createConnection();
        this.startConnection();
    }

    subscribe(resource: string, realTimeUpdate: IRealTimeUpdate) {
        this._hubConnection.on(resource.toLowerCase(), (data: any) => {
            console.log(data);
            realTimeUpdate.onUpdate(resource, JSON.parse(data.payload));
        });

        this._hubConnection.send("Subscribe", resource);
       
    }

    unsubscribe(resource: string) {
        this._hubConnection.send("Unsubscribe", resource);
        this._hubConnection.off(resource);
    }

    private createConnection() {
        this._hubConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:5001/realtime')
            .build();
    }

    private startConnection(): void {
        this._hubConnection
            .start()
            .then(() => {
                console.log('Hub connection started');
            })
            .catch(err => {
                console.log('Error while establishing connection, retrying...');
                setTimeout(() => { this.startConnection(); }, 5000);
            });
    }
}    