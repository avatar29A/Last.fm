namespace Hqub.Lastfm
{
    using System;

    /// <summary>
    /// This exception is thrown whenever a method is called thar required an authenticated 
    /// <see cref="Session"/> object and the given was not.
    /// </summary>
    public class NotAuthenticatedException : Exception
	{
		internal NotAuthenticatedException()
			:base("This method requires an authenticated session.")
		{
		}
	}
}
