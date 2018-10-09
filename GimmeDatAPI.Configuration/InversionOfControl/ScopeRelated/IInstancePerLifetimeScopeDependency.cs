namespace GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated
{
    /// <inheritdoc />
    ///  <summary>
    ///  This scope applies to nested lifetimes. 
    ///  A component with per-lifetime scope will have at most a single instance per nested lifetime scope.
    ///  This is useful for objects specific to a single unit of work 
    ///  that may need to nest additional logical units of work. 
    ///  Each nested lifetime scope will get a new instance of the registered dependency.
    ///  </summary>
    public interface IInstancePerLifetimeScopeDependency : IDependency
    {
        
    }
}