import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CityService } from '../_services/city.service';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-weather-data',
  templateUrl: './weather-data.component.html',
  styleUrls: ['./weather-data.component.css']
})

export class WeatherDataComponent implements OnInit {
    @ViewChild('newCity', {static: false}) newCityInput;
    cities: any;
    selectedCity: string = '';
    cityData: any;
    model: Object = {};
    selectedCityId: string = '';
    weatherForm: FormGroup;
    selectedWeatherId: string = '';
    updateForm: FormGroup;
    selectedCityDates: any = [];

    constructor(
        private http: HttpClient, 
        private cityService: CityService, 
        private fb: FormBuilder,
    ) {}

    ngOnInit() {
        this.getCities();
        this.intitializeForm();
    }

    intitializeForm(): void {
        this.weatherForm = this.fb.group({
            date: "",
            tempC: "",
            rainfall: "",
            windSpeed: ""
        })
    }

    onSubmit(): void {
        this.addNewWeather(this.weatherForm.value);
    }

    onUpdate(id, date, tempC, rainfall, windSpeed): void {
        this.updateWeather(id, date, tempC, rainfall, windSpeed);
    }

    getCities() {
        this.http.get('https://localhost:5001/api/cities').subscribe(response => {
            this.cities = response;
        }, error => {
            console.log(error);
        })
    }

    selectedCityHandler(event: any) {
        this.selectedCity = event.target.value;
        this.getSelectedCityData(this.selectedCity);
    }

    getSelectedCityData(city: any) {
        this.http.get('https://localhost:5001/api/cities/' + city.toLowerCase()).subscribe(response => {
            this.cityData = response;
            this.selectedCityId = this.cityData.id;
            
            let outputArray = [];
            for (let el in this.cityData)
            {
                outputArray.push({
                    data: this.cityData[el]
                });
            }
            let x = outputArray.flatMap(x => x.data)
            let xDates = x.flatMap(x => x.date);
            this.selectedCityDates = [];
            for (const el of xDates) 
            {
                if (el != undefined) {
                    let subEl = el.substring(0, 10)
                    this.selectedCityDates.push(subEl);
                }
            }
        }, error => {
            console.log(error);
        })
    }

    addNewCity(data: any) {
        this.cityService
            .addNewCity({
                cityName: data
            })
            .subscribe(w => {
                this.cities.push(w)
                this.newCityInput.nativeElement.value = '';
            });
    }

    deleteCity(id: any) {
        this.cityService.deleteCity(id).subscribe(() => {
            this.cities.splice(this.cities.findIndex(c => c.id === id), 1)
        })
    }

    addNewWeather(data: any) {

        if(this.selectedCityDates.indexOf(data.date) !== -1)
        {
            console.log("Weather for that day already exists")
        } else if (data.rainfall < 0 || data.windSpeed < 0) {
            console.log("Bad values given")
        } else {
            this.cityService
            .addNewWeather({
                date: data.date,
                tempC: data.tempC,
                rainfall: data.rainfall,
                windSpeed: data.windSpeed,
                appCityId: this.selectedCityId
            })
            .subscribe(w=> {
                this.cities.push(w)
                this.weatherForm.reset();
            }); 
        }
    }

    deleteWeather(id: any) {
        this.cityService.deleteWeather(id).subscribe(() => {
            console.log("deleted");
        })
    }

    updateWeather(id, date, tempC, rainfall, windSpeed) {
        if (rainfall < 0 || windSpeed < 0) {
            console.log("Bad values given")
        } else {
            this.cityService
            .updateWeather({
                id: id,
                date: date,
                tempC: tempC,
                rainfall: rainfall,
                windSpeed: windSpeed,
                appCityId: this.selectedCityId
            })
            .subscribe(data => {
                console.log("updated");
            })
        }
    }

    refreshPage(){
        console.log("clicked");
        window.location.reload();
    }
}

