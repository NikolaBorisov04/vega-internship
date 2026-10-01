import { useState } from "react";

import type { SponsorResponseDTO } from "../../../../../api/generated/api";

interface SponsorCardProps {
    sponsor: SponsorResponseDTO;
}

function getSafeWebsiteUrl(websiteUrl?: string): string | null {
    const value = websiteUrl?.trim();

    if (!value) {
        return null;
    }

    const normalizedUrl =
        value.startsWith("http://") || value.startsWith("https://")
            ? value
            : `https://${value}`;

    try {
        const url = new URL(normalizedUrl);

        if (url.protocol !== "http:" && url.protocol !== "https:") {
            return null;
        }

        return url.href;
    } catch {
        return null;
    }
}

export function SponsorCard({ sponsor }: SponsorCardProps) {
    const [hasImageError, setHasImageError] = useState(false);

    const websiteUrl = getSafeWebsiteUrl(sponsor.websiteUrl);

    const fallbackInitial =
        sponsor.name.trim().charAt(0).toUpperCase() || "✦";

    const cardContent = (
        <>
            <div className="event-sponsor-card__visual">
                {!hasImageError && sponsor.imageUrl ? (
                    <img
                        src={sponsor.imageUrl}
                        alt={`${sponsor.name} logo`}
                        className="event-sponsor-card__image"
                        loading="lazy"
                        onError={() => setHasImageError(true)}
                    />
                ) : (
                    <div
                        className="event-sponsor-card__image-fallback"
                        aria-hidden="true"
                    >
                        {fallbackInitial}
                    </div>
                )}
            </div>

            <div className="event-sponsor-card__body">
                <div className="event-sponsor-card__name-row">
                    <h3>{sponsor.name}</h3>

                    {websiteUrl && (
                        <span
                            className="event-sponsor-card__arrow"
                            aria-hidden="true"
                        >
                            ↗
                        </span>
                    )}
                </div>

                {sponsor.description && (
                    <p className="event-sponsor-card__description">
                        {sponsor.description}
                    </p>
                )}

                <span className="event-sponsor-card__link-label">
                    {websiteUrl
                        ? "Visit sponsor website ↗"
                        : "Sponsor information"}
                </span>
            </div>
        </>
    );

    if (websiteUrl) {
        return (
            <a
                href={websiteUrl}
                target="_blank"
                rel="noopener noreferrer"
                className="event-sponsor-card"
                aria-label={`Visit ${sponsor.name} website`}
            >
                {cardContent}
            </a>
        );
    }

console.log({
  name: sponsor.name,
  websiteUrl: sponsor.websiteUrl,
  safeWebsiteUrl: websiteUrl,
});

    return (
        <article className="event-sponsor-card">
            {cardContent}
        </article>
    );
}