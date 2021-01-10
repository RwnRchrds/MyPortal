import { Component, OnInit } from '@angular/core';
import {UserViewService} from '../user-view.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  viewService: UserViewService;

  constructor(userViewService: UserViewService) {
    this.viewService = userViewService;
  }

  ngOnInit(): void {
    this.viewService.loadRoles();
    // @ts-ignore
    // $('#user_roles').select2();
  }

}
