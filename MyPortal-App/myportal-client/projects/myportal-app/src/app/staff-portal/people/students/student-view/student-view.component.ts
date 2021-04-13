import {AfterViewInit, Component, ElementRef, OnDestroy, OnInit, Renderer2} from '@angular/core';
import {PortalViewDirective} from '../../../../_directives/portal-view/portal-view.directive';
import {StudentModel} from 'myportal-api';
import {Subscription} from 'rxjs';
import {StudentViewService} from './student-view.service';
import {map} from 'rxjs/operators';
import {ActivatedRoute, Router} from '@angular/router';
import {ScriptService} from '../../../../_services/script.service';

@Component({
  selector: 'app-student-view',
  templateUrl: './student-view.component.html',
  styleUrls: ['./student-view.component.css']
})
export class StudentViewComponent extends PortalViewDirective implements OnInit, OnDestroy {

  student: StudentModel;
  studentSubscription: Subscription;

  constructor(protected renderer: Renderer2, protected hostElement: ElementRef,
              private viewService: StudentViewService,
              private route: ActivatedRoute, private scriptService: ScriptService) {
    super(renderer, hostElement);
  }

  ngOnInit(): void {
    this.scriptService.loadScript('/assets/lib/js/pages/custom/profile/profile.js').then(result =>
    {
      this.viewService.init(this.route.snapshot.paramMap.get('studentId'));
      this.studentSubscription = this.viewService.currentStudent.pipe(map((student: StudentModel) => {
        this.student = student;
        if (student) {
          setTimeout(this.initSidebar, 0);
        }
      })).subscribe();
    });
  }

  initSidebar(): void {
    // Used to enable the mobile sidebar toggle
    // @ts-ignore
    KTProfile.init();
    console.log('init sidebar');
  }

  ngOnDestroy(): void {
    this.viewService.reset();
    super.ngOnDestroy();
  }
}
