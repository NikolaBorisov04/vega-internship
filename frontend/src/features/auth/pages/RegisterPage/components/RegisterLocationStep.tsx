import { Globe2, MapPin, Phone } from "lucide-react";
import type { RegisterDraftFields } from "../../../store/registerStore";
import type { RegisterErrors } from "../../../utils/registerValidation";
import { RegisterInput } from "./RegisterInput";

interface RegisterLocationStepProps {
    draft: RegisterDraftFields;
    errors: RegisterErrors;
    isLoading: boolean;
    onChange: <
        K extends keyof RegisterDraftFields
    >(
        field: K,
        value: RegisterDraftFields[K]
    ) => void;
}

export function RegisterLocationStep({
    draft,
    errors,
    isLoading,
    onChange,
}: RegisterLocationStepProps) {
    return (
        <div className="register-form-step register-form-step--enter">
            <div className="register-form-intro">
                <span>YOUR HOME BASE</span>

                <h2>
                    Tell us
                    <em> where.</em>
                </h2>

                <p>
                    Give your profile a little more context.
                </p>
            </div>

            <div className="register-form-grid">
                <RegisterInput
                    id="register-country"
                    label="Country"
                    icon={<Globe2 size={18} />}
                    value={draft.country}
                    onValueChange={(value) => onChange("country", value)}
                    placeholder="Country Name"
                    autoComplete="country-name"
                    disabled={isLoading}
                    error={errors.country}
                />

                <RegisterInput
                    id="register-city"
                    label="City"
                    icon={<MapPin size={18} />}
                    value={draft.city}
                    onValueChange={(value) => onChange("city", value)}
                    placeholder="City Name"
                    autoComplete="address-level2"
                    disabled={isLoading}
                    error={errors.city}
                />

                <RegisterInput
                    id="register-address"
                    label="Address"
                    icon={<MapPin size={18} />}
                    value={draft.address}
                    onValueChange={(value) => onChange("address", value)}
                    placeholder="Street and number"
                    autoComplete="street-address"
                    disabled={isLoading}
                    error={errors.address}
                />

                <RegisterInput
                    id="register-phone"
                    label="Phone number"
                    icon={<Phone size={18} />}
                    value={draft.phoneNumber}
                    onValueChange={(value) => onChange("phoneNumber", value)}
                    placeholder="+381 64 123 4567"
                    autoComplete="tel"
                    inputMode="tel"
                    disabled={isLoading}
                    error={errors.phoneNumber}
                />
            </div>
        </div>
    );
}