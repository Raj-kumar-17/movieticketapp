using System;
namespace MovieTicketApp.JWT_Token_Manager
{
	public interface IJwtAuthenticationManager
	{
		string Authenticate(string UserName, string PassWord);
	}
}

