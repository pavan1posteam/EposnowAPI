using System;
using System.Configuration;
using System.Net.Mail;

public class clsEmail
{
	public bool sendEmail(string to, string cc, string bcc, string subject, string body)
	{
		try
		{
			char[] separator = new char[1] { ',' };
			string[] array = to.Split(separator);
			MailMessage mailMessage = new MailMessage();
			for (int i = 0; i < array.Length; i++)
			{
				if ((!array[i].ToLower().StartsWith("undertesting") || !array[i].ToLower().EndsWith("@gmail.com")) && !array[i].ToLower().EndsWith("@mk.com") && (!array[i].ToLower().StartsWith("1") || !array[i].ToLower().EndsWith("@bottlecapps.com")))
				{
					mailMessage.To.Add(array[i]);
				}
			}
			if (cc != null && cc != "" && (!cc.ToLower().StartsWith("undertesting") || !cc.ToLower().EndsWith("@gmail.com")) && !cc.ToLower().EndsWith("@mk.com") && (!cc.ToLower().StartsWith("1") || !cc.ToLower().EndsWith("@bottlecapps.com")))
			{
				mailMessage.CC.Add(cc);
			}
			if (bcc != null && bcc != "")
			{
				string[] array2 = bcc.Split(separator);
				for (int j = 0; j < array2.Length; j++)
				{
					if ((!array2[j].ToLower().StartsWith("undertesting") || !array2[j].ToLower().EndsWith("@gmail.com")) && !array2[j].ToLower().EndsWith("@mk.com") && (!array2[j].ToLower().StartsWith("1") || !array2[j].ToLower().EndsWith("@bottlecapps.com")))
					{
						mailMessage.Bcc.Add(array2[j]);
					}
				}
			}
			mailMessage.Subject = subject;
			mailMessage.Body = body;
			mailMessage.IsBodyHtml = true;
			if (mailMessage.To.Count > 0)
			{
				SmtpClient smtpClient = new SmtpClient();
				smtpClient.Send(mailMessage);
			}
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message ?? "");
			return false;
		}
	}

	public bool sendEmailUser(string to, string cc, string bcc, string subject, string body, string FromStoreAddress)
	{
		try
		{
			char[] separator = new char[1] { ',' };
			string[] array = to.Split(separator);
			MailMessage mailMessage = new MailMessage();
			for (int i = 0; i < array.Length; i++)
			{
				if ((!array[i].ToLower().StartsWith("undertesting") || !array[i].ToLower().EndsWith("@gmail.com")) && !array[i].ToLower().EndsWith("@mk.com") && (!array[i].ToLower().StartsWith("1") || !array[i].ToLower().EndsWith("@bottlecapps.com")))
				{
					mailMessage.To.Add(array[i]);
				}
			}
			if (cc != null && cc != "" && (!cc.ToLower().StartsWith("undertesting") || !cc.ToLower().EndsWith("@gmail.com")) && !cc.ToLower().EndsWith("@mk.com") && (!cc.ToLower().StartsWith("1") || !cc.ToLower().EndsWith("@bottlecapps.com")))
			{
				mailMessage.CC.Add(cc);
			}
			if (bcc != null && bcc != "")
			{
				string[] array2 = bcc.Split(separator);
				for (int j = 0; j < array2.Length; j++)
				{
					if ((!array2[j].ToLower().StartsWith("undertesting") || !array2[j].ToLower().EndsWith("@gmail.com")) && !array2[j].ToLower().EndsWith("@mk.com") && (!array2[j].ToLower().StartsWith("1") || !array2[j].ToLower().EndsWith("@bottlecapps.com")))
					{
						mailMessage.Bcc.Add(array2[j]);
					}
				}
			}
			mailMessage.Subject = subject;
			mailMessage.Body = body;
			mailMessage.IsBodyHtml = true;
			string text = "";
			text = ConfigurationManager.AppSettings.Get("MailUserName");
			MailAddress mailAddress = new MailAddress(text, ConfigurationManager.AppSettings.Get("BCappsUserName").ToString());
			mailMessage.From = mailAddress;
			if (mailMessage.To.Count > 0)
			{
				using SmtpClient smtpClient = new SmtpClient();
				smtpClient.Send(mailMessage);
			}
			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message ?? "");
			return false;
		}
	}
}
