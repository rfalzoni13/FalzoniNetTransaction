using System.ComponentModel.DataAnnotations;

namespace FalzoniNetTransaction.Model.Annotations;

internal class MinDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not DateTime)
        {
            return false;
        }

        return (DateTime)value <= DateTime.Now;
    }
}
