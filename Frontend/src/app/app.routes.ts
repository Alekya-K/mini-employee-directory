import { Routes } from '@angular/router';
import { DepartmentComponent } from './AppComponent/department/department.component';
import { EmployeeComponent } from './AppComponents/employee/employee.component';
import { HomeComponent } from './home/home.component';


export const routes: Routes = [  { path: 'home', component: HomeComponent },
    {path:'department',component:DepartmentComponent},
   
{path:'employee',component:EmployeeComponent},
{ path: '', redirectTo: '/home', pathMatch: 'full' }
];
