import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  addNewCity(model: any) {
    return this.http.post(this.baseUrl + 'city/add-city', model)
  }

  deleteCity(id: any) {
    return this.http.delete(this.baseUrl + 'city/delete-city/' + id)
  }

  addNewWeather(model: any) {
    return this.http.post(this.baseUrl + 'city/add-weather', model);
  }

  deleteWeather(id: any) {
    return this.http.delete(this.baseUrl + 'city/delete-weather/' + id)
  }

  updateWeather(model: any) {
    return this.http.put(this.baseUrl + 'city/update', model);
  }
}
