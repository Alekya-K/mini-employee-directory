import { Component } from '@angular/core';
import { RouterOutlet, RouterModule } from '@angular/router';
import { DepartmentComponent } from './AppComponent/department/department.component';
import { EmployeeComponent } from './AppComponents/employee/employee.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterModule, DepartmentComponent,EmployeeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'DirectorySystem';
}
