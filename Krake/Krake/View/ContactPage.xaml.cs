using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Krake.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
            InfoLabel.Text = "Moin! \nWenn dir ein Event oder eine Location hier in unserer App fehlt schreib uns einfach. \nFeedback ist auch immer gern gesehen! \nE-Mail Adresse: info@krake-party.de \n";
        }
        private async void SendConatctButton_Clicked(object sender, EventArgs e)
        {
            //try
            //{
            //    MailMessage mailMessage = new MailMessage();
            //    SmtpClient smtp = new SmtpClient("smtp.ionos.de");

            //    mailMessage.From = new MailAddress(EMailContactEntry.Text);
            //    mailMessage.To.Add("info@krake-party.de");
            //    mailMessage.Subject = "test";
            //    mailMessage.Body = TextContactEntry.Text;

            //    smtp.Send(mailMessage);
            //    //smtp.Port = 587;
            //    //smtp.Host = "smtp.ionos.de";
            //    //smtp.EnableSsl = true;
            //    //smtp.UseDefaultCredentials = false;
            //    //smtp.Credentials = new System.Net.NetworkCredential
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            try
            {
                if (SubjectContactEntry.Text == "" || SubjectContactEntry.Text == null)
                {
                    await DisplayAlert("Error", "Bitte Betreff eintragen", "Ok");

                }
                else if (TextContactEntry.Text == "" || TextContactEntry.Text == null)
                {
                    await DisplayAlert("Error", "Bitte einen Text eintragen", "Ok");

                }
                else
                {
                    List<string> mailingList = new List<string>();
                    mailingList.Add("info@krake-party.de");
                    var message = new EmailMessage
                    {
                        Subject = SubjectContactEntry.Text,
                        Body = TextContactEntry.Text,
                        To = mailingList,
                        //Cc = ccRecipients,
                        //Bcc = bccRecipients
                    };
                    await Email.ComposeAsync(message);
                }
            }


            catch (FeatureNotSupportedException)
            {
                // Email is not supported on this device
            }
            catch (Exception)
            {
                // Some other exception occurred
            }
        }
    }
}