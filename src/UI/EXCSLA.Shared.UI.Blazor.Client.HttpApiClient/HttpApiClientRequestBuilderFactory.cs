using EXCSLA.Shared.UI.Blazor.Client.AlertService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EXCSLA.Shared.UI.Blazor.Client.HttpApiClient
{
    public class HttpApiClientRequestBuilderFactory : IHttpApiClientRequestBuilderFactory
    {
        private readonly NavigationManager _navigationManager;
        private readonly HttpClient _httpClient;
        private readonly IAlertService _alertService;

        public HttpApiClientRequestBuilderFactory(NavigationManager navigationManager, HttpClient httpClient, IAlertService alertService)
        {
            _navigationManager = navigationManager;
            _httpClient = httpClient;
            _alertService = alertService;
        }

        public HttpApiClientRequestBuilder Create(string url)
        {
            return new HttpApiClientRequestBuilder(_navigationManager, url, _httpClient, _alertService);
        }
    }
}
