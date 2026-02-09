using EXCSLA.Shared.Core;
using EXCSLA.Shared.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ardalis.GuardClauses;

public static class DuplicateInListGuard
{
    public static void DuplicateInList<T>(this IGuardClause guardClause, T listItem, List<T> list) where T : BaseEntity<int>
    {
        guardClause.DuplicateInList<T, int>(listItem, list);
    }
    public static void DuplicateInList<T, TId>(this IGuardClause guardClause, T listItem, List<T> list) where T : BaseEntity<TId>
    {
        bool isDuplicate = false;

        foreach (var item in list)
        {
            if (item == listItem)
            {
                isDuplicate = true;
                break;
            }
        }

        if (isDuplicate) throw new ItemIsDuplicateException();
    }
}
