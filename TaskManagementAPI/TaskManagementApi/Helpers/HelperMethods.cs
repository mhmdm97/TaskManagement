namespace TaskManagementApi.Helpers
{
    public class HelperMethods
    {
        private static char[] delimiters = new char[] { ' ', '\r', '\n', '\t' };
        public static List<string> SplitStringToWordList(string query) => query.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
