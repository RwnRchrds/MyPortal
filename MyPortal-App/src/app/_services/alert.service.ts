import { Injectable } from '@angular/core';

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
}
