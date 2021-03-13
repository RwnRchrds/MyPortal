import { Directive } from '@angular/core';
import {BaseComponentDirective} from './base-component/base-component.directive';
import {FormGroup} from '@angular/forms';

@Directive({
  selector: '[appBaseForm]'
})
export abstract class BaseFormDirective extends BaseComponentDirective{

  form: FormGroup;

  protected constructor() {
    super();
  }

  protected validate(): boolean {
    this.form.markAllAsTouched();
    if (this.form.invalid) {
      this.alertService.error('Please review the errors and try again.');
      return false;
    }
    return true;
  }

  protected abstract submit(): void;
}
