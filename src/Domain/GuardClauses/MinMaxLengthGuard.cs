using EXCSLA.Shared.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.GuardClauses;

public static class MinMaxLengthGuards
{
    public static void MinLengthGuard(this IGuardClause guardClause, string value, int minLength)
    {
        if (value.Length < minLength) throw new MinimumLengthExceededException($"This requires a minimum length of {minLength}");
    }

    public static void MaxLengthGuard(this IGuardClause guardClause, string value, int maxLength)
    {
        if (value.Length > maxLength) throw new MaximumLengthExceededException($"This requires a maximum length of {maxLength}");
    }

    public static void MinMaxLengthGuard(this IGuardClause guardClause, string value, int minLength, int maxLength)
    {

        if (value.Length < minLength && value.Length > maxLength) throw new MinimumLengthExceededException($"This requires a minimum length of {minLength}",
            new MaximumLengthExceededException($"This requires a maximum length of {maxLength}"));

        if (value.Length < minLength) throw new MinimumLengthExceededException($"This requires a minimum length of {minLength}");
        if (value.Length > maxLength) throw new MaximumLengthExceededException($"This requires a maximum length of {maxLength}");

    }
}
