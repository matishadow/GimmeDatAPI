namespace GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated
{
    /// <inheritdoc />
    /// <summary>
    /// This is also known as ‘singleton.’ 
    ///
    /// Using single instance scope, one instance is returned 
    /// from all requests in the root and all nested scopes.
    /// </summary>
    public interface ISingleInstanceDependency : IDependency
    {
        
    }
}