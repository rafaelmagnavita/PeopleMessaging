using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RabbitProjectFiles.Services
{
	public interface INotificationService
	{
		Task SendEmail(string email, string template, Dictionary<string, string> parameters);
		Task SendSms(string phoneNumber, string template, Dictionary<string, string> parameters);
	}
}

