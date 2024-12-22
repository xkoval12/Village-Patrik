import {IApplication, IApplicationContext, IErrorMessage} from "../Contexts.tsx";
import {IErrorResponse} from "../../Api/Api.ts";

function getErrorMessages(message: string, application: IApplication): IErrorMessage[] {

    let errorMessages : IErrorMessage[] = [];
    if (application.errorMessages !== undefined) {
        errorMessages = [...application.errorMessages]
    }

    const existingIndex = errorMessages.findIndex(item => item.message === message);

    if (existingIndex !== -1) {
        errorMessages.splice(existingIndex, 1);
    }

    errorMessages.push({time: new Date(), message: message });

    return errorMessages;
}

function handleError(message: string | undefined | null, applicationContext: IApplicationContext) {
    if (message === undefined || message == null)
    {
        return;
    }
    applicationContext.setApplication({
        ...applicationContext.application,
        errorMessages: getErrorMessages(message, applicationContext.application)
    });
}

export async function PostJson<TContent, TResult>(content: TContent, controller: string, method: string,
    applicationContext: IApplicationContext) : Promise<TResult> {

    return await fetch('api/' + controller + '/' + method, {
        method: "POST",
        body: JSON.stringify(content),
        headers: {
            'Content-type': 'application/json; charset=UTF-8',
        }
    })
        .then(async response => {
            if (!response.ok) {
                console.log(response)
                handleError("Unexpected problem has occured", applicationContext);
                throw new Error("Unexpected problem has occured");
            }
            const responseBody = await response.json();
            const errorResponse = responseBody as IErrorResponse;
            if (errorResponse !== undefined) {
                handleError(errorResponse.message, applicationContext);
            }
            return responseBody;
        })
        .catch(error => {
            console.log(error)
            handleError("Unexpected problem has occured", applicationContext);
            throw new Error("Unexpected problem has occured");
        })
}

export async function GetJson<TResult>(controller: string, method: string,
    applicationContext: IApplicationContext) : Promise<TResult> {
    return await fetch('api/' + controller + '/' + method)
        .then(async response => {
            if (!response.ok) {
                console.log(response)
                handleError("Unexpected problem has occured", applicationContext);
                throw new Error("Unexpected problem has occured");
            }
            const responseBody = await response.json();
            const errorResponse = responseBody as IErrorResponse;
            if (errorResponse !== undefined) {
                handleError(errorResponse.message, applicationContext);
            }
            return responseBody;
        })
        .catch(error => {
            console.log(error)
            handleError("Unexpected problem has occured", applicationContext);
            throw new Error("Unexpected problem has occured");
        });
}
