import { Component, OnInit } from '@angular/core';
import { Camel } from '../shared/models/camel';
import { CamelService } from '../shared/services/camel-service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'app-camel-list',
    standalone: true,
    imports: [CommonModule, RouterLink],
    templateUrl: './camel-list.html',
    styleUrls: ['./camel-list.css'],
})
export class CamelListComponent implements OnInit {
    camels$!: Observable<Camel[]>;

    constructor(private camelService: CamelService) {}

    ngOnInit(): void {
        this.camels$ = this.camelService.getAll();
    }
}
