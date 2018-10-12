namespace GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated
{
    /// <inheritdoc />
    /// <summary>
    /// Some application types naturally lend themselves to “request” type semantics, 
    /// for example ASP.NET web forms and MVC applications. 
    /// In these application types, it’s helpful to have the ability to have a sort of “singleton per request.”
    ///
    /// Instance per request builds on top of instance per matching lifetime scope 
    /// by providing a well-known lifetime scope tag, a registration convenience method, 
    /// and integration for common application types. 
    /// Behind the scenes, though, it’s still just instance per matching lifetime scope.
    ///
    /// What this means is that if you try to resolve components that are registered 
    /// as instance-per-request but there’s no current request… you’re going to get an exception.
    /// </summary>
    public interface IInstancePerRequestDependency : IDependency
    {
        
    }
}