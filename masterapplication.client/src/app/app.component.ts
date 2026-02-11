import { TablePaginationSettingsConfig } from '../shared-library/component/master-table/table-settings.model';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MasterTableComponent } from 'src/shared-library/component/master-table/Master-table.component';
import { SharedLibraryModule } from 'src/shared-library/shared-library.module';
import { RouterModule } from "@angular/router";

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css','../bootstrap.css'],
    imports: [SharedLibraryModule, RouterModule],
    standalone: true
})
export class AppComponent extends TablePaginationSettingsConfig implements OnInit  {
  public forecasts: WeatherForecast[] = [];
  title = 'masterapplication.client';
  isRefresh;
  constructor(private http: HttpClient) {
    super()
  }

  ngOnInit() {
    // this.getForecasts();
  }
  toggleTheme() {
  document.documentElement.classList.toggle('dark');
}

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }
  edit($event){

  }
  del($event){

  }
  getFileId($event){

  }
 columns = [
    {
      name: 'date',
      displayName: 'Date',
      isSearchable: true,
      hide: false,
      type:'text'

    },
    {
      name: 'temperatureC',
      displayName: 'Temp. (C)',
      isSearchable: true,
      hide: false,
      type: 'text',
    },
    {
      name: 'temperatureF',
      displayName: 'Temp .(F)',
      isSearchable: true,
      hide: false,
      type: 'text',
    },
    {
      name: 'summary',
      displayName: 'Summary',
      isSearchable: true,
      hide: false,
      type: 'text',
    }
  ];
}
