import { Component } from '@angular/core';
import { IRealTimeUpdate } from './IRealTimeUpdate';
import { IResourceModel } from './IResourceModel';
import { MonitorService } from './monitor.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  providers: [MonitorService],
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements IRealTimeUpdate {
  resourceOne: IResourceModel;
  resourceTwo: IResourceModel;

  constructor(private monitorService: MonitorService) {
    this.resourceOne = this.newResourceModel();
    this.resourceTwo = this.newResourceModel();
  }

  onUpdate = (resource: string, model: IResourceModel) => {
    if (resource === 'ResourceServiceOne') {
      this.resourceOne = model
    } else {
      this.resourceTwo = model
    }
  }

  startMonitor(resource: string) {
    this.monitorService.subscribe(resource, this);
  }

  stopMonitor(resource: string) {
    this.monitorService.unsubscribe(resource);
    if (resource === 'ResourceServiceOne') {
      this.resourceOne = this.newResourceModel();
    } else {
      this.resourceTwo = this.newResourceModel();
    }
  }

  private newResourceModel(): IResourceModel {
    return {
      cpu: '---',
      ram: "---"
    }
  }

}
