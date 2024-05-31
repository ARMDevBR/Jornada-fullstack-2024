﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Finan.Core.Responses
{
    public class Response<TData>
    {
        private int _code = Configuration.DefaultStatusCode; // Http Status Code

        [JsonConstructor]
        public Response() => _code = Configuration.DefaultStatusCode;

        public Response(TData? data, int code = Configuration.DefaultStatusCode, string? message = null)
        {
            Data = data;
            _code = code;
            Message = message;
        }

        public TData? Data { get; set; }
        public string? Message { get; set; }

        [JsonIgnore]
        public bool IsSuccess => _code is >= Configuration.FirstSuccessfulStatusCode and <= Configuration.LastSuccessfulStatusCode;
    }
}