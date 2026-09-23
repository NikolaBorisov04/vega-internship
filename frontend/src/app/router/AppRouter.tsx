import { Routes, Route, Navigate } from "react-router-dom";
import { EventsPage } from "../../features/events/pages/EventsPage/EventsPage";
import { EventDetailsPage } from "../../features/events/pages/EventDetailsPage/EventDetailsPage";
import LoginPage from "../../features/auth/pages/LoginPage/LoginPage";
import { AppLayout } from "../../layouts/AppLayout";
import { GuestRoute } from "./GuestRoute";
import { OrganizerRoute } from "./OrganizerRoute";
import { CreateEventPage } from "../../features/events/pages/CreateEventPage/CreateEventPage";


export function AppRouter() {
    return (
        <Routes>
            <Route element={<GuestRoute />}>
                <Route
                    path="/login"
                    element={<LoginPage />}
                />
            </Route>

            <Route element={<AppLayout />}>
                <Route
                    path="/events"
                    element={<EventsPage />}
                />

                <Route
                    path="/events/:id"
                    element={<EventDetailsPage />}
                />

                <Route element={<OrganizerRoute />}>
                    <Route
                        path="/createevent"
                        element={<CreateEventPage />}
                    />
                </Route>
            </Route>

            <Route
                path="*"
                element={<Navigate to="/events" replace />}
            />
        </Routes>
    );
}