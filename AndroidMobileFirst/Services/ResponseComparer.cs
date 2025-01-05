namespace AndroidMobileFirst.Services;

public static class ResponseComparer
{
    public static bool CompareResponse(string userResponse, string expectedResponse)
    {
        return string.Equals(userResponse, expectedResponse, StringComparison.OrdinalIgnoreCase);
    }
}