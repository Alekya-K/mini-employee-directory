import { Component, inject } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-dialog-box',
  imports: [],
  standalone: true,
  templateUrl: './dialog-box.component.html',
  styleUrls: ['./dialog-box.component.css']
})
export class DialogBoxComponent {

  modal = inject(NgbActiveModal)
   
  confirm() {
    
    this.modal.close({event:"confirm"});
  }
}

