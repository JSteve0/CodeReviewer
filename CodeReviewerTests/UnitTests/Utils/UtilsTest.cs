using SrcUtils = CodeReviewer.Utils.Utils;

// ReSharper disable ConvertToConstant.Local
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable UseVerbatimString

namespace CodeReviewerTests.UnitTests.Utils;

public class UtilsTests {

    [Fact]
    public void EncodeJsString_NormalString_ReturnsCorrectlyEncodedString() {
        var input = "Hello, World!";
        var expected = "\"Hello, World!\"";

        var result = SrcUtils.EncodeJsString(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void EncodeJsString_StringWithQuotes_ReturnsCorrectlyEncodedString() {
        var input = "Hello \"Rick\"!";
        var expected = "\"Hello \\\"Rick\\\"!\"";

        var result = SrcUtils.EncodeJsString(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void EncodeJsString_StringWithBackslashes_ReturnsCorrectlyEncodedString() {
        var input = "C:\\Program Files\\";
        var expected = "\"C:\\\\Program Files\\\\\"";

        var result = SrcUtils.EncodeJsString(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void EncodeJsString_StringWithControlCharacters_ReturnsCorrectlyEncodedString() {
        var input = "Hello\r\nWorld";
        var expected = "\"Hello\\r\\nWorld\"";

        var result = SrcUtils.EncodeJsString(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void EncodeJsString_StringWithSpecialCharacters_ReturnsCorrectlyEncodedString() {
        var input = "Hello\bWorld\t!";
        var expected = "\"Hello\\bWorld\\t!\"";

        var result = SrcUtils.EncodeJsString(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void EncodeJsString_StringWithUnicodeCharacters_ReturnsCorrectlyEncodedString() {
        var input = "Hello\u00A9World";
        var expected = "\"Hello\\u00A9World\"";

        var result = SrcUtils.EncodeJsString(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void EncodeJsString_EmptyString_ReturnsCorrectlyEncodedString() {
        var input = string.Empty;
        var expected = "\"\"";

        var result = SrcUtils.EncodeJsString(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void EncodeJsString_NullString_ThrowsArgumentNullException() {
        string? input = null;

        Assert.Throws<NullReferenceException>(() => SrcUtils.EncodeJsString(input!));
    }

}
