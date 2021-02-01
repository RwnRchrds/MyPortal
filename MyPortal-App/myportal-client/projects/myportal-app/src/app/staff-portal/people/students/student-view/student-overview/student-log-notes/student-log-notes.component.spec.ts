import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentLogNotesComponent } from './student-log-notes.component';

describe('StudentLogNotesComponent', () => {
  let component: StudentLogNotesComponent;
  let fixture: ComponentFixture<StudentLogNotesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentLogNotesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentLogNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
