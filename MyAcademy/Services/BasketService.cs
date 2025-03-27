using MyAcademy.Models;

namespace MyAcademy.Services;

public class BasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Basket?> GetBasketAsync()
    {
        var response = await _httpClient
            .GetFromJsonAsync<Basket>("basket")
            .ConfigureAwait(false);

        return response;
    }

    public async Task AddItemToBasketAsync(Guid courseId)
    {
        var response = await _httpClient
            .PostAsync($"basket/{courseId}", null)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveItemFromBasketAsync(Guid courseId)
    {
        var response = await _httpClient
            .DeleteAsync($"basket/{courseId}")
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
    }
}