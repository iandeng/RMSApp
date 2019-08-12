import { Component, Inject, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser'
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

@NgModule({
  imports: [BrowserModule, FormsModule],
  declarations: [HomeComponent],
  bootstrap: [HomeComponent],
})

export class HomeComponent {
  bookingResult: BookingResult;
  baseUrl: string;
  http: HttpClient;
  trainingName: string = "";
  startDate: Date;
  endDate: Date;
 
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
    console.log(this.baseUrl);
  }

  public bookTraining() {
    var data = { "TrainingName": this.trainingName, "StartDate": this.startDate || new Date(0), "EndDate": this.endDate || new Date(0) };
    console.log(JSON.stringify(data));
    this.http.post<BookingResult>(
      this.baseUrl + 'api/Training/BookTraining',
      JSON.stringify(data),
      {
        headers: {
          'Content-Type': 'application/json'
        }
      }).subscribe(result => {
        this.bookingResult = result;
      }, error => console.error(error));
  }
}

interface BookingResult {
  message: string;
  trainingDurationInDays: number;
  isSuccess: boolean;
}

