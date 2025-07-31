namespace FalzoniNetTransaction.Model;

public record struct SummaryStatistics
{
    public int Count { get; init; }
    public decimal Sum { get; init; }
    public decimal Average { get; init; }
    public decimal Min { get; init; }
    public decimal Max { get; init; }
    public SummaryStatistics(int count, decimal sum, decimal average, decimal min, decimal max)
    {
        Count = count;
        Sum = sum;
        Average = average;
        Min = min;
        Max = max;
    }
}