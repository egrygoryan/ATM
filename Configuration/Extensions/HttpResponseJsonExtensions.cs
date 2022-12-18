namespace ATM.Configuration.Extensions;

public static class HttpResponseJsonExtensions
{
    public static Task WithJsonContent(this HttpResponse response, object content) =>
        response.WriteAsJsonAsync(content);
}
