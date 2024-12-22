import {Button, Grid, GridColumn, GridRow} from "semantic-ui-react";
import InvertibleSegment from "../Common/InvertibleSegment.tsx";
import {Api, IDummyResult} from "../Api/Api.ts";
import {ApplicationContext} from "../Common/Contexts.tsx";
import {useContext, useState} from "react";

function VillagePage() {

    const applicationContext = useContext(ApplicationContext);

    const [serverValue, setServerValue] = useState<IDummyResult>();

    function postSomething(value: string) {
        return Api.Test.PostSomething({someValue: value}, applicationContext);
    }

    function getSomething() {
        Api.Test.GetSomething(applicationContext)
            .then(value => setServerValue(value));
    }

    function triggerError() {
        Api.Test.GetError(applicationContext);
    }

    return (
        <InvertibleSegment>
            <Grid>
                <GridRow>
                    <GridColumn width={3}>
                        Zde kopni Evžene do {serverValue?.someValue}
                    </GridColumn>
                    <GridColumn>
                        <Button color='orange' onClick={() => postSomething("test")}>Post</Button>
                    </GridColumn>
                    <GridColumn>
                        <Button color='orange' onClick={() => getSomething()}>Get</Button>
                    </GridColumn>
                    <GridColumn>
                        <Button color='red' onClick={() => triggerError()}>Error</Button>
                    </GridColumn>
                </GridRow>
            </Grid>
        </InvertibleSegment>
    )
}

export default VillagePage
