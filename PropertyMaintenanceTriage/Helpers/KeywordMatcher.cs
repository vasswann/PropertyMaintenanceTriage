namespace PropertyMaintenanceTriage.Helpers
{
    public static class KeywordMatcher
    {
        public static bool ContainsAny(string description, string[] keywords)
        {
            return keywords.Any(keyword =>
                description.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }
    }
}