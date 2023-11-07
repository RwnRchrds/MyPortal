using System;
using MyPortal.Database.Enums;
using SqlKata;

namespace MyPortal.Database.Constants;

public class Functions
{
    internal static Query GetName(string alias, Guid personId, NameFormat format, bool usePreferredName,
        bool includeMiddleName)
    {
        var query = new Query().FromRaw(
            $"GetName('{personId}', {format}, {(usePreferredName ? 1 : 0)}, {(includeMiddleName ? 1 : 0)})");

        query.Select($"{alias}.PersonId", $"{alias}.Name");

        return query;
    }

    internal static Query GetName(string alias, string personIdAlias, NameFormat format, bool usePreferredName,
        bool includeMiddleName)
    {
        var query = new Query().FromRaw(
            $"GetName({personIdAlias}, {format}, {(usePreferredName ? 1 : 0)}, {(includeMiddleName ? 1 : 0)})");

        query.Select($"{alias}.PersonId", $"{alias}.Name");

        return query;
    }
}