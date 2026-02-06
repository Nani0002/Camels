import { Routes } from '@angular/router';
import { CamelListComponent } from './camel-list/camel-list';
import { CamelFormComponent } from './camel-form/camel-form';

export const routes: Routes = [
    { path: '', redirectTo: '/camels', pathMatch: 'full' },
    { path: 'camels', component: CamelListComponent },
    { path: 'camels/create', component: CamelFormComponent },
    { path: 'camels/edit/:id', component: CamelFormComponent },
];
