import Contexts from "./Contexts.tsx";
import { QueryClient, QueryClientProvider } from "react-query";
import {ReactNode, StrictMode } from "react";

const queryClient = new QueryClient()

type ApplicationProps = {
    children: ReactNode
}

function Application(props: ApplicationProps) {
    return (
        <StrictMode>
            <QueryClientProvider client={queryClient}>
                <Contexts>
                    {props.children}
                </Contexts>
            </QueryClientProvider>
        </StrictMode>
    )
}

export default Application