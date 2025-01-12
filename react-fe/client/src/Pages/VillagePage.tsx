import {Button, Grid, GridColumn, GridRow} from "semantic-ui-react";
import InvertibleSegment from "../Common/InvertibleSegment.tsx";
import {Api, IDummyResult, IVillageDto} from "../Api/Api.ts";
import {ApplicationContext} from "../Common/Contexts.tsx";
import {useContext, useState} from "react";

function VillagePage() {

    const applicationContext = useContext(ApplicationContext);

    const [serverValue, setServerValue] = useState<IDummyResult>();
    const [village, setVillage] = useState<IVillageDto>();

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
    function getVillage() {
        Api.Village.GetVillage(applicationContext)
            .then(value => setVillage(value));
    }

    return (
        <InvertibleSegment>
            <Grid>
                <GridRow>
                    <GridColumn width={3}>
                        Zde kopni Evžene do {serverValue?.someValue}
                    </GridColumn>
                    <GridColumn width={3}>
                        Village values {village?.lumberjack.houseNumber}
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
                    <GridColumn>
                        <Button color='green' onClick={() => getVillage()}>GetVillage</Button>
                    </GridColumn>
                </GridRow>
            </Grid>
        </InvertibleSegment>
    )
}

export default VillagePage
