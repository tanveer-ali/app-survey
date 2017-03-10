import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SurveyComponent } from "./survey.component";
import { QuestionComponent } from "./question.component";

import { SurveyService } from "./survey.service";
import { QuestionService } from "./question.service";
import { DashboardService } from "../home/dashboard.service"

import { SharedModule } from '../shared/shared.module';
import { SurveyDashboardComponent } from "../home/surveyDashboard.component"
import { ChartsModule } from 'ng2-charts/ng2-charts';
import {ToastyModule} from 'ng2-toasty';

@NgModule({
    imports: [
        SharedModule,
        ChartsModule,
        ToastyModule.forRoot(),        
        RouterModule.forChild([            
            { path: 'dashboard/:id/:name', component: SurveyDashboardComponent },
            { path: 'surveys', component: SurveyComponent },
            { path: 'questions/:id/:name', component: QuestionComponent }
        ])
    ],
    declarations: [        
        SurveyDashboardComponent,
        SurveyComponent,
        QuestionComponent

    ],
    providers: [
        DashboardService,
        SurveyService,
        QuestionService
    ]
})
export class SurveyModule { }
