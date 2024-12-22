import { Grid, GridColumn, GridRow } from "semantic-ui-react";
import InvertibleSegment from "../Common/InvertibleSegment.tsx";

function SettingsPage() {

    return (
        <InvertibleSegment>
            <Grid>
                <GridRow>
                    <GridColumn>
                        Settings
                    </GridColumn>
                </GridRow>
            </Grid>
        </InvertibleSegment>
    )
}

export default SettingsPage
