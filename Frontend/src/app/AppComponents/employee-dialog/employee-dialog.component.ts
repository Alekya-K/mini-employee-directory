import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, inject, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-employee-dialog',
  imports: [FormsModule, CommonModule],
  templateUrl: './employee-dialog.component.html',
  styleUrl: './employee-dialog.component.css'
})
export class EmployeeDialogComponent {
  @Input() private employee:any;

  btnText: string="Add";
  disableEmployeeIdInput = false;

httpClient = inject(HttpClient)

modal=inject(NgbActiveModal)

employeeDetails={
  employeeId:"",
  fullName:"",
  department:"",
  email:"",
  phone:"",
  hireDate:""
}
  onSubmit(){
    let apiUrl="https://localhost:7137/api/Employee";

    let httpOptions={
      headers:new HttpHeaders({
        Authorization:'my-auth-token',
        'Content-Type': 'application/json'


      })
    }
    if(this.disableEmployeeIdInput==true){
       this.httpClient.put(apiUrl, this.employeeDetails, httpOptions).subscribe({
  next: (v) => console.log(v),
  error: (e) => console.log(e),
  complete: () => {
    alert("Employee details updated successfully: " + JSON.stringify(this.employeeDetails));

    this.modal.close({event:"closed"});
  }
});

    }
    else {
      this.httpClient.post(apiUrl, this.employeeDetails, httpOptions).subscribe({
  next: (v) => console.log(v),
  error: (e) => console.log(e),
  complete: () => {
    alert("Employee details saved successfully: " + JSON.stringify(this.employeeDetails));

    this.modal.close({event:"closed"});
  }
});


    }
    

  }
  ngOnInit(){
    if(this.employee!=null){
     this.employeeDetails = this.employee;

     this.btnText = "Update";
      this.disableEmployeeIdInput = true;
    }
  
  
}
}
