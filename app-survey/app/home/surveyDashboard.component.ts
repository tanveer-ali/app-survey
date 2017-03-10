import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs/Subscription';

import { IDashboard } from "./idashboard";
import { DashboardService } from "./dashboard.service"

@Component({
    templateUrl: 'app/home/surveydashboard.component.html'
})
export class SurveyDashboardComponent implements OnInit, OnDestroy {
    pageTitle: string;
    surveyId: number;
    errorMessage: string;
    private sub: Subscription;
    // Doughnut
    surveyResponses: IDashboard[];
    public doughnutChartType: string = 'doughnut';  

    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _service: DashboardService) {
    }

    ngOnInit(): void {
        let success = this._route.queryParams.subscribe(c=>{
            if(c['success'] === ""){

            }
        })
        
        this.sub = this._route.params.subscribe(
            params => {   
                this.surveyId = +params['id'];  
                this.pageTitle = params['name'];
                this.getResponses(this.surveyId);
        });        
    }

    getResponses(surveyId: number) {
        this._service.getSurveyResponses(surveyId)
            .subscribe(responses => {this.surveyResponses = responses},
            error => this.errorMessage = <any>error);
    }

    ngOnDestroy() {
    }

    onBack(): void {
        this._router.navigate(['/dashboard']);
    }
}
