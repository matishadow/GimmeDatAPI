using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using GimmeDatAPI.Configuration.InversionOfControl.RegistrationRelated;
using GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated;
using Microsoft.Extensions.DependencyModel;
using IContainer = System.ComponentModel.IContainer;

namespace GimmeDatAPI.Configuration.InversionOfControl
{
    public static class InversionOfControlConfiguration
    {
        private const string ApplicationName = "GimmeDat";
        
        public static void Register(ContainerBuilder containerBuilder)
        {
            IEnumerable<Assembly> applicationAssemblies = GetApplicationAssemblies(ApplicationName);            
        
            RegisterTypes(applicationAssemblies, containerBuilder);
        }
        
        private static bool IsCandidateLibrary(Library library, string assemblyName)
        {
            return library.Name.StartsWith(assemblyName)
                   || library.Dependencies.Any(d => d.Name.StartsWith(assemblyName));
        }

        private static IEnumerable<Assembly> GetApplicationAssemblies(string applicationName)
        {
            IReadOnlyList<RuntimeLibrary> dependencies = DependencyContext.Default.RuntimeLibraries;
            List<Assembly> assemblies = (from library in dependencies
                where IsCandidateLibrary(library, applicationName)
                select Assembly.Load(new AssemblyName(library.Name))).ToList();

            return assemblies;
        }

        private static void RegisterTypes(IEnumerable<Assembly> assemblies, ContainerBuilder builder)
        {
            foreach (Assembly assembly in assemblies)
            {
                RegisterInstanceDependenciesAsInterfaces(builder, assembly);
                RegisterInstanceDependenciesAsSelf(builder, assembly);
                RegisterLifetimeScopeAsInterfaces(builder, assembly);
                RegisterLifetimeScopeAsSelf(builder, assembly);
                RegisterInstanceRequestAsInterfaces(builder, assembly);
                RegisterInstanceRequestAsSelf(builder, assembly);
                RegisterSingleInstanceAsInterfaces(builder, assembly);
                RegisterSingleInstanceAsSelf(builder, assembly);
            }
        }

        private static void RegisterInstanceDependenciesAsInterfaces(ContainerBuilder builder, Assembly assembly)
        {
            Type[] types =
                FilterTypesByInterfaces(assembly.GetTypes(), 
                    typeof(IInstancePerDependency),
                    typeof(IAsImplementedInterfacesDependency));

            builder.RegisterTypes(types)
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }

        private static void RegisterInstanceDependenciesAsSelf(ContainerBuilder builder, Assembly assembly)
        {
            Type[] types =
                FilterTypesByInterfaces(assembly.GetTypes(), 
                    typeof(IInstancePerDependency),
                    typeof(IAsSelfRegistrationDependency));

            builder.RegisterTypes(types)
                .AsSelf()
                .InstancePerDependency();
        }

        private static void RegisterLifetimeScopeAsInterfaces(ContainerBuilder builder, Assembly assembly)
        {
            Type[] types =
                FilterTypesByInterfaces(assembly.GetTypes(), 
                    typeof(IInstancePerLifetimeScopeDependency),
                    typeof(IAsImplementedInterfacesDependency));

            builder.RegisterTypes(types)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private static void RegisterLifetimeScopeAsSelf(ContainerBuilder builder, Assembly assembly)
        {
            Type[] types =
                FilterTypesByInterfaces(assembly.GetTypes(), 
                    typeof(IInstancePerLifetimeScopeDependency),
                    typeof(IAsSelfRegistrationDependency));

            builder.RegisterTypes(types)
                .AsSelf()
                .InstancePerLifetimeScope();
        }

        private static void RegisterInstanceRequestAsInterfaces(ContainerBuilder builder, Assembly assembly)
        {
            Type[] types =
                FilterTypesByInterfaces(assembly.GetTypes(), 
                    typeof(IInstancePerRequestDependency),
                    typeof(IAsImplementedInterfacesDependency));

            builder.RegisterTypes(types)
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }

        private static void RegisterInstanceRequestAsSelf(ContainerBuilder builder, Assembly assembly)
        {
            Type[] types =
                FilterTypesByInterfaces(assembly.GetTypes(), 
                    typeof(IInstancePerRequestDependency),
                    typeof(IAsSelfRegistrationDependency));

            builder.RegisterTypes(types)
                .AsSelf()
                .InstancePerRequest();
        }

        private static void RegisterSingleInstanceAsInterfaces(ContainerBuilder builder, Assembly assembly)
        {
            Type[] types =
                FilterTypesByInterfaces(assembly.GetTypes(), 
                    typeof(ISingleInstanceDependency),
                    typeof(IAsImplementedInterfacesDependency));

            builder.RegisterTypes(types)
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private static void RegisterSingleInstanceAsSelf(ContainerBuilder builder, Assembly assembly)
        {
            Type[] types =
                FilterTypesByInterfaces(assembly.GetTypes(),
                    typeof(ISingleInstanceDependency),
                    typeof(IAsSelfRegistrationDependency));

            builder.RegisterTypes(types)
                .AsSelf()
                .SingleInstance();
        }

        private static Type[] FilterTypesByInterfaces(IEnumerable<Type> types, params Type[] interfaces)
        {
            return (from type in types
                let hasAllInterfaces = interfaces.All(filteringInterface => filteringInterface.IsAssignableFrom(type))
                where hasAllInterfaces
                select type).ToArray();
        }
    }
}