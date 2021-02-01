import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentBehaviourComponent } from './student-behaviour.component';

describe('StudentBehaviourComponent', () => {
  let component: StudentBehaviourComponent;
  let fixture: ComponentFixture<StudentBehaviourComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentBehaviourComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentBehaviourComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
