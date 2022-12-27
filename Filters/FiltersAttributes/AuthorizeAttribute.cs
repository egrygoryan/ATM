namespace ATM.Filters.FiltersAttributes;

public class AuthorizeAttribute : TypeFilterAttribute
{
    public AuthorizeAttribute() : base(typeof(AuthorizeActionFilter)) { }
}