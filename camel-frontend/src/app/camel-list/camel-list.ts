import { Component, OnInit } from '@angular/core';
import { Camel } from '../shared/models/camel';
import { CamelService } from '../services/camel-service';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-camel-list',
    standalone: true,
    imports: [CommonModule],
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
