import {Component, ElementRef, OnDestroy, OnInit, Renderer2} from '@angular/core';
import {UserViewService} from './user-view.service';
import {PortalViewDirective} from '../../../../_directives/portal-view/portal-view.directive';
import {ActivatedRoute} from '@angular/router';
import {Subscription} from 'rxjs';
import {map} from 'rxjs/operators';
import {UserModel} from 'myportal-api';

@Component({
  selector: 'app-user-view',
  templateUrl: './user-view.component.html',
  styleUrls: ['./user-view.component.css']
})
export class UserViewComponent extends PortalViewDirective implements OnInit, OnDestroy {

  user: UserModel;
  userSubscription: Subscription;

  showDetailsComponent: boolean;
  showLinkPersonComponent: boolean;
  showResetPasswordComponent: boolean;

  showDetailsSubscription: Subscription;
  showLinkPersonSubscription: Subscription;
  showResetPasswordSubscription: Subscription;

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef,
              private viewService: UserViewService, private route: ActivatedRoute) {
    super(renderer, hostElement);
  }

  ngOnInit(): void {
    const userId = this.route.snapshot.paramMap.get('id');
    this.viewService.init(userId);
    this.userSubscription = this.viewService.currentUser.pipe(map((user: UserModel) => {
      this.user = user;
    })).subscribe();
    this.showDetailsSubscription = this.viewService.currentShowDetails.pipe(map((showDetails: boolean) => {
      this.showDetailsComponent = showDetails;
    })).subscribe();
    this.showLinkPersonSubscription = this.viewService.currentLinkPerson.pipe(map((showLinkPerson: boolean) => {
      this.showLinkPersonComponent = showLinkPerson;
    })).subscribe();
    this.showResetPasswordSubscription = this.viewService.currentShowResetPassword.pipe(map((showResetPassword: boolean) => {
      this.showResetPasswordComponent = showResetPassword;
    })).subscribe();
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
    this.showDetailsSubscription.unsubscribe();
    this.showLinkPersonSubscription.unsubscribe();
    this.showResetPasswordSubscription.unsubscribe();
    this.viewService.reset();
    super.ngOnDestroy();
  }

}
