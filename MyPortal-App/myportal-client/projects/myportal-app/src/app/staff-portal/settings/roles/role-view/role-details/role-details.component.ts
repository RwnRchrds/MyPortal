import {Component, OnInit} from '@angular/core';
import {RoleViewService} from '../role-view.service';

@Component({
  selector: 'app-role-details',
  templateUrl: './role-details.component.html',
  styleUrls: ['./role-details.component.css']
})
export class RoleDetailsComponent implements OnInit {

  viewService: RoleViewService;

  constructor(roleViewService: RoleViewService) {
    this.viewService = roleViewService;
  }

  ngOnInit(): void {
    this.viewService.loadPermissionsTree();
  }

}
