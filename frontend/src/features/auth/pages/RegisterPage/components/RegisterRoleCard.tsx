import {
    ArrowUpRight,
} from "lucide-react";

import type { LucideIcon } from "lucide-react";
import type { RegistrationRole } from "../../../store/registerStore";

interface RegisterRoleCardProps {
    role: RegistrationRole;

    eyebrow: string;
    title: string;
    description: string;

    Icon: LucideIcon;

    onSelect: () => void;
}

export function RegisterRoleCard({
    eyebrow,
    title,
    description,
    Icon,
    onSelect,
}: RegisterRoleCardProps) {
    return (
        <button
            type="button"
            className="register-role-card"
            onClick={onSelect}
        >
            <span className="register-role-card__shine" />

            <div className="register-role-card__top">
                <span className="register-role-card__icon">
                    <Icon size={23} />
                </span>

                <ArrowUpRight
                    size={19}
                    className="register-role-card__arrow"
                />
            </div>

            <div className="register-role-card__content">
                <span className="register-role-card__eyebrow">
                    {eyebrow}
                </span>

                <h3>{title}</h3>

                <p>{description}</p>
            </div>

            <span className="register-role-card__action">
                Continue
                <ArrowUpRight size={15} />
            </span>
        </button>
    );
}