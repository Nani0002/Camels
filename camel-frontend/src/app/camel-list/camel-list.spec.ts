import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CamelListComponent } from './camel-list';
import { of } from 'rxjs';
import { CamelService } from '../shared/services/camel-service';

const mockService = {
    getAll: () => of([]),
};

describe('CamelList', () => {
    let component: CamelListComponent;
    let fixture: ComponentFixture<CamelListComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [CamelListComponent],
            providers: [
                {
                    provide: CamelService,
                    useValue: mockService,
                },
            ],
        }).compileComponents();

        fixture = TestBed.createComponent(CamelListComponent);
        component = fixture.componentInstance;
        await fixture.whenStable();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
