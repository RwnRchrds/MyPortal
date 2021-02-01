import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentCurriculumComponent } from './student-curriculum.component';

describe('StudentCurriculumComponent', () => {
  let component: StudentCurriculumComponent;
  let fixture: ComponentFixture<StudentCurriculumComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentCurriculumComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentCurriculumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
