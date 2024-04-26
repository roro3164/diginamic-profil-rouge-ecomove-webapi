namespace ecomove_back.Helpers
{
    public class Tools
    {

        /// <summary>
        /// Permet de mettre la premier lettre en majuscule
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstLetterToUpper(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            string upper = s.ToUpper();
            string lower = s.ToLower();

            string result = upper[0] + lower[1..];

            return result;
        }
    }
}


