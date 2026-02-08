import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Camel } from '../shared/models/camel';

@Injectable({
    providedIn: 'root',
})
export class CamelService {
    private apiUrl = `${environment.apiUrl}/camels`;

    constructor(private http: HttpClient) {}

    getAll(): Observable<Camel[]> {
        return this.http.get<Camel[]>(this.apiUrl);
    }

    getById(id: Number): Observable<Camel> {
        return this.http.get<Camel>(`${this.apiUrl}/${id}`);
    }

    create(camel: Camel) {
        return this.http.post<Camel>(this.apiUrl, camel);
    }

    update(id: Number, camel: Camel) {
        return this.http.put<void>(`${this.apiUrl}/${id}`, camel);
    }
}
