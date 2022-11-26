using System.Text.RegularExpressions;

namespace Domain.Shared;

public static class Utils
{
    public static string ToSlug(this string title)
    {
        var slug = title?
            .ToLower()
            .Trim()
            .Replace(' ', '-');

        // Replace all subsequent dashes with a single dash
        if (slug != null) slug = Regex.Replace(slug, @"[-]{2,}", "-");
        return slug;
    }
}