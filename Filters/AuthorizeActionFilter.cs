using ATM.CustomResults;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ATM.Filters;

public class AuthorizeActionFilter : IAuthorizationFilter
{
    private static bool _inited;
    private static bool _authorized;
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if(context.ActionDescriptor is ControllerActionDescriptor descriptor)
        {
            switch (descriptor.ActionName)
            {
                case "Init":
                    _inited = true;
                    break;
                case "Authorize" when _inited:
                    _authorized = true;
                    break;
                case "Authorize" when !_inited:
                    context.Result = new NotInitializedResult();
                    break;
                case "Withdraw" or "GetBalance" when !_authorized:
                    _inited = false;
                    context.Result = new UnauthorizedResult();
                    break;
                case "Withdraw" or "GetBalance" when _authorized:
                    _inited = false;
                    _authorized = false;
                    break;
            }
        }
    }
}