import {createContext, ReactNode, useState} from "react";
import ErrorModal from "./ErrorModal.tsx";

export interface IApplication {
    isInverted: boolean;
    activeMenuName: string;
    errorMessages?: IErrorMessage[];
}

export interface IApplicationContext {
    application: IApplication;
    setApplication: (application: IApplication) => void;
}

export interface IErrorMessage {
    time: Date;
    message: string;
}

export const ApplicationContext = createContext<IApplicationContext>({
    application : {
        isInverted: true,
        activeMenuName: "village"
    },
    setApplication: (_) => {}
});

type ContextsProps = {
    children: ReactNode
}

function Contexts(props: ContextsProps) {

    const [application, setApplication] = useState<IApplication>({
        isInverted: true,
        activeMenuName: "village",
    });

    return (
        <ApplicationContext.Provider value={{application, setApplication}}>
            {props.children}
            {application.errorMessages && <ErrorModal></ErrorModal>}
        </ApplicationContext.Provider>
    )
}

export default Contexts
