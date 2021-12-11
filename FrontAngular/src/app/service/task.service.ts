import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { TaskToDo } from '../models/task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private taskUrl = 'https://localhost:44303/api/task';
  constructor(private http: HttpClient) {
  }

  getTasks(): Observable<TaskToDo[]> {
    var task = this.http.get<TaskToDo[]>(this.taskUrl);
    return task;
  }
  getTask(id: number): Observable<TaskToDo> {
    const url = `${this.taskUrl}/${id}`;
    return this.http.get<TaskToDo>(url);
  }
  deleteTask(id: number): boolean {
    return true
  }
}
