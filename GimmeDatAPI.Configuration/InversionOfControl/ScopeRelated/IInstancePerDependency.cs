namespace GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated
{
    /// <inheritdoc />
    ///  <summary>
    ///  Also called ‘transient’ or ‘factory’ in other containers. 
    ///  Using per-dependency scope, a unique instance will be returned from each request for a service.
    ///  </summary>
    public interface IInstancePerDependency : IDependency
    {
        
    }
}