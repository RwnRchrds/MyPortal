import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogNoteFormComponent } from './log-note-form.component';

describe('LogNoteFormComponent', () => {
  let component: LogNoteFormComponent;
  let fixture: ComponentFixture<LogNoteFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LogNoteFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LogNoteFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
