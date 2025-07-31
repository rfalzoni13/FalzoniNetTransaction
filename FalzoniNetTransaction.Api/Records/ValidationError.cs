namespace FalzoniNetTransaction.Api.Records;
/// <summary>
/// Validation Error record to represent validation errors in API responses.
/// </summary>
/// <param name="Field"></param>
/// <param name="Error"></param>
internal record ValidationError(string Field, IEnumerable<string> Error)
{
}