import {Pipe, PipeTransform} from '@angular/core';
import {PersonModel} from 'myportal-api';
import {NameFormats} from '../../_constants/name-formats';

@Pipe({
  name: 'nameFormat'
})
export class NameFormatPipe implements PipeTransform {

  transform(value: PersonModel, format: NameFormats = NameFormats.Default): string {
    if (value == null) {
      return '';
    }
    let name: string;
    switch (format) {
      case NameFormats.FullNameAbbreviated:
        name = `${value.title} ${value.firstName.substr(0, 1)} ${value.middleName != null ? value.middleName.substr(0, 1) : ''} ${value.lastName}`;
        break;
      case NameFormats.FullName:
        name = `${value.title} ${value.firstName} ${value.middleName} ${value.lastName}`;
        break;
      default:
        name = `${value.lastName}, ${value.firstName}`;
    }

    return name.trim().replace('  ', ' ');
  }

}
