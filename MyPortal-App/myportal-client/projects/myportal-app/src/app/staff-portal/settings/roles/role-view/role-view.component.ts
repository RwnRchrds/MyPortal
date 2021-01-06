import {Component, ElementRef, OnDestroy, OnInit, Renderer2} from '@angular/core';
import {PortalViewDirective} from '../../../../shared/portal-view/portal-view.directive';
import {RoleViewService} from './role-view.service';
import {ActivatedRoute} from '@angular/router';
import {ScriptService} from '../../../../_services/script.service';

@Component({
  selector: 'app-role-view',
  templateUrl: './role-view.component.html',
  styleUrls: ['./role-view.component.css']
})
export class RoleViewComponent extends PortalViewDirective implements OnInit {

  viewService: RoleViewService;

  constructor(protected route: ActivatedRoute, renderer: Renderer2,
              hostElement: ElementRef, roleViewService: RoleViewService,
              private scriptService: ScriptService) {
    super(renderer, hostElement, roleViewService);
    this.viewService = roleViewService;
  }

  ngOnInit(): void {
    this.scriptService.loadScript('../../../../../assets/lib/plugins/custom/jstree/jstree.bundle.js');
    this.scriptService.loadStyleSheet('../../../../../assets/lib/plugins/custom/jstree/jstree.bundle.css');
    const roleId = this.route.snapshot.paramMap.get('id');
    this.viewService.loadModel(roleId);
  }
}
