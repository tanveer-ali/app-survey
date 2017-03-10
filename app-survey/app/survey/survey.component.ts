import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { Subscription }       from 'rxjs/Subscription';

import { ISurvey } from "./isurvey";
import { SurveyService } from "./survey.service";

@Component({
    templateUrl: 'app/survey/survey.component.html'
})
export class SurveyComponent implements OnInit, OnDestroy {
    pageTitle: string = 'Surveys';
    surveys: ISurvey[];
    errorMessage: string;    

    constructor(private _route: ActivatedRoute,
                private _router: Router,
                private _service: SurveyService) {
    }

    ngOnInit(): void {
        this._service.getSurveys()
            .subscribe(surveys => this.surveys = surveys,
            error => this.errorMessage = <any>error);
    }

    ngOnDestroy() {        
    }
}
