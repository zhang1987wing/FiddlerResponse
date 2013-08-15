/*
 * Created by SharpDevelop.
 * User: yi.zhang
 * Date: 2013-8-9
 * Time: 14:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net.Mail;
using System.Net;

namespace FlashResponse
{
	/// <summary>
	/// Description of Email.
	/// </summary>
	public static class Email
	{	
		public static void sendMessage(string body)
		{
			
			IPHostEntry IPHost = Dns.Resolve(Dns.GetHostName());
            Console.WriteLine(IPHost.HostName);
            string[] aliases = IPHost.Aliases;

            IPAddress[] addr = IPHost.AddressList;
			
			MailMessage mailmessage = new MailMessage("yi.zhang@tiancity.com", "yi.zhang@tiancity.com", "FlashResponse错误  " + addr[0], body);
        	mailmessage.Priority = MailPriority.Normal;
        	SmtpClient smtpClient = new SmtpClient("192.168.10.143", 25);
        	smtpClient.Credentials = new NetworkCredential("yi.zhang", "1987wing");
            //smtpClient.EnableSsl = true;
        	smtpClient.Send(mailmessage); 
		}
	}
}
