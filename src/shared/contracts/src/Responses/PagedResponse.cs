namespace Contracts.Responses;

public sealed class PagedResponse<T>
{
    public required IReadOnlyCollection<T> Items { get; init; }

    public required int Total { get; init; }

    public required int Page { get; init; }

    public required int Limit { get; init; }
}