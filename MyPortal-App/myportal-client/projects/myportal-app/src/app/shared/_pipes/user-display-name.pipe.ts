import {Pipe, PipeTransform} from '@angular/core';
import {UserModel} from 'myportal-api';
import {NameFormatPipe} from './name-format.pipe';
import {NameFormats} from '../../_constants/name-formats';

@Pipe({
  name: 'userDisplayName'
})
export class UserDisplayNamePipe implements PipeTransform {

  constructor(private nameFormatPipe: NameFormatPipe) {
  }

  transform(value: UserModel): string {
    if (value.person == null){
      return value.userName;
    }

    return this.nameFormatPipe.transform(value.person, NameFormats.FullNameAbbreviated);
  }

}
