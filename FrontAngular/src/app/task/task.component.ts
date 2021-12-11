import { Component, OnInit, ViewChild } from '@angular/core';
import { TaskToDo } from '../models/task';
import { Priority } from '../models/priority';
import { TaskService } from '../service/task.service';
import { PriorityService } from '../service/priority.service';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  displayedColumns: string[] = ['done', 'description', 'priority', 'update', 'delete'];
  dataSource: TaskToDo[] = [];
  priorities: Priority[] = [];
  task: TaskToDo = {
    Completed: false,
    Id: 0,
    Description: "",
    PriorityId: 0
  };
  selectedValue = 0;
  disabled = false;

  constructor(private taskService: TaskService, private priorityService: PriorityService) {


  }

  ngOnInit(): void {
    this.getTasks();
    this.getPriorities();
  }

  save(id:number, description:string, priority:number, completed:boolean) {
    this.task = {
      Completed: completed,
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
    this.selectedValue = 0;
  }

  actionBtnUpdate(element: any) {
    $("#id").val(element["Id"] as string);
    $("#description").val(element["Description"]);
    this.selectedValue = element["PriorityId"];
  }
  completeTask(element: any){
    this.disabled = true;
    this.save(element.Id,element.Description,element.PriorityId,true)
  }

  updateTask(task: TaskToDo): void {
    this.taskService.updateTask(task).subscribe(response => {
      this.getTasks();
    });
  }

  getTasks(): void {
    this.taskService.getTasks().subscribe(task => this.dataSource = task);
  }

  getPriorities(): void {
    this.priorityService.getPriorities().subscribe(priorities => this.priorities = priorities);
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
