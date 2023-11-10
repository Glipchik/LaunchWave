namespace ProjectService.BLL.Constants;

public static class ExceptionConstants
{
    public const string EndDateExpired = "End date of the request has expired";
    public const string RequestRejectedStatus = "Request has already been rejected. Cannot approve";
    public const string RequestAcceptedStatus = "Project has already been created. Cannot approve accepted status";
}
