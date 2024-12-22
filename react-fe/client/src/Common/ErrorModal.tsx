import {
    Button, Grid, GridColumn, GridRow,
    Header,
    Icon,
    Modal,
    ModalActions,
    ModalContent,
} from "semantic-ui-react";
import {useContext} from "react";
import {ApplicationContext} from "./Contexts.tsx";

function ErrorModal() {

    const applicationContext = useContext(ApplicationContext);
    const applicationState = applicationContext.application;

    function close() {
        applicationContext.setApplication({
            ...applicationState,
            errorMessages: undefined
        });
    }

    return (
        <Modal
            size={"tiny"}
            open={applicationState.errorMessages !== null}
            //trigger={<Button>Show Modal</Button>}
            onClose={close}
        >
            <Header icon='thumbs down outline' content='Nastal problém'/>
            <ModalContent>
                <Grid>
                    {
                        applicationState.errorMessages?.map(errorMessage => {
                            return (
                                <GridRow>
                                    <GridColumn width={2}>
                                        {errorMessage.time.toLocaleTimeString('cs-CZ', {
                                            hour: '2-digit',   // Hour in 2-digit format (24-hour format)
                                            minute: '2-digit', // Minute in 2-digit format
                                            second: '2-digit', // Second in 2-digit format
                                            hour12: false      // Ensure 24-hour format
                                        })}
                                    </GridColumn>
                                    <GridColumn width={14}>
                                        {errorMessage.message}
                                    </GridColumn>
                                </GridRow>
                            )
                        })
                    }
                </Grid>
            </ModalContent>
            <ModalActions>
                <Button color='red' onClick={close}>
                    <Icon name='remove'/> Close
                </Button>
            </ModalActions>
        </Modal>
    )
}

export default ErrorModal
