import { inject } from 'aurelia-framework';
import { DataService } from './services/data-service';
import { IServiceRuntimeEnvironment } from './runtime-env';

@inject(DataService)
export class RuntimeInfo {
    private dataService: DataService;
    runtimeEnvironments: Array<IServiceRuntimeEnvironment>;
    apiError: string;

    constructor(dataService: DataService) {
        this.dataService = dataService;
        this.refresh();
    }

    refresh() {
        this.runtimeEnvironments = [];
        this.dataService.getRuntimes()
            .then(result => {
                this.runtimeEnvironments = result;
            })
            .catch(error => {
                this.apiError = error.message;
                throw error;
            });
    }
}