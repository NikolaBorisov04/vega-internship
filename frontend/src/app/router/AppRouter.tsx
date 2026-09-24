import { Routes, Route, Navigate } from "react-router-dom";
import { EventsPage } from "../../features/events/pages/EventsPage/EventsPage";
import { EventDetailsPage } from "../../features/events/pages/EventDetailsPage/EventDetailsPage";
import LoginPage from "../../features/auth/pages/LoginPage/LoginPage";
import { AppLayout } from "../../layouts/AppLayout";
import { GuestRoute } from "./GuestRoute";
import { OrganizerRoute } from "./OrganizerRoute";
import { CreateEventPage } from "../../features/events/pages/CreateEventPage/CreateEventPage";
import { ROUTES } from "../../constants/routes";
import { RegisterRoute } from "./RegisterRoute";
import RegisterPage from "../../features/auth/pages/RegisterPage/RegisterPage";


export function AppRouter() {
    return (
        <Routes>
            <Route element={<GuestRoute />}>
                <Route
                    path={ROUTES.LOGIN}
                    element={<LoginPage />}
                />
            </Route>

            <Route element={<RegisterRoute />}>
                <Route
                    path={ROUTES.REGISTER}
                    element={<RegisterPage />}
                />
            </Route>

            <Route element={<AppLayout />}>
                <Route
                    path={ROUTES.EVENTS}
                    element={<EventsPage />}
                />

                <Route
                    path={`${ROUTES.EVENTS}/:id`}
                    element={<EventDetailsPage />}
                />

                <Route element={<OrganizerRoute />}>
                    <Route
                        path={ROUTES.CREATEEVENT}
                        element={<CreateEventPage />}
                    />
                </Route>
            </Route>

            <Route
                path="*"
                element={<Navigate to={ROUTES.EVENTS} replace />}
            />
        </Routes>
    );
}