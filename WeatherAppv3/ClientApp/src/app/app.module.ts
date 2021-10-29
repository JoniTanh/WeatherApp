import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { WeatherDataComponent } from './weather-data/weather-data.component';
import { DatePipe } from '@angular/common';
import { BarChartComponent } from './bar-chart/bar-chart.component';
import { WeatherService } from './_services/weather.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,
    WeatherDataComponent,
    BarChartComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'weather-data', component: WeatherDataComponent },
      { path: 'bar-chart', component: BarChartComponent },
    ]),
  ],
  providers: [DatePipe, WeatherService],
  bootstrap: [AppComponent]
})
export class AppModule { }
