import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentCommunicationComponent } from './student-communication.component';

describe('StudentCommunicationComponent', () => {
  let component: StudentCommunicationComponent;
  let fixture: ComponentFixture<StudentCommunicationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentCommunicationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentCommunicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
