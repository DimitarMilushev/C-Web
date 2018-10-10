namespace SIS.HTTP.Extensions
{
    public static class StringExtensions
    {
        public static string Capitalize(this string word)
        {
            if (word == null)
                return null;

            if (word.Length > 1)
                return char.ToUpper(word[0]) + word.Substring(1).ToLower();

            return word.ToUpper();
        }
    }
}
