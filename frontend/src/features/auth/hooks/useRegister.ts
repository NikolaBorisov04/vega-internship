import { useMutation } from "@tanstack/react-query";

import type {
    RegisterAdminDTO,
    RegisterCustomerDTO,
    RegisterOrganizerDTO,
    UserResponseDTO,
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
    const mutation =
        useMutation<
            UserResponseDTO,
            unknown,
            RegisterPayload
        >({
            mutationFn: async ({
                role,
                data,
            }) => {
                const commonFields = {
                    name: data.name.trim(),
                    email: data.email.trim(),
                    password: data.password,
                    country: data.country.trim(),
                    city: data.city.trim(),
                    address: data.address.trim(),
                    phoneNumber:
                        data.phoneNumber.trim(),
                };

                switch (role) {
                    case "Customer": {
                        const payload: RegisterCustomerDTO = {
                            ...commonFields,
                        };

                        return apiClient.customer(
                            payload
                        );
                    }

                    case "Organizer": {
                        const payload: RegisterOrganizerDTO = {
                            ...commonFields,
                            companyName:
                                data.companyName.trim(),
                        };

                        return apiClient.organizer(
                            payload
                        );
                    }

                    case "Admin": {
                        const payload: RegisterAdminDTO = {
                            ...commonFields,
                            companyName:
                                data.companyName.trim(),
                        };

                        return apiClient.admin(
                            payload
                        );
                    }

                    default:
                        throw new Error(
                            "Invalid registration role."
                        );
                }
            },
        });

    const register = async (
        payload: RegisterPayload
    ): Promise<UserResponseDTO | null> => {
        try {
            return await mutation.mutateAsync(
                payload
            );
        } catch {
            return null;
        }
    };

    return {
        register,

        isLoading: mutation.isPending,

        error: mutation.error
            ? getApiErrorMessage(
                  mutation.error
              )
            : null,

        clearError: () =>
            mutation.reset(),
    };
}