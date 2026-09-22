using Events.Application.DTOs;
using Events.Domain.Entities;

namespace Events.Application.Factories.Email;

public static class WelcomeEmailFactory
{
    public static EmailMessageDTO Create(User user)
    {
        var htmlContent = $"""
            <!DOCTYPE html>
            <html lang="sr">
            <head>
                <meta charset="UTF-8">
                <meta name="viewport" content="width=device-width, initial-scale=1.0">
                <title>Dobrodošli na Events</title>
            </head>

            <body style="
                margin: 0;
                padding: 0;
                background-color: #f4f6f8;
                font-family: Arial, Helvetica, sans-serif;
                color: #333333;
            ">
                <table width="100%" cellpadding="0" cellspacing="0" style="padding: 40px 20px;">
                    <tr>
                        <td align="center">

                            <table
                                width="600"
                                cellpadding="0"
                                cellspacing="0"
                                style="
                                    max-width: 600px;
                                    width: 100%;
                                    background-color: #ffffff;
                                    border-radius: 12px;
                                    overflow: hidden;
                                "
                            >

                                <!-- Header -->
                                <tr>
                                    <td style="
                                        background-color: #1f2937;
                                        padding: 30px;
                                        text-align: center;
                                    ">
                                        <h1 style="
                                            margin: 0;
                                            color: #ffffff;
                                            font-size: 28px;
                                        ">
                                            Events
                                        </h1>
                                    </td>
                                </tr>

                                <!-- Content -->
                                <tr>
                                    <td style="padding: 40px;">

                                        <h2 style="
                                            margin-top: 0;
                                            margin-bottom: 20px;
                                            font-size: 24px;
                                            color: #1f2937;
                                        ">
                                            Dobrodošli, {user.Name}!
                                        </h2>

                                        <p style="
                                            font-size: 16px;
                                            line-height: 1.6;
                                            margin-bottom: 20px;
                                        ">
                                            Hvala vam što ste se registrovali na
                                            <strong>Events</strong>.
                                            Vaš nalog je uspešno kreiran.
                                        </p>

                                        <p style="
                                            font-size: 16px;
                                            line-height: 1.6;
                                            margin-bottom: 25px;
                                        ">
                                            Sada možete da istražujete događaje,
                                            pronađete one koji vam se dopadaju i
                                            rezervišete svoje ulaznice.
                                        </p>

                                        <div style="
                                            background-color: #f3f4f6;
                                            border-radius: 8px;
                                            padding: 20px;
                                            margin-bottom: 25px;
                                        ">
                                            <p style="
                                                margin: 0 0 8px 0;
                                                font-size: 14px;
                                                color: #6b7280;
                                            ">
                                                Registrovani email:
                                            </p>

                                            <p style="
                                                margin: 0;
                                                font-size: 16px;
                                                font-weight: bold;
                                                color: #1f2937;
                                            ">
                                                {user.Email}
                                            </p>
                                        </div>

                                        <p style="
                                            font-size: 16px;
                                            line-height: 1.6;
                                            margin-bottom: 0;
                                        ">
                                            Želimo vam prijatno iskustvo i
                                            puno nezaboravnih događaja!
                                        </p>

                                    </td>
                                </tr>

                                <!-- Footer -->
                                <tr>
                                    <td style="
                                        background-color: #f9fafb;
                                        padding: 20px 40px;
                                        text-align: center;
                                        border-top: 1px solid #e5e7eb;
                                    ">
                                        <p style="
                                            margin: 0;
                                            font-size: 13px;
                                            color: #6b7280;
                                        ">
                                            Ovo je automatski generisana poruka.
                                            Molimo vas da ne odgovarate na ovaj email.
                                        </p>

                                        <p style="
                                            margin: 8px 0 0 0;
                                            font-size: 13px;
                                            color: #9ca3af;
                                        ">
                                            © Events
                                        </p>
                                    </td>
                                </tr>

                            </table>

                        </td>
                    </tr>
                </table>
            </body>
            </html>
            """;

        return new EmailMessageDTO(
            RecipientEmail: user.Email,
            RecipientName: user.Name,
            Subject: "Dobrodošli na Events!",
            HtmlContent: htmlContent);
    }
}