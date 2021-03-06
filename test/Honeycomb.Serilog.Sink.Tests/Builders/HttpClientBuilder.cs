﻿using System;
using System.Net;
using System.Net.Http;

using Honeycomb.Serilog.Sink.Tests.Helpers;

namespace Honeycomb.Serilog.Sink.Tests.Builders
{
    public class HttpClientBuilder
    {
        private HttpStatusCode _statusCode;

        public static implicit operator HttpClientStub(HttpClientBuilder instance)
        {
            var handler = new HttpMessageHandlerStub();
            handler.ReturnsStatusCode(instance._statusCode);
            return new HttpClientStub(handler);
        }

        public HttpClientBuilder ThatReturnsStatusCode(HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
            return this;
        }
    }

    public class HttpClientStub : HttpClient
    {
        private readonly HttpMessageHandlerStub _handlerStub;

        public HttpRequestMessage RequestSubmitted => _handlerStub.RequestMessage;
        public string RequestContent => _handlerStub.RequestContent;

        public HttpClientStub(HttpMessageHandlerStub httpMessageHandler)
            : base(httpMessageHandler)
        {
            BaseAddress = new Uri("http://dummyUri");
            _handlerStub = httpMessageHandler;
        }
    }
}
