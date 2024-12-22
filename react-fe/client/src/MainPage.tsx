import {Grid, GridColumn, GridRow} from "semantic-ui-react";
import {useContext} from "react";
import TopMenu from "./TopMenu.tsx";
import {ApplicationContext} from "./Common/Contexts.tsx";
import InvertibleSegment from "./Common/InvertibleSegment.tsx";
import { Outlet } from "react-router-dom";

function MainPage() {

    const applicationState = useContext(ApplicationContext).application;

    return (
        <InvertibleSegment style={{minHeight: '100vh'}}>
            <Grid inverted={applicationState.isInverted}>
                <GridRow>
                    <GridColumn>
                        <TopMenu></TopMenu>
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn>
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn>
                        <Outlet />
                    </GridColumn>
                </GridRow>
            </Grid>
            
        </InvertibleSegment>
    )
}

export default MainPage
