namespace Focusu.GUI
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Helpers class.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Returns true if the <paramref name="text"/> contains only numbers.
        /// </summary>
        /// <param name="text">
        /// The text to check for numbers.
        /// </param>
        /// <returns>
        /// true if valid number, false if invalid.
        /// </returns>
        public static bool IsValidNumberInput(string text)
        {
            var regex = new Regex("[0-9]+");
            return regex.IsMatch(text);
        }

        /// <summary>
        /// Get the description attribute text of the given enum.
        /// https://stackoverflow.com/questions/7966102/how-to-assign-string-values-to-enums-and-use-that-value-in-a-switch/30174850#30174850
        /// </summary>
        /// <param name="e">
        /// The enum value target.
        /// </param>
        /// <returns>
        /// The <see cref="string"/> description.
        /// </returns>
        public static string GetDescription(this Enum e)
        {
            var attribute =
                e.GetType()
                        .GetTypeInfo()
                        .GetMember(e.ToString())
                        .FirstOrDefault(member => member.MemberType == MemberTypes.Field)
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .SingleOrDefault()
                    as DescriptionAttribute;

            return attribute?.Description ?? e.ToString();
        }
    }
}
