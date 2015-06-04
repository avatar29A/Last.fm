// ServiceException.cs
//
//  Copyright (C) 2008 Amr Hassan
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
//

using System;
using System.Collections.Generic;

namespace Lastfm.Services
{
	public enum ServiceExceptionType
	{
		InvalidService = 2,
		InvalidMethod = 3,
		AuthenticationFailed = 4,
		InvalidFormat = 5,
		InvalidParameters = 6,
		InvalidResource = 7,
		TokenError = 8,
		InvalidSessionKey = 9,
		InvalidAPIKey = 10,
		ServiceOffline = 11,
		SubscribersOnly = 12,
		InvalidSignature = 13,
		UnauthorizedToken = 14,
		ExpiredToken = 15
	}
	
	/// <summary>
	/// A Last.fm web service exception
	/// </summary>
	public class ServiceException : Exception
	{
		/// <summary>
		/// The exception type.
		/// </value>
		public ServiceExceptionType Type {get; private set;}
		
		/// <summary>
		/// The description of the exception.
		/// </summary>
		public string Description {get; private set;}
		
		public ServiceException(ServiceExceptionType type, string description) : base()
		{
			this.Type = type;
			this.Description = description;
		}
		
		public override string Message
		{
			get
			{
				/*
				Dictionary<ServiceExceptionType, string> messages = new Dictionary<ServiceExceptionType,string>();
				
				messages.Add(ServiceExceptionType.AuthenticationFailed, "Authentication Failed - You do not have permissions to access the service.");
				messages.Add(ServiceExceptionType.ExpiredToken, "Token Expired -This token has expired.");
				messages.Add(ServiceExceptionType.InvalidAPIKey, "Invalid API Key - You must be granted a valid key by last.fm.");
				messages.Add(ServiceExceptionType.InvalidFormat, "Invalid Format - This service doesn't exist in that format.");
				messages.Add(ServiceExceptionType.InvalidMethod, "Invalid Method - No method with that name in this package.");
				messages.Add(ServiceExceptionType.InvalidParameters, "Invalid Parameters - Your Request is missing a required parameter.");
				messages.Add(ServiceExceptionType.InvalidResource, "Invalid Resource Specified.");
				messages.Add(ServiceExceptionType.InvalidService, "Invalid Service - This service does not exist.");
				messages.Add(ServiceExceptionType.InvalidSessionKey, "Invalid Session Key - Please re-authenticate.");
				messages.Add(ServiceExceptionType.InvalidSignature, "Invalid method signature supplied.");
				messages.Add(ServiceExceptionType.ServiceOffline, "Service Offline - This service is temporarily offline. Try again later.");
				messages.Add(ServiceExceptionType.SubscribersOnly, "Subscribers Only - This service is only available to paid last.fm subscribers.");
				messages.Add(ServiceExceptionType.TokenError, "Token Error - There was an error granting the Request token.");
				messages.Add(ServiceExceptionType.UnauthorizedToken, "Unauthorized Token - This token has not been authorized.");
				*/
				
				
				return this.Type.ToString() + ": " + this.Description;
			}
		}
	}
}
