import { Component, OnInit, ViewChild } from '@angular/core';
import { TaskToDo } from '../models/task';
import { TaskService } from '../service/task.service';
import { MatTable } from '@angular/material/table';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  displayedColumns: string[] = ['done','description', 'update', 'delete'];
  dataSource: TaskToDo[] = [];
  constructor(private taskService: TaskService) { }

  ngOnInit(): void {
    this.getTasks()
  }

  getTasks(): void {
    this.taskService.getTasks().subscribe(task => this.dataSource = task);
  }

  deleteTask(id:number): void {
    this.deleteTask(id);
  }
}
