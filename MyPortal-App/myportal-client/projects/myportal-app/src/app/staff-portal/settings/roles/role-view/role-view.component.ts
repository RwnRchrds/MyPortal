import {Component, ElementRef, OnDestroy, OnInit, Renderer2} from '@angular/core';
import {PortalViewDirective} from '../../../../_directives/portal-view/portal-view.directive';
import {ScriptService} from '../../../../_services/script.service';
import {RoleModel} from 'myportal-api';
import {RoleViewService} from './role-view.service';
import {Subscription} from 'rxjs';
import {ActivatedRoute} from '@angular/router';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-role-view',
  templateUrl: './role-view.component.html',
  styleUrls: ['./role-view.component.css']
})
export class RoleViewComponent extends PortalViewDirective implements OnInit, OnDestroy {

  role: RoleModel;
  subscription: Subscription;

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef,
              private scriptService: ScriptService,
              private viewService: RoleViewService, private route: ActivatedRoute) {
    super(renderer, hostElement);
  }

  ngOnInit(): void {
    const roleId = this.route.snapshot.paramMap.get('id');
    this.viewService.init(roleId);
    this.subscription = this.viewService.currentRole.pipe(map((role: RoleModel) => {
      this.role = role;
    })).subscribe();
    this.scriptService.loadScript('/assets/lib/plugins/custom/jstree/jstree.bundle.js');
    this.scriptService.loadStyleSheet('/assets/lib/plugins/custom/jstree/jstree.bundle.css');
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    this.viewService.reset();
    super.ngOnDestroy();
  }
}
