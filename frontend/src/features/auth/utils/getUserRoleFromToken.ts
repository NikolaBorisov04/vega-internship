import { UserRole } from "../../../api/generated/api";

interface JwtPayload {
    role?: string;
    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"?:
        | string
        | string[];
}

export function getUserRoleFromToken(
    token: string | null
): UserRole | null {
    if (!token) {
        return null;
    }

    try {
        const payload = token.split(".")[1];

        if (!payload) {
            return null;
        }

        const decoded = JSON.parse(
            atob(
                payload
                    .replace(/-/g, "+")
                    .replace(/_/g, "/")
            )
        ) as JwtPayload;

        const rawRole =
            decoded.role ??
            decoded[
                "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
            ];

        const role = Array.isArray(rawRole)
            ? rawRole[0]
            : rawRole;

        switch (role) {
            case "Customer":
                return UserRole.Customer;
            case "Organizer":
                return UserRole.Organizer;
            case "Admin":
                return UserRole.Admin;
            default:
                return null;
        }
    } catch {
        return null;
    }
}