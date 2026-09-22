import { Routes, Route, Navigate } from "react-router-dom";
import { LoginPage } from "../../features/auth/pages/LoginPage";
import { EventsPage } from "../../features/events/pages/EventsPage/EventsPage";
import { EventDetailsPage } from "../../features/events/pages/EventDetailsPage/EventDetailsPage";


export function AppRouter() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/events" element={<EventsPage />} />
      <Route path="/events/:id" element={<EventDetailsPage />} />

      <Route
        path="*"
        element={<Navigate to="/events" replace />}
      />
    </Routes>
  );
}