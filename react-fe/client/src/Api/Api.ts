// generated file
import {GetJson, PostJson} from '../Common/Api/Fetch.ts'
import {IApplicationContext} from '../Common/Contexts.tsx'

export class Test {
    PostSomething(input: IDummyInput, applicationContext: IApplicationContext): Promise<IErrorResponse> {
        return PostJson(input, "test", "postsomething", applicationContext);
    }

    GetSomething(applicationContext: IApplicationContext): Promise<IDummyResult> {
        return GetJson("test", "getsomething", applicationContext);
    }

    GetError(applicationContext: IApplicationContext): Promise<IErrorResponse> {
        return GetJson("test", "geterror", applicationContext);
    }

}

export class ApiContext {
    Test: Test;

    constructor() {
        this.Test = new Test();
    }
}

export const Api = new ApiContext();

export interface IDummyInput {
    someValue: string;
}

export interface IErrorResponse {
    message: string;
}

export interface IDummyResult {
    someValue: string;
}

