import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import  {MatToolbarModule} from '@angular/material/toolbar'
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select' 
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import * as $ from 'jquery';

import { AppComponent } from './app.component';
import { TaskComponent } from './task/task.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    TaskComponent
  ],
  imports: [

    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatCardModule,
    MatTableModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    HttpClientModule,
    FormsModule,
    MatSelectModule   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
