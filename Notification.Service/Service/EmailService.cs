namespace Notification.Service.Service;


using System.Text;
using System.Net;
using System.Net.Mail;
using Notification.Service.Models;

public class EmailService : MailMessage
{
    
    // SMTP (Simple Mail Transfer Protocol)
    private static readonly string EMAIL_HOST = Environment.GetEnvironmentVariable("EMAIL_HOST")!;
    private static readonly string EMAIL_FROM = Environment.GetEnvironmentVariable("EMAIL_USERNAME")!;
    private static readonly string EMAIL_PASSWORD = Environment.GetEnvironmentVariable("EMAIL_PASSWORD")!;

    public static void Send(Message message)
    {
        try
        {   // tenta criar uma mensagem de e-mail da classe System.Net.Mail. O objeto é uma instância da classe MailMessage.
            using (var msgEmail = new MailMessage())
            {
                msgEmail.From = new MailAddress(EMAIL_FROM);
                // adiciona um ou mais endereços de e-mail de destino.
                msgEmail.To.Add(new MailAddress(message.MailTo!));
                // atributo que armazena o assunto do e-mail.
                msgEmail.Subject = message.Title;
                // atributo que armazena o corpo do e-mail.
                msgEmail.Body = message.Text;
                // atributo que armazena o tipo de encoding e charset do e-mail.
                msgEmail.BodyEncoding = Encoding.UTF8;
                // atributo que armazena se o corpo do e-mail é um HTML
                msgEmail.IsBodyHtml = true;
                // prioridade de envio do e-mail para o provedor.
                msgEmail.Priority = MailPriority.Normal;
                
                // Após criar o corpo do e-mail, ele é enviado através do SMTPClient
                using (var smtpClient = new SmtpClient())
                {
                    // atributo que armazena o servidor de SMTP.
                    smtpClient.Host = EMAIL_HOST;
                    // atributo que armazena a porta do serviço SMTP, no caso, a porta padrão 587.
                    smtpClient.Port = Int32.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT"));
                    // atributo que habilita ou não, a conexão segura. No caso, true.
                    smtpClient.EnableSsl = true;
                    // atributo que armazena o método de envio.
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    // atributo que armazena as credenciais que serão utilizadas para conectar no host. 
                    smtpClient.Credentials = new NetworkCredential(EMAIL_FROM, EMAIL_PASSWORD);
                    // atributo que armazena se as credenciais de conexão serão as default. No caso, será false porque utilizaremos as nossas credenciais.
                    smtpClient.UseDefaultCredentials = false;

                    smtpClient.Send(msgEmail);
                }
            }
        } // captura falha no método Send
        catch (SmtpFailedRecipientException ex)
        {
            Console.WriteLine("Message : {0} " + ex.Message);
            return;
        } // captura qualquer outra falha no SMTP.
        catch (SmtpException ex)
        {
            Console.WriteLine("Message SMPT Fail : {0} " + ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Message Exception : {0} " + ex.Message);
            return;
        }
    }
}