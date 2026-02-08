import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { AppMessage } from '../models/message';

@Injectable({
    providedIn: 'root',
})
export class MessageService {
    private messageSubject = new BehaviorSubject<AppMessage | null>(null);

    message$ = this.messageSubject.asObservable();

    show(message: AppMessage) {
        this.messageSubject.next(message);
    }

    clear() {
        this.messageSubject.next(null);
    }
}
