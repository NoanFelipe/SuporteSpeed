using Blazored.LocalStorage;
using System.Security.Principal;

namespace SuporteSpeed.Blazor.Server.UI.Services.Base
{
    public class BaseHttpService
    {
        private readonly IClient client;
        private readonly ILocalStorageService localStorage;

        public BaseHttpService(IClient client, ILocalStorageService localStorage) 
        {
            this.client = client;
            this.localStorage = localStorage;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>() { Message = "Erros de validação ocorreram.", ValidationErrors = ex.Response, Success = false };
            }
            if (ex.StatusCode == 404)
            {
                return new Response<Guid>() { Message = "O item requisitado não foi encontrado.", ValidationErrors = ex.Response, Success = false };
            }
            
            if (ex.StatusCode >= 200 && ex.StatusCode <= 299)
            {
                return new Response<Guid>() { Message = "Operação foi um sucesso.", Success = true };
            }

            return new Response<Guid>() { Message = "Algo deu errado, por favor tente novamente.", ValidationErrors = ex.Response, Success = false };

        }

        protected async Task GetBearerToken()
        {
            var token = await localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                client.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            }
        }
    }
}
