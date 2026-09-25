import { BriefcaseBusiness, Check } from "lucide-react";
import type { RegisterDraftFields } from "../../../store/registerStore";
import type { RegisterErrors } from "../../../utils/registerValidation";
import { RegisterInput } from "./RegisterInput";

interface RegisterOrganizationStepProps {
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

export function RegisterOrganizationStep({
    draft,
    errors,
    isLoading,
    onChange,
}: RegisterOrganizationStepProps) {
    return (
        <div className="register-form-step register-form-step--enter">
            <div className="register-form-intro">
                <span>THE BIG PICTURE</span>

                <h2>
                    Bring your
                    <em> world.</em>
                </h2>

                <p>
                    This profile represents an organization inside the Events
                    platform.
                </p>
            </div>

            <RegisterInput
                id="register-company"
                label="Company / organization"
                icon={<BriefcaseBusiness size={18} />}
                value={draft.companyName}
                onValueChange={(value) => onChange("companyName", value)}
                placeholder="Your company or organization"
                autoComplete="organization"
                disabled={isLoading}
                error={errors.companyName}
            />

            <div className="register-ready-panel">
                <div className="register-ready-panel__icon">
                    <Check size={18} />
                </div>

                <div>
                    <strong>Profile almost ready</strong>

                    <p>
                        Everything looks good. Create the profile and step into
                        Events.
                    </p>
                </div>
            </div>
        </div>
    );
}