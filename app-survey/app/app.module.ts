import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { DashboardComponent } from "./home/dashboard.component"

/* Feature Modules */
import {SurveyModule } from "./survey/survey.module"
import { ChartsModule } from 'ng2-charts/ng2-charts';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        ChartsModule,
        RouterModule.forRoot([
            { path: 'dashboard', component: DashboardComponent },
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
        ]),        
        SurveyModule
    ],
    declarations: [
        AppComponent,
        DashboardComponent
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}