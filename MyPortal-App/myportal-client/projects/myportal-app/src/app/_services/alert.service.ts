import { Injectable } from '@angular/core';
import {SweetAlertResult} from '../_models/sweetAlertResult';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

constructor() { }

error(message: string): void {
  // @ts-ignore -- using SweetAlert
  Swal.fire({
    title: 'Error',
    text: message,
    icon: 'error'
  });
}

success(message: string): void {
  // @ts-ignore -- using SweetAlert
  Swal.fire({
    title: 'Success',
    text: message,
    icon: 'success'
  });
}

warning(message: string): void {
  // @ts-ignore -- using SweetAlert
  Swal.fire({
    title: 'Warning',
    text: message,
    icon: 'warning'
  });
}

info(message: string): void {
  // @ts-ignore -- using SweetAlert
  Swal.fire({
    title: 'Information',
    text: message,
    icon: 'info'
  });
}

areYouSure(message: string): Promise<SweetAlertResult> {
  // @ts-ignore -- using SweetAlert
  return Swal.fire({
    title: 'Are you sure?',
    text: message,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Yes',
    cancelButtonText: 'No',
    reverseButtons: true
  });
}
}
