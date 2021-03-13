import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {NewEntityResponse, RolesService} from 'myportal-api';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';
import {BaseFormDirective} from '../../../../../_directives/base-form/base-form.directive';

@Component({
  selector: 'app-create-role',
  templateUrl: './create-role.component.html',
  styleUrls: ['./create-role.component.css']
})
export class CreateRoleComponent extends BaseFormDirective implements OnInit {

  get code(): AbstractControl {
    return this.form.get('code');
  }

  get name(): AbstractControl {
    return this.form.get('name');
  }

  constructor(private roleService: RolesService) {
    super();
  }

  ngOnInit(): void {
    this.componentName = 'new_role';
    this.form = new FormGroup({
      code: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required])
    });
  }

  goBack(): void {
    this.form.reset();
    this.router.navigate(['/staff/settings/roles']);
  }

  submit(): void {
    if (this.validate()) {
      this.roleService.createRole({name: this.code.value, description: this.name.value}).pipe(map((response: NewEntityResponse) => {
        this.router.navigate([`/staff/settings/roles/${response.id}`]);
      }), catchError((err: HttpErrorResponse) => {
        this.alertService.error(err.error);
        console.log(err);
        return throwError(err);
      })).subscribe();
    }
  }
}
