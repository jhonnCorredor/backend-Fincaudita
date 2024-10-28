﻿using Business.Interfaces.Additional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implements.Additional
{
    public class WhatsappBusiness: IWhatsappBusiness
    {
        private readonly string _token;
        private readonly string _idTelefono;

        public WhatsappBusiness()
        {
            _token = "EAAEDUso0rUMBO4dLV8QAjZCwBMuEiHNz1hVKOgKLc9tVEtHWhbqRaJZAOa62jiuA7RqioAyPUe4voYw6gNVwZBs2D9WZCr28zZBb3ZA5qGyfya4JZAqeyD9X3vir1mP4KmHuMWF2rNrZBdwuyWAhhr1SDsHNexRZC4AlO7IURaGBcFZAJfjYqWVZBPJPFsoq2eI6ZAGG7QZDZD";
            _idTelefono = "493271527192708"; 
        }

        public async Task<string> EnviarMensajeAsync(string numeroWhatsApp, string template, string variable, string imageUrl)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://graph.facebook.com/v20.0/{_idTelefono}/messages");
                    request.Headers.Add("Authorization", $"Bearer {_token}");
                    string numeroConPrefijo = $"57{numeroWhatsApp}";
                    request.Content = new StringContent(@$"
                    {{
                        ""messaging_product"": ""whatsapp"",
                        ""to"": ""{numeroConPrefijo}"",
                        ""type"": ""template"",
                        ""template"": {{
                            ""name"": ""{template}"",
                            ""language"": {{
                                ""code"": ""es""
                            }},
                            ""components"": [
                                {{
                                    ""type"": ""header"",
                                    ""parameters"": [{{
                                        ""type"": ""image"",
                                        ""image"": {{
                                            ""link"": ""{imageUrl}""
                                        }}
                                    }}]
                                }},
                                {{
                                    ""type"": ""body"",
                                    ""parameters"": [{{
                                        ""type"": ""text"",
                                        ""text"": ""{variable}""
                                    }}]
                                }}
                            ]
                        }}
                    }}");
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return responseBody;
                }
            }
            catch (Exception ex)
            {
                return $"Error enviando mensaje: {ex.Message}";
            }
        }
    }
}