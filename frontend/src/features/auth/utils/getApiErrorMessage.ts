import type { ProblemDetails } from "../../../api/generated/api";

interface ApiError extends ProblemDetails {
    result?: ProblemDetails;
}

export function getApiErrorMessage(
    error: unknown
): string {
    const problem =
        (error ?? {}) as ApiError;

    return (
        problem.detail ??
        problem.result?.detail ??
        "Something went wrong. Please try again."
    );
}