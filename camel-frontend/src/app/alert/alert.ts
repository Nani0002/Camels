import { Component } from '@angular/core';
import { MessageService } from '../shared/services/message-service';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-alert',
    imports: [CommonModule],
    templateUrl: './alert.html',
    styleUrls: ['./alert.css'],
    standalone: true,
})
export class AlertComponent {
    message$;

    constructor(private messageService: MessageService) {
        this.message$ = this.messageService.message$;
    }

    close() {
        this.messageService.clear();
    }
}
