import { Component, OnInit } from '@angular/core';
import {RoleBrowserService} from '../role-browser.service';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {NewEntityResponse, RolesService} from 'myportal-api';
import {Router} from '@angular/router';
import {AlertService} from '../../../../../_services/alert.service';
import {catchError, map} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {throwError} from 'rxjs';

@Component({
  selector: 'app-create-role',
  templateUrl: './create-role.component.html',
  styleUrls: ['./create-role.component.css']
})
export class CreateRoleComponent implements OnInit {

  viewService: RoleBrowserService;

  newRoleForm = new FormGroup({
    code: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required])
  });

  get code(): AbstractControl {
    return this.newRoleForm.get('code');
  }

  get name(): AbstractControl {
    return this.newRoleForm.get('name');
  }

  constructor(roleBrowserService: RoleBrowserService, private roleService: RolesService,
              private router: Router, private alertService: AlertService) {
    this.viewService = roleBrowserService;
  }

  ngOnInit(): void {
  }

  goBack(): void {
    this.newRoleForm.reset();
    this.viewService.showSearch();
  }

  save(): void {
    this.newRoleForm.markAllAsTouched();
    if (this.newRoleForm.invalid) {
      this.alertService.error('Please review the errors and try again.');
      return;
    }
    this.roleService.createRole({name: this.code.value, description: this.name.value}).pipe(map((response: NewEntityResponse) => {
      this.router.navigate([`/staff/settings/roles/${response.id}`]);
    }), catchError((err: HttpErrorResponse) => {
      this.alertService.error(err.error);
      console.log(err);
      return throwError(err);
    })).subscribe();
  }
}
