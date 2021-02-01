import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentOverviewComponent } from './student-overview.component';

describe('StudentOverviewComponent', () => {
  let component: StudentOverviewComponent;
  let fixture: ComponentFixture<StudentOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
