import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CamelService } from '../services/camel-service';
import { Camel } from '../shared/models/camel';

@Component({
    selector: 'app-camel-form',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, RouterLink],
    templateUrl: './camel-form.html',
    styleUrls: ['./camel-form.css'],
})
export class CamelFormComponent implements OnInit {
    form!: FormGroup;
    isEdit = false;
    camelId!: number;

    constructor(
        private formBuilder: FormBuilder,
        private camelService: CamelService,
        private route: ActivatedRoute,
        private router: Router,
    ) {}

    ngOnInit(): void {
        this.form = this.formBuilder.group({
            name: [''],
            color: [''],
            humpCount: [1],
            lastFed: [''],
        });

        const id = this.route.snapshot.paramMap.get('id');

        if (id) {
            this.isEdit = true;
            this.camelId = Number(id);

            this.camelService.getById(this.camelId).subscribe((camel) => {
                camel.lastFed = camel.lastFed?.slice(0, 16);
                this.form.patchValue(camel);
            });
        }
    }

    submit(): void {
        const camel: Camel = this.form.value;

        if (this.isEdit) {
            this.camelService
                .update(this.camelId, camel)
                .subscribe(() => this.router.navigate(['/camels']));
        } else {
            this.camelService.create(camel).subscribe(() => this.router.navigate(['/camels']));
        }
    }

    setDateNow(): void {
        const now = new Date();

        const local = new Date(now.getTime() - now.getTimezoneOffset() * 60000)
            .toISOString()
            .slice(0, 16);

        this.form.patchValue({
            lastFed: local,
        });
    }
}
