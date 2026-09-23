import { Outlet } from "react-router-dom";
import { AppHeader } from "../shared/components/AppHeader";

export function AppLayout() {
  return (
    <>
      <AppHeader />

      <Outlet />
    </>
  );
}