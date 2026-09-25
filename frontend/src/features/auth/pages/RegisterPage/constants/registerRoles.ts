import { UserRole } from "../../../../../api/generated/api";
import type { RegistrationRole } from "../../../store/registerStore";


interface RegisterRoleConfig {
    title: string;
    submitLabel: string;
}

export const REGISTER_ROLE_CONFIG: Record<
    RegistrationRole,
    RegisterRoleConfig
> = {
    [UserRole.Customer]: {
        title: "Customer profile",
        submitLabel: "Create customer profile",
    },

    [UserRole.Organizer]: {
        title: "Organizer profile",
        submitLabel: "Create organizer profile",
    },

    [UserRole.Admin]: {
        title: "Admin profile",
        submitLabel: "Create admin profile",
    },
};