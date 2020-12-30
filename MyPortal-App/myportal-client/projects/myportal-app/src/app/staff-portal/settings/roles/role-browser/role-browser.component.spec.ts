import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleBrowserComponent } from './role-browser.component';

describe('RoleBrowserComponent', () => {
  let component: RoleBrowserComponent;
  let fixture: ComponentFixture<RoleBrowserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoleBrowserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RoleBrowserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
