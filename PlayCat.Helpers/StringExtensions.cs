namespace PlayCat.Helpers
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

        //public static string AddExtension(this string str, string extension)
        //{
        //    if (str is null)
        //        return null;

        //    if (str == string.Empty)
        //        return string.Empty;

        //    if (string.IsNullOrWhiteSpace(extension))
        //        return str;

        //    return $"{str}.{extension}";
        //}
    }
}
