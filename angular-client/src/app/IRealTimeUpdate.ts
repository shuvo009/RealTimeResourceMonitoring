export interface IRealTimeUpdate {
    onUpdate(resource: string, model: any): void;
}