namespace ATM.CustomResults;

public class NotInitializedResult : JsonResult
{
    public NotInitializedResult()
        : base(new { Title = "NotInitialized", Status = 400 }) { }
}