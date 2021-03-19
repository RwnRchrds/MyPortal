import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentBasicDetailsComponent } from './student-basic-details.component';

describe('StudentBasicDetailsComponent', () => {
  let component: StudentBasicDetailsComponent;
  let fixture: ComponentFixture<StudentBasicDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentBasicDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentBasicDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
