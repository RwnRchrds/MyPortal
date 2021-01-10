import {Component, ElementRef, OnInit, Renderer2} from '@angular/core';
import {UserViewService} from './user-view.service';
import {PortalViewDirective} from '../../../../shared/portal-view/portal-view.directive';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-user-view',
  templateUrl: './user-view.component.html',
  styleUrls: ['./user-view.component.css']
})
export class UserViewComponent extends PortalViewDirective implements OnInit {

  viewService: UserViewService;

  constructor(renderer: Renderer2, hostElement: ElementRef, userViewService: UserViewService, private route: ActivatedRoute) {
    super(renderer, hostElement, userViewService);
  }

  ngOnInit(): void {
    const userId = this.route.snapshot.paramMap.get('id');
    this.viewService.loadModel(userId);
  }

}
