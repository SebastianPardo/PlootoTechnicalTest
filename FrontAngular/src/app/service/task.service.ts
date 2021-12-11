import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { TaskToDo } from '../models/task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private taskUrl = 'https://localhost:44303/api/task';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
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
  addTask(task: TaskToDo): Observable<TaskToDo>  {
    return this.http.post<TaskToDo>(this.taskUrl, task, this.httpOptions)
  }
  updateTask(task: TaskToDo): Observable<any> {
    return this.http.put(this.taskUrl, task, this.httpOptions)
  }
  deleteTask(id: number): Observable<TaskToDo> {
    const url = `${this.taskUrl}/${id}`;
    return this.http.delete<TaskToDo>(url, this.httpOptions)
  }
}
