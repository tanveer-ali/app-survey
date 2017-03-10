import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs/Subscription';

import { IQuestion } from "./iquestion";
import { QuestionService } from "./question.service";
import {ToastyService, ToastyConfig, ToastOptions, ToastData} from 'ng2-toasty';

@Component({
    templateUrl: 'app/survey/question.component.html',
    styleUrls: ['app/survey/question.component.css']
})
export class QuestionComponent implements OnInit, OnDestroy {
    surveyId: number;
    pageTitle: string;
    questions: IQuestion[];
    errorMessage: string;
    private sub: Subscription;

    constructor(private _route: ActivatedRoute,
        private _router: Router,
        private _service: QuestionService,
        private toastyService:ToastyService, private toastyConfig: ToastyConfig) {
            this.toastyConfig.theme = 'bootstrap';
    }

    ngOnInit(): void {
        this.sub = this._route.params.subscribe(
            params => {
                this.surveyId = +params['id'];
                this.pageTitle = params['name'];
                this.getQuestions(this.surveyId);
            });
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    getQuestions(id: number) {
        this._service.getQuestions(id).subscribe(
            questions => this.questions = questions,
            error => this.errorMessage = <any>error);
    }

    submitSurvey() {    
        
        this._service.submitSurvey(this.surveyId,this.questions).subscribe(
            response => {this.toastyService.success('Submitted!');this._router.navigate(['/dashboard']);},
            error => this.errorMessage = <any>error);
    }

    setRadioValue(questionId: number, optionId: number) {
        this.questions.find(c=>c.questionId == questionId).questionOptions.forEach(d => {
            d.selected = false;
            if (d.optionId == optionId) {
                d.selected = true;
            }
        });
    }

    onBack(): void {
        this._router.navigate(['/surveys']);
    }

 
}
