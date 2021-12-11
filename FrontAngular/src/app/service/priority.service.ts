import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { Priority } from '../models/priority';

@Injectable({
  providedIn: 'root'
})
export class PriorityService {

  private taskUrl = 'https://localhost:44303/api/priority';
  constructor(private http: HttpClient) { }
  getPriorities(): Observable<Priority[]> {
    var task = this.http.get<Priority[]>(this.taskUrl);
    return task;
  }
}
