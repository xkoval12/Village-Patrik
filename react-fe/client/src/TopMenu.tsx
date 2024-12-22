import {
    Grid,
    GridColumn,
    GridRow,
    Menu,
    MenuItem,
    MenuMenu
} from "semantic-ui-react";
import {useContext, useEffect} from "react";
import {ApplicationContext} from "./Common/Contexts.tsx";
import { Link, useLocation } from "react-router-dom";

function TopMenu() {

    const location = useLocation();
    const applicationContext = useContext(ApplicationContext);
    const applicationState = applicationContext.application;

    useEffect(() => {
        applicationContext.setApplication({
            ...applicationState,
            activeMenuName: location.pathname
        })
    }, [location]);

    return (
        <Grid>
            <GridRow>
                <GridColumn>
                    <Menu inverted={applicationState.isInverted} pointing secondary size='massive'>
                        <MenuItem header>
                                Vrkoslavice
                        </MenuItem>
                        <MenuMenu position={"right"}>
                            <MenuItem name='village' active={applicationState.activeMenuName == "/"}
                                      as={Link} to={"/"}
                                      onClick={() => {
                                          applicationContext.setApplication({
                                              ...applicationState,
                                              activeMenuName: "/"
                                          })
                                      }}>
                                Village
                            </MenuItem>

                            <MenuItem name="settings" active={applicationState.activeMenuName == "/settings"}
                                      as={Link} to={"/settings"}
                                      onClick={() => {
                                          applicationContext.setApplication({
                                              ...applicationState,
                                              activeMenuName: "/settings"
                                          })
                                      }}>
                                Settings
                            </MenuItem>

                            <MenuItem icon={"sun"}
                                      onClick={() => {
                                          applicationContext.setApplication({
                                              ...applicationState,
                                              isInverted: !applicationState.isInverted
                                          })
                                      }}>
                            </MenuItem>
                        </MenuMenu>
                    </Menu>
                </GridColumn>
            </GridRow>
        </Grid>

    )
}

export default TopMenu
