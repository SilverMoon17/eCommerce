namespace Application;

public class CommandStatus
{
    public bool IsSuccessful { get; set; } = true;
    public string ErrorMessage { get; set; } = String.Empty;

    public static CommandStatus Failed(string error)
    {
        return new()
        {
            IsSuccessful = false,
            ErrorMessage = error
        };
    }
}