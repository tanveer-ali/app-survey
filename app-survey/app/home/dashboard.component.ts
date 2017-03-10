import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs/Subscription';

import { IDashboard } from "./idashboard";
import { DashboardService } from "./dashboard.service"

@Component({
    templateUrl: 'app/home/dashboard.component.html'
})
export class DashboardComponent implements OnInit, OnDestroy {
    pageTitle: string = 'Survey Dashboard';
    errorMessage: string;
    private sub: Subscription;
    // Doughnut
    surveyResponse: IDashboard;
    public doughnutChartType: string = 'doughnut';  

    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _service: DashboardService) {
    }

    ngOnInit(): void {
        this.sub = this._route.params.subscribe(
            params => {                
                this.getResponses();
        });        
    }

    getResponses() {
        this._service.getAllResponses()
            .subscribe(responses => {this.surveyResponse = responses},
            error => this.errorMessage = <any>error);
    }

    // events
  public chartClicked(e:any):void {
      this._router.navigate(['/dashboard',this.surveyResponse.surveyIds[e.active[0]._index],this.surveyResponse.labels[e.active[0]._index] ]);    
  }

    ngOnDestroy() {
    }
}
