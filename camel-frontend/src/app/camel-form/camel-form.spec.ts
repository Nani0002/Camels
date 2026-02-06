import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CamelFormComponent } from './camel-form';

describe('CamelForm', () => {
  let component: CamelFormComponent;
  let fixture: ComponentFixture<CamelFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CamelFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CamelFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
