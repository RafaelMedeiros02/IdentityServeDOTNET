using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;

namespace GeekShopping.WEB.Utils
{
    public static class HttpClientExtensions
    {
        public static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");

        //RECEBE DA API E FORMATA DE JSON PARA OBJETO 
        public static async Task<T> ReadContentAs <T> (
            this HttpResponseMessage response  )
        {

            if (!response.IsSuccessStatusCode) throw
                    new ApplicationException(
                        $"Algo Aconteceu de Errado na Chamada da API: " +
                        $"{response.ReasonPhrase}"
                        );

            var dataAsString = await response.Content.ReadAsStringAsync ().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(dataAsString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        //ENVIA POST ( INSERT )  PARA API E CONVERTE PARA JSON 

        public static Task<HttpResponseMessage> PostAsJson<T>(
            this HttpClient httpClient,
            string url,
            T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;

            return httpClient.PostAsync(url, content);
        }


        //ENVIA PUT ( UPDATE )  PARA API E CONVERTE PARA JSON 

        public static Task<HttpResponseMessage> PutAsJson<T>(
            this HttpClient httpClient,
            string url,
            T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;

            return httpClient.PutAsync(url, content);
        }

    }

  
}
