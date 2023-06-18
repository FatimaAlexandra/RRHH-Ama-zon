using System;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using amazon.Models;

namespace amazon.Services{
    public class SendEmail
    {
        DbamazonContext context = new DbamazonContext();
        public void EnviarEmail(dynamic message)
        {
            var client = new SmtpClient();
            client.Connect("sandbox.smtp.mailtrap.io", 587, SecureSocketOptions.StartTls);
            client.Authenticate("681927af3e134d", "324fe0fe503705");

            client.Send(message);
            client.Disconnect(true);
        }

        public  dynamic GenerarPdf (int empleadoId, string fileName) {
            Empleado empleado = context.Empleados
                .Include(x => x.Contrato)
                    .ThenInclude(x => x.Acuerdo)
                .Include(d => d.Documento)
                .FirstOrDefault(x => x.Id == empleadoId);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Recursos Humanos", "eh18010@ues.edu.sv"));
            message.To.Add(new MailboxAddress(empleado.Nombre, empleado.Correo));

            message.Subject = "Contrato de confidencialidad";

            var builder = new BodyBuilder();

            // string html = empleado.Contrato.Acuerdo.Contenido;

            // Logica aqui pare remplazar los datos del empleado en el html
            string cuerpo = @"
<p>Estimado/a [nombre],</p>

<p>Esperamos que esté bien.</p>


<p>Adjunto a este correo electrónico, encontrará un Contrato de Confidencialidad. Este contrato es un estándar en nuestra industria y protege tanto a nuestra empresa como a usted. Asegura que la información sensible y confidencial que pueda llegar a manejar durante el curso de su empleo permanezca privada y segura.

Le rogamos que lea el contrato cuidadosamente, lo firme y nos lo devuelva a la mayor brevedad posible. Si tiene alguna pregunta o necesita aclaraciones sobre algún punto, no dude en ponerse en contacto con nosotros. Estamos aquí para ayudar.

Apreciamos su atención a este asunto y esperamos tener una relación laboral productiva y exitosa.

Atentamente,</p>

<p>Departamento de Recursos Humanos<p>
            ";

            cuerpo = cuerpo.Replace("[nombre]", empleado.Nombre);

            builder.HtmlBody = cuerpo;


             // Establecer el tipo de contenido HTML en el encabezado del mensaje
            var htmlEntity = new TextPart("html")
            {
                Text = builder.HtmlBody
            };

            var alternative = new Multipart("alternative");
            alternative.Add(htmlEntity);
            // Crear el contenido mixto (para adjuntos)
            var mixed = new Multipart("mixed");
            mixed.Add(alternative);

            if(File.Exists(fileName)) {
                // builder.Attachments.Add(fileName);
                var attachment = new MimePart()
                {
                    Content = new MimeContent(File.OpenRead(fileName), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(fileName)
                };
                mixed.Add(attachment);
            }


            // message.Body = builder.ToMessageBody();
            message.Body = mixed;
            
            return message;
        }

    }
    
}

