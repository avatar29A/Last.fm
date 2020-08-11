namespace Hqub.Lastfm
{
    using System;
    using System.Net;

    /// <summary>
    /// A last.fm web service exception
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// Gets the last.fm service method that caused the exception.
        /// </summary>
        public string Method { get; private set; }

        /// <summary>
        /// Gets the last.fm error code.
        /// </summary>
        public int ErrorCode { get; private set; }

        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class.
        /// </summary>
        /// <param name="method">The Last.fm service method that failed.</param>
        /// <param name="error">The error code.</param>
        /// <param name="message">The error message.</param>
        public ServiceException(string method, int error, string message) : base(message)
        {
            Method = method;
            ErrorCode = error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class.
        /// </summary>
        /// <param name="method">The last.fm service method that failed.</param>
        /// <param name="error">The error code.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="message">The error message.</param>
        public ServiceException(string method, int error, HttpStatusCode statusCode, string message) : base(message)
        {
            Method = method;
            ErrorCode = error;
            StatusCode = statusCode;
        }

        /// <summary>
        /// Gets a description of the error code.
        /// </summary>
        /// <param name="code">The error code</param>
        /// <returns></returns>
        /// <remarks>
        /// See https://www.last.fm/api/errorcodes
        /// </remarks>
        public static string GetErrorDescription(int code)
        {
            switch (code)
            {
                case 2:
                    return "Invalid service -This service does not exist.";
                case 3:
                    return "Invalid Method - No method with that name in this package.";
                case 4:
                    return "Authentication Failed - You do not have permissions to access the service.";
                case 5:
                    return "Invalid format - This service doesn't exist in that format.";
                case 6:
                    return "Invalid parameters - Your request is missing a required parameter.";
                case 7:
                    return "Invalid resource specified.";
                case 8:
                    return "Operation failed - Most likely the backend service failed. Please try again.";
                case 9:
                    return "Invalid session key - Please re-authenticate.";
                case 10:
                    return "Invalid API key - You must be granted a valid key by last.fm.";
                case 11:
                    return "Service Offline - This service is temporarily offline. Try again later.";
                case 12:
                    return "Subscribers Only - This station is only available to paid last.fm subscribers.";
                case 13:
                    return "Invalid method signature supplied.";
                case 14:
                    return "Unauthorized Token - This token has not been authorized.";
                case 15:
                    return "This item is not available for streaming.";
                case 16:
                    return "The service is temporarily unavailable, please try again.";
                case 17:
                    return "Login: User requires to be logged in.";
                case 18:
                    return "Trial Expired - This user has no free radio plays left. Subscription required.";
                case 20:
                    return "Not Enough Content - There is not enough content to play this station.";
                case 21:
                    return "Not Enough Members - This group does not have enough members for radio.";
                case 22:
                    return "Not Enough Fans - This artist does not have enough fans for for radio.";
                case 23:
                    return "Not Enough Neighbours - There are not enough neighbours for radio.";
                case 24:
                    return "No Peak Radio - This user is not allowed to listen to radio during peak usage.";
                case 25:
                    return "Radio Not Found - Radio station not found.";
                case 26:
                    return "API Key Suspended - This application is not allowed to make requests to the web services.";
                case 27:
                    return "Deprecated - This type of request is no longer supported.";
                case 29:
                    return "Rate Limit Exceded - Your IP has made too many requests in a short period, exceeding our API guidelines.";
            }

            return "This error does not exist.";
        }
    }
}
