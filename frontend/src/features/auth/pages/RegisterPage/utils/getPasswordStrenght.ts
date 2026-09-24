export function getPasswordStrength(password: string) {
    let score = 0;

    if (password.length >= 8) {
        score += 1;
    }

    if (/[A-Z]/.test(password)) {
        score += 1;
    }

    if (/[0-9]/.test(password)) {
        score += 1;
    }

    if (/[^A-Za-z0-9]/.test(password)) {
        score += 1;
    }

    const labels = [
        "Start with 8+ characters",
        "Needs a little more",
        "Getting stronger",
        "Nice and strong",
        "Excellent password",
    ];

    return {
        score,
        label: labels[score],
    };
}