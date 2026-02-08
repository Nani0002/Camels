import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CamelFormComponent } from './camel-form';
import { ActivatedRoute } from '@angular/router';

describe('CamelForm', () => {
    let component: CamelFormComponent;
    let fixture: ComponentFixture<CamelFormComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [CamelFormComponent],
            providers: [{
                provide: ActivatedRoute,
                useValue: {
                    snapshot: {
                        paramMap: {
                            get: () => null
                        }
                    }
                }
            }]
        }).compileComponents();

        fixture = TestBed.createComponent(CamelFormComponent);
        component = fixture.componentInstance;
        await fixture.whenStable();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should create the form', () => {
        expect(component.form).toBeTruthy();
    });

    it('should be invalid when empty', () => {
        expect(component.form.valid).toBeFalsy();
    });

    it('should require name', () => {
        const control = component.form.get('name');

        control?.setValue('');

        expect(control?.valid).toBeFalsy();
    });

    it('should require humpCount  1 <= x <= 2', () => {
        const control = component.form.get('humpCount');

        control?.setValue(0);
        expect(control?.valid).toBeFalsy();

        control?.setValue(2);
        expect(control?.valid).toBeTruthy();

        control?.setValue(3);
        expect(control?.valid).toBeFalsy();
    });

    it('should be valid with correct data', () => {
        component.form.setValue({
            name: 'Test Camel',
            color: 'Brown',
            humpCount: 2,
            lastFed: null,
        });

        expect(component.form.valid).toBeTruthy();
    });
});
