import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CamelService } from '../shared/services/camel-service';
import { Camel } from '../shared/models/camel';
import { MessageService } from '../shared/services/message-service';

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
        private messageService: MessageService,
    ) {}

    ngOnInit(): void {
        this.form = this.formBuilder.group({
            name: ['', [Validators.required]],
            color: [''],
            humpCount: [1, [Validators.required, Validators.min(1), Validators.max(2)]],
            lastFed: [''],
        });

        const id = this.route.snapshot.paramMap.get('id');

        if (id) {
            this.isEdit = true;
            this.camelId = Number(id);

            this.camelService.getById(this.camelId).subscribe({
                next: (camel) => {
                    camel.lastFed = camel.lastFed?.slice(0, 16);
                    this.form.patchValue(camel);
                },
                error: (err) => {
                    if (err.status === 404)
                        this.messageService.show({
                            text: `No camel found with id of ${this.camelId}.`,
                            type: 'danger',
                        });
                    this.router.navigate(['/camels']);
                },
            });
        }
    }

    submit(): void {
        if (this.form.invalid) {
            this.form.markAllAsTouched();
            return;
        }

        const camel: Camel = this.form.value;

        if (!camel.lastFed) {
            camel.lastFed = null;
        }

        if (this.isEdit) {
            this.camelService.update(this.camelId, camel).subscribe({
                next: () => {
                    this.messageService.show({
                        text: 'Camel updated successfully!',
                        type: 'success',
                    });
                    this.router.navigate(['/camels']);
                },
                error: (err) => {
                    this.messageService.show({
                        text: err.error,
                        type: 'danger',
                    });
                },
            });
        } else {
            this.camelService.create(camel).subscribe({
                next: () => {
                    this.messageService.show({
                        text: 'Camel updated successfully!',
                        type: 'success',
                    });
                    this.router.navigate(['/camels']);
                },
                error: (err) => {
                    this.messageService.show({
                        text: err.error,
                        type: 'danger',
                    });
                },
            });
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
