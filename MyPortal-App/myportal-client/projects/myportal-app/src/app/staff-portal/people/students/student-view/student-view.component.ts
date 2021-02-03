import {Component, ElementRef, OnDestroy, OnInit, Renderer2} from '@angular/core';
import {PortalViewDirective} from '../../../../shared/portal-view/portal-view.directive';
import {StudentModel} from 'myportal-api';
import {Subscription} from 'rxjs';
import {StudentViewService} from './student-view.service';
import {map} from 'rxjs/operators';
import {ActivatedRoute, Router} from '@angular/router';
import {ScriptService} from '../../../../_services/script.service';
import {AuthService} from '../../../../_services/auth.service';
import {AppPermissions} from '../../../../_constants/app-permissions';

@Component({
  selector: 'app-student-view',
  templateUrl: './student-view.component.html',
  styleUrls: ['./student-view.component.css']
})
export class StudentViewComponent extends PortalViewDirective implements OnInit, OnDestroy {

  student: StudentModel;
  studentSubscription: Subscription;

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef, private viewService: StudentViewService,
              private route: ActivatedRoute, private scriptService: ScriptService, private authService: AuthService) {
    super(renderer, hostElement);
  }

  ngOnInit(): void {
    this.scriptService.loadScript('../../../../../assets/lib/js/pages/custom/profile/profile.js');
    this.viewService.init(this.route.snapshot.paramMap.get('id'));
    this.studentSubscription = this.viewService.currentStudent.pipe(map((student: StudentModel) => {
      this.student = student;
    })).subscribe();
  }

  ngOnDestroy(): void {
    this.viewService.reset();
    super.ngOnDestroy();
  }
}
