import { IQuestionType } from "./iquestionType";
import { IQuestionOption } from "./iquestionOption";

export interface IQuestion {
    questionId: number;
    questionLabel: string;
    questionOrder: number;
    required: boolean;
    questionType: IQuestionType;
    questionOptions: IQuestionOption[];
    answer: string;
}

