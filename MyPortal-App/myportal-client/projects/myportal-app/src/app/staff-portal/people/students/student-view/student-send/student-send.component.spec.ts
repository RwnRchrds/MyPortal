import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentSendComponent } from './student-send.component';

describe('StudentSendComponent', () => {
  let component: StudentSendComponent;
  let fixture: ComponentFixture<StudentSendComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentSendComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentSendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
