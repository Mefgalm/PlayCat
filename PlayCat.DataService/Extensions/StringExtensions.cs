namespace PlayCat.DataService.Extensions
{
    public static class StringExtensions
    {
        public static string ToLowerFirstCharacter(this string str)
        {
            if (str is null)
                return null;

            if (str == string.Empty)
                return string.Empty;
            
            return str[0].ToString().ToLower() + str.Substring(1);
        }
    }
}
