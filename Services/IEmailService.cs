using Apropio.API.Models;

namespace Apropio.API.Services
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(string email, string nombreCompleto);
        Task SendPasswordResetEmailAsync(string email, string resetToken);
        Task SendContractNotificationAsync(string email, Contrato contrato);
        Task SendPaymentReminderAsync(string email, PagoAlquiler pago);
        Task SendVisitConfirmationAsync(string email, Visita visita);
        Task SendPropertyUpdateAsync(string email, Inmueble inmueble, string updateType);
        Task SendEmailAsync(string to, string subject, string htmlContent, string? plainTextContent = null);
        Task SendBulkEmailAsync(List<string> recipients, string subject, string htmlContent);
    }
} 