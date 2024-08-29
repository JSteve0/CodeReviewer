using System.Text;

namespace CodeReviewer.Utils;

public static class Utils {
    /// <summary>
    ///     Encodes a string to be represented as a string literal. The format
    ///     is essentially a JSON string.
    ///     The string returned includes outer quotes
    ///     Example Output: "Hello \"Rick\"!\r\nRock on"
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    /// <!--Modified from https://stackoverflow.com/questions/806944/escape-quote-in-c-sharp-for-javascript-consumption/59864990#59864990-->
    public static string EncodeJsString(string s) {
        var sb = new StringBuilder();
        sb.Append('"');

        // Fix quotes breaking JavaScript code 
        string sCopy = s.Replace("\\\"", "\"");

        foreach (char c in sCopy)
            switch (c) {
                case '\"':
                    Console.WriteLine("Hit");
                    sb.Append("\\\"");
                    break;
                case '\\':
                    sb.Append(@"\\");
                    break;
                case '\b':
                    sb.Append("\\b");
                    break;
                case '\f':
                    sb.Append("\\f");
                    break;
                case '\n':
                    sb.Append("\\n");
                    break;
                case '\r':
                    sb.Append("\\r");
                    break;
                case '\t':
                    sb.Append("\\t");
                    break;
                default:
                    int i = c;
                    // ReSharper disable once MergeIntoLogicalPattern
                    if (i < 32 || i > 127)
                        sb.Append($"\\u{i:X04}");
                    else
                        sb.Append(c);
                    break;
            }

        sb.Append('"');

        return sb.ToString();
    }
}