import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-apod-dl',
  templateUrl: './apod-dl.component.html'
})
export class ApodDownloadComponent {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    //console.log("baseUrl:", baseUrl);
  }

  public DownloadAndSave() {
    this.http.post(this.baseUrl + 'MarsRover/DownloadAndSaveApods', null).subscribe(result => {
      console.log("result:", result);
    }, error => console.error(error));
  }
}
