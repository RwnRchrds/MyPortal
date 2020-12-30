import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleSearchComponent } from './role-search.component';

describe('RoleSearchComponent', () => {
  let component: RoleSearchComponent;
  let fixture: ComponentFixture<RoleSearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoleSearchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RoleSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
