import { Pipe, PipeTransform } from '@angular/core';
import {AddressModel} from 'myportal-api';

@Pipe({
  name: 'postalAddress'
})
export class PostalAddressPipe implements PipeTransform {

  transform(address: AddressModel): string {
    const text = `${address.apartment ?? ''}
      ${address.houseName ?? ''}
      ${address.houseNumber ?? ''} ${address.street ?? ''}
      ${address.town ?? ''}
      ${address.county ?? ''}
      ${address.postcode ?? ''}
      ${address.country ?? ''}`;
    return text.trim();
  }

}
