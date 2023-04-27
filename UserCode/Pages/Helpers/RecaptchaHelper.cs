﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using UserCode.Models;
using UserCode.Pages.Options;

namespace UserCode.Pages.Helpers
{
    public class RecaptchaHelper
    {
        private readonly RecaptchaOption _option;
        public RecaptchaHelper(IOptions<RecaptchaOption> option)
        {
            _option = option.Value;
        }
        public RecaptchaResponse ValidateCaptcha(string response)
        {
            using (var client = new WebClient())
            {
                string secret = _option.SecretKey;
                string url = $"{_option.Url}secret={secret}&response={response}";
                var result = client.DownloadString(url);
                try
                {
                    var data = JsonConvert.DeserializeObject<RecaptchaResponse>(result.ToString());
                    return data;
                }
                catch (Exception)
                {
                    return default(RecaptchaResponse);
                }
            }
        }
    }
}
