namespace GimmeDatAPI.Configuration.InversionOfControl.RegistrationRelated
{
    /// <inheritdoc />
    /// <summary>
    /// Register types as themselves - useful when also overriding the default with another service specification.
    /// </summary>
    public interface IAsSelfRegistrationDependency : IDependency
    {
        
    }
}