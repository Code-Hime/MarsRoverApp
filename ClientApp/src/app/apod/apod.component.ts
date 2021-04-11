import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser'

@Component({
  selector: 'app-apod',
  templateUrl: './apod.component.html'
})
export class ApodComponent {
  public apod: MarsRoverApod;
  public sanitizedUrl: SafeResourceUrl;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, sanitizer: DomSanitizer) {
    //console.log("baseUrl:", baseUrl);
    http.get<MarsRoverApod>(baseUrl + 'MarsRover/GetApod').subscribe(result => {
      this.apod = result;
      console.log("result:", result);
      this.sanitizedUrl = sanitizer.bypassSecurityTrustResourceUrl(this.apod.url);
      console.log('sanitizedUrl:', this.sanitizedUrl);
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
