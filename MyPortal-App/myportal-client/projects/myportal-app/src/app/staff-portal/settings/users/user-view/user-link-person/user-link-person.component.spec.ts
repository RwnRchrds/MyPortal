import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserLinkPersonComponent } from './user-link-person.component';

describe('UserLinkPersonComponent', () => {
  let component: UserLinkPersonComponent;
  let fixture: ComponentFixture<UserLinkPersonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserLinkPersonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserLinkPersonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
