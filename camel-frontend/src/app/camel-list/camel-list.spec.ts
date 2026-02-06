import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CamelListComponent } from './camel-list';

describe('CamelList', () => {
  let component: CamelListComponent;
  let fixture: ComponentFixture<CamelListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CamelListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CamelListComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
