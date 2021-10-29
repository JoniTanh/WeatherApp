import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable()
export class WeatherService {

    constructor(private _http: HttpClient) { }

    weatherData() {
        return this._http.get("https://localhost:5001/api/cities")
            .pipe(map(result => result))
    }

    getCities() {
        this._http.get('https://localhost:5001/api/cities').subscribe(response => {
            return response;
        }, error => {
            console.log(error);
        })
    }

}