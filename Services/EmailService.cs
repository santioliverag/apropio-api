using System.Net;
using System.Net.Mail;
using System.Text;
using Apropio.API.Models;

namespace Apropio.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpClient _smtpClient;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpClient = new SmtpClient
            {
                Host = _configuration["Email:SmtpHost"] ?? "smtp.gmail.com",
                Port = int.Parse(_configuration["Email:SmtpPort"] ?? "587"),
                EnableSsl = bool.Parse(_configuration["Email:EnableSsl"] ?? "true"),
                Credentials = new NetworkCredential(
                    _configuration["Email:Username"] ?? "",
                    _configuration["Email:Password"] ?? ""
                )
            };
        }

        public async Task SendWelcomeEmailAsync(string email, string nombreCompleto)
        {
            var subject = "¡Bienvenido a Apropio!";
            var htmlContent = GenerateWelcomeEmailTemplate(nombreCompleto);
            
            await SendEmailAsync(email, subject, htmlContent);
        }

        public async Task SendPasswordResetEmailAsync(string email, string resetToken)
        {
            var subject = "Restablecer contraseña - Apropio";
            var resetUrl = $"{_configuration["Frontend:BaseUrl"]}/reset-password?token={resetToken}";
            var htmlContent = GeneratePasswordResetTemplate(resetUrl);
            
            await SendEmailAsync(email, subject, htmlContent);
        }

        public async Task SendContractNotificationAsync(string email, Contrato contrato)
        {
            var subject = $"Notificación de Contrato - {contrato.TipoContrato}";
            var htmlContent = GenerateContractNotificationTemplate(contrato);
            
            await SendEmailAsync(email, subject, htmlContent);
        }

        public async Task SendPaymentReminderAsync(string email, PagoAlquiler pago)
        {
            var subject = "Recordatorio de Pago de Alquiler";
            var htmlContent = GeneratePaymentReminderTemplate(pago);
            
            await SendEmailAsync(email, subject, htmlContent);
        }

        public async Task SendVisitConfirmationAsync(string email, Visita visita)
        {
            var subject = "Confirmación de Visita - Apropio";
            var htmlContent = GenerateVisitConfirmationTemplate(visita);
            
            await SendEmailAsync(email, subject, htmlContent);
        }

        public async Task SendPropertyUpdateAsync(string email, Inmueble inmueble, string updateType)
        {
            var subject = $"Actualización de Propiedad - {updateType}";
            var htmlContent = GeneratePropertyUpdateTemplate(inmueble, updateType);
            
            await SendEmailAsync(email, subject, htmlContent);
        }

        public async Task SendEmailAsync(string to, string subject, string htmlContent, string? plainTextContent = null)
        {
            var fromEmail = _configuration["Email:FromEmail"] ?? "noreply@apropio.com";
            var fromName = _configuration["Email:FromName"] ?? "Apropio";

            using var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromEmail, fromName);
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = htmlContent;
            mailMessage.IsBodyHtml = true;

            if (!string.IsNullOrEmpty(plainTextContent))
            {
                var plainView = AlternateView.CreateAlternateViewFromString(plainTextContent, Encoding.UTF8, "text/plain");
                mailMessage.AlternateViews.Add(plainView);
            }

            await _smtpClient.SendMailAsync(mailMessage);
        }

        public async Task SendBulkEmailAsync(List<string> recipients, string subject, string htmlContent)
        {
            var tasks = recipients.Select(email => SendEmailAsync(email, subject, htmlContent));
            await Task.WhenAll(tasks);
        }

        private string GenerateWelcomeEmailTemplate(string nombreCompleto)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Bienvenido a Apropio</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #2563eb; color: white; padding: 30px; text-align: center; }}
        .content {{ padding: 30px; background-color: #f8fafc; }}
        .footer {{ padding: 20px; text-align: center; font-size: 12px; color: #666; }}
        .btn {{ display: inline-block; padding: 12px 24px; background-color: #2563eb; color: white; text-decoration: none; border-radius: 5px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>¡Bienvenido a Apropio!</h1>
        </div>
        <div class='content'>
            <h2>Hola {nombreCompleto},</h2>
            <p>¡Gracias por registrarte en Apropio! Estamos emocionados de tenerte como parte de nuestra comunidad.</p>
            
            <p>Con Apropio podrás:</p>
            <ul>
                <li>Gestionar tus propiedades de manera eficiente</li>
                <li>Administrar contratos y pagos</li>
                <li>Programar visitas</li>
                <li>Y mucho más</li>
            </ul>
            
            <p>Si tienes alguna pregunta, no dudes en contactarnos.</p>
            
            <p>¡Bienvenido a bordo!</p>
            
            <p><strong>El equipo de Apropio</strong></p>
        </div>
        <div class='footer'>
            <p>© 2024 Apropio. Todos los derechos reservados.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GeneratePasswordResetTemplate(string resetUrl)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Restablecer Contraseña</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #dc2626; color: white; padding: 30px; text-align: center; }}
        .content {{ padding: 30px; background-color: #f8fafc; }}
        .footer {{ padding: 20px; text-align: center; font-size: 12px; color: #666; }}
        .btn {{ display: inline-block; padding: 12px 24px; background-color: #dc2626; color: white; text-decoration: none; border-radius: 5px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Restablecer Contraseña</h1>
        </div>
        <div class='content'>
            <p>Has solicitado restablecer tu contraseña de Apropio.</p>
            
            <p>Haz clic en el siguiente botón para crear una nueva contraseña:</p>
            
            <p style='text-align: center; margin: 30px 0;'>
                <a href='{resetUrl}' class='btn'>Restablecer Contraseña</a>
            </p>
            
            <p><strong>Este enlace expirará en 24 horas.</strong></p>
            
            <p>Si no solicitaste este cambio, puedes ignorar este email de forma segura.</p>
        </div>
        <div class='footer'>
            <p>© 2024 Apropio. Todos los derechos reservados.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GenerateContractNotificationTemplate(Contrato contrato)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Notificación de Contrato</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #059669; color: white; padding: 30px; text-align: center; }}
        .content {{ padding: 30px; background-color: #f8fafc; }}
        .footer {{ padding: 20px; text-align: center; font-size: 12px; color: #666; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Notificación de Contrato</h1>
        </div>
        <div class='content'>
            <h2>Nuevo Contrato Creado</h2>
            
            <p><strong>Tipo:</strong> {contrato.TipoContrato}</p>
            <p><strong>Fecha de Inicio:</strong> {contrato.FechaInicio:dd/MM/yyyy}</p>
            <p><strong>Fecha de Fin:</strong> {contrato.FechaFin:dd/MM/yyyy}</p>
            <p><strong>Monto:</strong> ${contrato.MontoAlquiler:N0}</p>
            
            <p>Para más detalles, ingresa a tu panel de control en Apropio.</p>
        </div>
        <div class='footer'>
            <p>© 2024 Apropio. Todos los derechos reservados.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GeneratePaymentReminderTemplate(PagoAlquiler pago)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Recordatorio de Pago</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #f59e0b; color: white; padding: 30px; text-align: center; }}
        .content {{ padding: 30px; background-color: #f8fafc; }}
        .footer {{ padding: 20px; text-align: center; font-size: 12px; color: #666; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Recordatorio de Pago</h1>
        </div>
        <div class='content'>
            <p>Te recordamos que tienes un pago de alquiler pendiente:</p>
            
            <p><strong>Monto:</strong> ${pago.Monto:N0}</p>
            <p><strong>Fecha de Vencimiento:</strong> {pago.FechaVencimiento:dd/MM/yyyy}</p>
            <p><strong>Período:</strong> {pago.PeriodoPago}</p>
            
            <p>Por favor, realiza el pago a la brevedad posible.</p>
        </div>
        <div class='footer'>
            <p>© 2024 Apropio. Todos los derechos reservados.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GenerateVisitConfirmationTemplate(Visita visita)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Confirmación de Visita</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #7c3aed; color: white; padding: 30px; text-align: center; }}
        .content {{ padding: 30px; background-color: #f8fafc; }}
        .footer {{ padding: 20px; text-align: center; font-size: 12px; color: #666; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Confirmación de Visita</h1>
        </div>
        <div class='content'>
            <p>Tu visita ha sido confirmada con los siguientes detalles:</p>
            
            <p><strong>Fecha y Hora:</strong> {visita.FechaHora:dd/MM/yyyy HH:mm}</p>
            <p><strong>Estado:</strong> {visita.Estado}</p>
            <p><strong>Comentarios:</strong> {visita.Comentarios}</p>
            
            <p>¡Te esperamos!</p>
        </div>
        <div class='footer'>
            <p>© 2024 Apropio. Todos los derechos reservados.</p>
        </div>
    </div>
</body>
</html>";
        }

        private string GeneratePropertyUpdateTemplate(Inmueble inmueble, string updateType)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Actualización de Propiedad</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #0891b2; color: white; padding: 30px; text-align: center; }}
        .content {{ padding: 30px; background-color: #f8fafc; }}
        .footer {{ padding: 20px; text-align: center; font-size: 12px; color: #666; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Actualización de Propiedad</h1>
        </div>
        <div class='content'>
            <p>Te informamos sobre una actualización en la siguiente propiedad:</p>
            
            <p><strong>Tipo de Actualización:</strong> {updateType}</p>
            <p><strong>Propiedad:</strong> {inmueble.Titulo}</p>
            <p><strong>Dirección:</strong> {inmueble.Direccion}, {inmueble.Ciudad}</p>
            <p><strong>Precio:</strong> ${inmueble.Precio:N0}</p>
            
            <p>Para más detalles, revisa tu panel de control.</p>
        </div>
        <div class='footer'>
            <p>© 2024 Apropio. Todos los derechos reservados.</p>
        </div>
    </div>
</body>
</html>";
        }

        public void Dispose()
        {
            _smtpClient?.Dispose();
        }
    }
} 