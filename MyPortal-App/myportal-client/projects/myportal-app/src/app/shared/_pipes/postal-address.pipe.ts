import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'postalAddress'
})
export class PostalAddressPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
