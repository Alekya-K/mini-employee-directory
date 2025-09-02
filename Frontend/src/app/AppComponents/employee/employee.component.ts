import { Component, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeDialogComponent } from '../employee-dialog/employee-dialog.component';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { DialogBoxComponent } from '../../AppComponent/dialog-box/dialog-box.component';


@Component({
  selector: 'app-employee',
  standalone: true, 
  imports: [CommonModule],
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent {
   private modalService = inject(NgbModal)
   httpClient=inject( HttpClient)
   employeeDetails:any;

  openEmployeeDialog(){
     this.modalService.open(EmployeeDialogComponent).result.then(data=>{
      if(data.event=="closed"){
        this.getEmployeeDetails();
      }
     });
 }
 ngOnInit()
 {
 this.getEmployeeDetails();
 }

 getEmployeeDetails(){
   let apiUrl="https://localhost:7137/api/Employee";
   this.httpClient.get(apiUrl).subscribe(result=>{
    this.employeeDetails=result;
    console.log(this.employeeDetails);
   }

   )
 }
 openConfirmDialog(employeeId:any){
  this.modalService.open(DialogBoxComponent).result.then(data=> {
     console.log(data);
      if(data.event == "confirm"){
       this. deleteEmployeeDetails(employeeId);
        
      }
    });
 }
deleteEmployeeDetails(employeeId: any){
  let apiUrl="https://localhost:7137/api/Employee?employeeId=";
  this.httpClient.delete(apiUrl+employeeId).subscribe(data=>{
    this.getEmployeeDetails();
  });
}
openEditDialogBox(employee:any){
  const mmodalReference = this.modalService.open(EmployeeDialogComponent);
  mmodalReference.componentInstance.employee = {
    employeeId : employee.employeeId,
    fullName: employee.fullName,
    department: employee.department,
    phone: employee.phone,
    email: employee.email,
    hireDate: employee.hireDate

  };
  mmodalReference.result.then(data=>{
    if(data.event == "closed"){
       this. getEmployeeDetails();
        
      }
  })

}
}