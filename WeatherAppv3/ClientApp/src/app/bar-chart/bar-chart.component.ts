import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WeatherService } from '../_services/weather.service';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})

export class BarChartComponent implements OnInit {
  cities: Object;
  cityData: Object; // object
  cityData2: Object;
  wDates = [];
  tData = [];
  rData = [];
  wsData = [];
  cityNames: string[] = [];
  citiesFiltered: string[] = [];
  shownCities: string[] = [];
  hiddenCities: string[] = [];
  filterChanged: boolean = false;
  myChart: Chart;
  cityFilterStatus: boolean = true;
  minDateValue: string;
  maxDateValue: string;


  constructor(private http: HttpClient, private weatherService: WeatherService) { }

  ngOnInit() {
    this.renderChart();
    this.weatherService.weatherData()
      .subscribe(res => {
        this.cityData = res;
        this.cityData2 = res;
        this.getData();
        this.cityFilter();
        this.myChart.destroy();
        this.renderChart();
      })
  }

  renderChart() {

    this.myChart = new Chart("weatherChart", {

      type: 'bar',
      data: {
        labels: this.wDates,
        datasets: [{
          label: 'Temp (C)',
          data: this.tData,
          backgroundColor: 'rgba(255, 99, 132, 0.2)',
          borderColor: 'rgb(255, 99, 132)',
          borderWidth: 1
        },
        {
          label: 'Rainfall (mm)',
          data: this.rData,
          backgroundColor: 'rgba(54, 162, 235, 0.2)',
          borderColor: 'rgb(54, 162, 235)',
          borderWidth: 1
        },
        {
          label: 'Wind Speed (m/s)',
          data: this.wsData,
          backgroundColor: 'rgba(255, 159, 64, 0.2)',
          borderColor: 'rgb(255, 159, 64)',
          borderWidth: 1
        }
        ]
      }
    },
    );
  }

  getData() {
    let outputArray = [];
    for (let element in this.cityData) {
      outputArray.push({
        name: this.cityData[element]
      });
    }

    let x = outputArray.flatMap(x => x.name)

    // filtering away hidden cities
    if (this.hiddenCities.length > 0 && this.filterChanged === true) {
      let filt;
      for (var i = 0; i < this.hiddenCities.length; ++i) {
        filt = this.hiddenCities[i]
        function findItem(x) {
          for (var i = 0; i < x.length; ++i) {
            var obj = x[i];
            if (obj.cityName == filt) {
              return i;
            }
          }
        }
        var ind = findItem(x);
        x.splice(ind, 1);
      }
      this.filterChanged = false;
    }

    // date filter 
    for (var i = 0; i < x.length; ++i) {

      for (var i = 0; i < x.length; ++i) {
        var obj = x[i].weatherData;
        if (this.maxDateValue != undefined && this.minDateValue != undefined) {
          obj = obj.filter(o => {
            if (o.date >= this.minDateValue && o.date <= this.maxDateValue) {
              return x;
            }
          })
        }

        x[i].weatherData = [];
        x[i].weatherData = obj;
      }
    }

    // getting tempC data
    let wData = x.flatMap(x => x.weatherData);

    let wTempC = wData.flatMap(x => x.tempC);

    for (const el of wTempC) {
      this.tData.push(el);
    }

    // getting rainfall data
    let wRain = wData.flatMap(x => x.rainfall);

    for (const el of wRain) {
      this.rData.push(el);
    }

    // getting windspeed data
    let wsData = wData.flatMap(x => x.windSpeed);

    for (const el of wsData) {
      this.wsData.push(el);
    }

    // getting cities with date
    for (let i = 0; i < x.length; i++) {

      let cData = x[i];
      let cName = cData.cityName;
      let cDate = cData.weatherData.flatMap(x => x.date);

      let cAll = cDate.map(i => cName.substring(0, 1)
        .toUpperCase() + cName.substring(1) + " " + i.substring(0, 10));
      let uniqC = [...new Set(cAll)];

      for (const el of uniqC) {
        this.wDates.push(el);
      }
    }
  }

  cityFilter() {
    if (this.cityFilterStatus) {
      let outputArray2 = [];
      let x2 = [];
      for (let element in this.cityData) {
        outputArray2.push({
          name: this.cityData2[element]
        });
        let x = outputArray2.flatMap(x => x.name)

        for (let i = 0; i < x.length; i++) {
          let cData2 = x[i];
          let cName2 = cData2.cityName;
          x2.push(cName2);
        }
      }

      let uniqC2 = [...new Set(x2)];
      for (const el of uniqC2) {
        this.shownCities.push(el);
      }
      this.cityFilterStatus = false;
    }
  }

  showCityFilter(event: any) {
    this.hiddenCities.push(event.target.value)
    const index = this.shownCities.indexOf(event.target.value);
    if (index > -1) {
      this.shownCities.splice(index, 1);
    }
    this.refresh();
  }

  hiddenCityFilter(event: any) {
    this.shownCities.push(event.target.value)
    const index = this.hiddenCities.indexOf(event.target.value);
    if (index > -1) {
      this.hiddenCities.splice(index, 1);
    }
    this.refresh();
  }

  refresh() {
    this.wDates = [];
    this.tData = [];
    this.rData = [];
    this.wsData = [];
    this.myChart.destroy();
    this.renderChart();
    this.filterChanged = true;
    this.ngOnInit();
  }

  minDate(event: any) {
    this.minDateValue = '';
    this.minDateValue = event.target.value;
    this.refresh();
  }

  maxDate(event: any) {
    this.maxDateValue = '';
    this.maxDateValue = event.target.value;
    this.refresh();
  }
}
