namespace EXCSLA.Shared.UI.Blazor.Client.HttpApiClient
{
    public interface IHttpApiClientRequestBuilderFactory
    {
        HttpApiClientRequestBuilder Create(string url);
    }
}