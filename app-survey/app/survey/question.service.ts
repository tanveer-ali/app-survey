import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';

import { IQuestion } from "./iquestion";

@Injectable()
export class QuestionService {
    private _apiUrl = 'http://localhost:60724/api/survey/';

    constructor(private _http: Http) { }

    getQuestions(id: number): Observable<IQuestion[]> {
        return this._http.get(this._apiUrl + 'questions/' + id.toString())
            .map((response: Response) => <IQuestion[]> response.json())            
            .catch(this.handleError);
    }

    submitSurvey(id: number, answers: IQuestion[]): Observable<boolean> {
        return this._http.post(this._apiUrl + id.toString(),answers)
           .map((res: Response) => {
                if (res) {
                    if (res.status === 201) {
                        return true;
                    }else{
                        return false;
                    }
                }
            })
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        // in a real world app, we may send the server to some remote logging infrastructure
        // instead of just logging it to the console
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}
