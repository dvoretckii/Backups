namespace Backups.Exceptions;

public class BackupsException : Exception
{
    private BackupsException(string message)
        : base(message) { }

    public static BackupsException ElementNotFound()
    {
        return new BackupsException("The element was not found");
    }

    public static BackupsException NullableVariable()
    {
        return new BackupsException("The result is null");
    }
}