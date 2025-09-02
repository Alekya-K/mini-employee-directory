import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap'
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';

@Component({
  selector: 'app-department',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']  
})
export class DepartmentComponent {
  httpClient = inject(HttpClient);
   departmentIdToDelete: number=0;
  private modalService=inject(NgbModal)

  disableDepartmentIdInput: boolean = false;
  departmentData = {
    departmentId: "",
    departmentName: "",
    location: ""
  };

  departmentDetails: any;

  ngOnInit(): void{
    this.getDepartmentDetails(); // add new function 
  }
  
  getDepartmentDetails(){
    let apiUrl = "https://localhost:7137/api/Department";
    this.httpClient.get(apiUrl).subscribe(data=>{
        this.departmentDetails=data;
        console.log(this.departmentDetails);
    });
    this.departmentData={
      departmentId: "",
      departmentName: "",
      location: ""
    }
    this.disableDepartmentIdInput == false;
  }
  onSubmit(): void {
    let apiUrl = "https://localhost:7137/api/Department";

    let httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json' 
      })
    };
    //update the data
    if(this.disableDepartmentIdInput==true) {
      this.httpClient.put(apiUrl, this.departmentData, httpOptions).subscribe({
      next: (v) => {
        console.log("Response:", v);
        alert("Form Submitted Successfully :" + JSON.stringify(this.departmentData));
        this.ngOnInit();
      },
      error: (e) => {
        console.error("Error occurred:", e);
        alert("Submission failed: " + e.message);
      }
    });
    }
    else 
      {
    this.httpClient.post(apiUrl, this.departmentData, httpOptions).subscribe({
      next: (v) => {
        console.log("Response:", v);
        alert("Form Submitted Successfully :" + JSON.stringify(this.departmentData));
        this.ngOnInit();
      },
      error: (e) => {
        console.error("Error occurred:", e);
        alert("Submission failed: " + e.message);
      }
    });
    }
  }
  openConfirmDialog(departmentId: number){
    this.departmentIdToDelete = departmentId;
    console.log(this.departmentIdToDelete)
    this.modalService.open(DialogBoxComponent).result.then(data=> {
      if(data.event == "confirm"){
       this.deleteDepartment();

      }
    });

    
  }
  deleteDepartment():void{
     let apiUrl="https://localhost:7137/api/Department?departmentId="+this.departmentIdToDelete;

     this.httpClient.delete(apiUrl).subscribe(data=>{
           this.getDepartmentDetails();
     });
    }
     depFormForEdit(department: any){
         this.departmentData.departmentId=department.departmentId;
         this.departmentData.departmentName=department.departmentName;
         this.departmentData.location=department.location;

         this.disableDepartmentIdInput = true;
     
  }
}
