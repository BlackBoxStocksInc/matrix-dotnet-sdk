// ReSharper disable MemberCanBePrivate.Global

using System;
using System.Net;

namespace BlackBoxStocks.Matrix.Sdk
{
    /// <summary>
    ///     Represents errors that occur from the Matrix API.
    /// </summary>
    public class ApiException : Exception
    {
        public ApiException(Uri uri, string? requestContent, string? responseContent, HttpStatusCode statusCode)
            : base($"Matrix API error. Status: {statusCode}, json: {responseContent}")
        {
            Uri = uri;
            RequestContent = requestContent;
            ResponseContent = responseContent;
            StatusCode = statusCode;
        }

        public Uri Uri { get; }

        public string? RequestContent { get; }

        public string? ResponseContent { get; }

        public HttpStatusCode StatusCode { get; }
    }
}