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

  displayedColumns: string[] = ['done', 'description', 'priority', 'update', 'delete'];
  dataSource: TaskToDo[] = [];
  task: TaskToDo = {
    Checked: false,
    Id: 0,
    Description: "",
    PriorityId: 0
  };

  constructor(private taskService: TaskService) {


  }

  ngOnInit(): void {
    this.getTasks();
  }

  save(id:number, description:string, priority:number) {
    this.task = {
      Checked: false,
      Id: id,
      Description: description,
      PriorityId: priority

    }
    if (this.task.Id == 0) {
      this.addTask(this.task)
    }else{
      this.updateTask(this.task)
    }
    $("#id").val('');
    $("#description").val('');
    $("#priority").val('');
  }

  actionBtnUpdate(element: any) {
    $("#id").val(element["Id"] as string);
    $("#description").val(element["Description"]);
    $("#priority").val(element["PriorityId"] as string);
  }

  updateTask(task: TaskToDo): void {
    this.taskService.updateTask(task).subscribe(response => {
      this.getTasks();
    });
  }

  getTasks(): void {
    this.taskService.getTasks().subscribe(task => this.dataSource = task);
  }

  deleteTask(id: number): void {
    this.taskService.deleteTask(id).subscribe( response => {
      this.getTasks();
    });
    
  }

  addTask(task: TaskToDo): void {
    this.taskService.addTask(task).subscribe(response => {
      this.getTasks();
    });
  }
}
