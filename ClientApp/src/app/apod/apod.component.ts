import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-apod',
  templateUrl: './apod.component.html'
})
export class ApodComponent {
  public apod: MarsRoverApod;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    console.log("baseUrl:", baseUrl);
    http.get<MarsRoverApod>(baseUrl + 'MarsRover/GetApod').subscribe(result => {
      this.apod = result;
      console.log("result:", result);
    }, error => console.error(error));
  }
}

interface MarsRoverApod {
        date: string,
        explanation: string,
        title: string,
        url: string,
        hdurl: string,
        mediatype: string
}
