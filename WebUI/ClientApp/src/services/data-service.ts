// import 'whatwg-fetch'
import { inject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';

@inject(HttpClient)
export class DataService {
    private http: HttpClient;

    constructor(http: HttpClient) {
        this.http = http;

        this.http.configure(config => {
            config
                .withDefaults({
                    headers: {
                        'Accept': 'application/json',
                        'X-Requested-With': 'Fetch'
                    }
                });
        });
    }

    private checkResponse(response: Response) {
        if (response.status >= 200 && response.status < 300) {
            return response;
        } else {
            let error = new Error(response.statusText || response.status.toString());
            (error as any).response = response;
            throw error;
        }
    }


    getRuntimes(): Promise<any> {
        let path = `runtimeenvironment`;
        return this.getJson(path);
    }

    private getJson(path: string): Promise<any> {
        return this.http.fetch(path, {
            method: 'get'
        })
            .then((response: Response) => this.checkResponse(response))
            .then((response: Response) => {
                return response.json();
            })
            .catch(error => {
                throw error;
            });
    }
}