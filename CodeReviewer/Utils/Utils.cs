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
    public static string EncodeJsString(ReadOnlySpan<char> s) {
        var sb = new StringBuilder(s.Length + 2); // Pre-allocate capacity
        sb.Append('"');

        foreach (char c in s) {
            switch (c) {
                case '\"':
                    sb.Append("\\\""); // Escape double quotes
                    break;
                case '\\':
                    sb.Append(@"\\"); // Escape backslash
                    break;
                case '\b':
                    sb.Append("\\b"); // Escape backspace
                    break;
                case '\f':
                    sb.Append("\\f"); // Escape form feed
                    break;
                case '\n':
                    sb.Append("\\n"); // Escape new line
                    break;
                case '\r':
                    sb.Append("\\r"); // Escape carriage return
                    break;
                case '\t':
                    sb.Append("\\t"); // Escape tab
                    break;
                default:
                    if (c >= 32 && c <= 127) {
                        sb.Append(c); // Append printable ASCII directly
                    } else {
                        sb.AppendFormat("\\u{0:X04}", (int)c); // Unicode escape for non-ASCII
                    }
                    break;
            }
        }

        sb.Append('"');
        return sb.ToString();
    }

}
