import Application from "./Common/Application.tsx";
import MainPage from "./MainPage.tsx";
import VillagePage from "./Pages/VillagePage.tsx";
import SettingsPage from "./Pages/SettingsPage.tsx";
import { BrowserRouter, Route, Routes } from "react-router-dom";

function Root() {
    return (
        <Application>
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<MainPage/>}>
                        <Route index element={<VillagePage/>}/>
                        <Route path="settings" element={<SettingsPage/>}/>
                    </Route>
                </Routes>
            </BrowserRouter>
        </Application>
    )
}

export default Root
