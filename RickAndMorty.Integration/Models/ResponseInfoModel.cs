namespace RickAndMorty.Core.Integration.Models
{
    public record ResponseInfoModel(
        long Count, 
        long Pages, 
        string? Next,
        string? Prev)
    {}
}