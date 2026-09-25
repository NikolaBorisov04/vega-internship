import { useMutation } from "@tanstack/react-query";

import {
    UserRole,
    type RegisterAdminDTO,
    type RegisterCustomerDTO,
    type RegisterOrganizerDTO,
    type UserResponseDTO,
} from "../../../api/generated/api";

import { apiClient } from "../../../api/client";

import { getApiErrorMessage } from "../utils/getApiErrorMessage";

import type {
    RegisterDraftFields,
    RegistrationRole,
} from "../store/registerStore";

interface RegisterPayload {
    role: RegistrationRole;
    data: RegisterDraftFields;
}

interface UseRegisterResult {
    register: (
        payload: RegisterPayload
    ) => Promise<UserResponseDTO | null>;

    isLoading: boolean;
    error: string | null;
    clearError: () => void;
}

export function useRegister(): UseRegisterResult {
    const mutation = useMutation<
        UserResponseDTO,
        unknown,
        RegisterPayload
    >({
        mutationFn: async ({ role, data }) => {
            const fields = {
                name: data.name.trim(),
                email: data.email.trim(),
                password: data.password,
                country: data.country.trim(),
                city: data.city.trim(),
                address: data.address.trim(),
                phoneNumber: data.phoneNumber.trim(),
            };

            switch (role) {
                case UserRole.Customer:
                    return apiClient.customer(
                        fields satisfies RegisterCustomerDTO
                    );

                case UserRole.Organizer:
                    return apiClient.organizer({
                        ...fields,
                        companyName: data.companyName.trim(),
                    } satisfies RegisterOrganizerDTO);

                case UserRole.Admin:
                    return apiClient.admin({
                        ...fields,
                        companyName: data.companyName.trim(),
                    } satisfies RegisterAdminDTO);

                default:
                    throw new Error("Invalid registration role.");
            }
        },
    });

    const register = async (
        payload: RegisterPayload
    ): Promise<UserResponseDTO | null> => {
        try {
            return await mutation.mutateAsync(payload);
        } catch {
            return null;
        }
    };

    return {
        register,
        isLoading: mutation.isPending,
        error: mutation.error
            ? getApiErrorMessage(mutation.error)
            : null,
        clearError: mutation.reset,
    };
}
