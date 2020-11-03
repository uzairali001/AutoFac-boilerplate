using Autofac;
using Autofac.Core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyApp.Core.Bootstrap
{
    public static class AppContainer
    {
        private static IContainer _container;

        public static void Initialize()
        {
            var builder = new ContainerBuilder();

            //Bootstrap
            var profiles =
            from t in typeof(AppContainer).Assembly.GetTypes()
            where typeof(Profile).IsAssignableFrom(t)
            select (Profile)Activator.CreateInstance(t);

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                // Adding profiles to Mapper
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
           .As<IMapper>()
           .InstancePerLifetimeScope();

            #region Pages
            /* ----------------------------------------------- */
            //      Pages
            /* ----------------------------------------------- */


            /* ----------------------------------------------- */
            #endregion


            #region ViewModels
            /* ----------------------------------------------- */
            //      ViewModels
            /* ----------------------------------------------- */


            /* ----------------------------------------------- */
            #endregion

            #region Services - General
            /* ----------------------------------------------- */
            //      Services - General
            /* ----------------------------------------------- */


            /* ----------------------------------------------- */
            #endregion

            #region Services - Data
            /* ----------------------------------------------- */
            //      Services - Data
            /* ----------------------------------------------- */


            /* ----------------------------------------------- */
            #endregion

            #region Repositories
            /* ----------------------------------------------- */
            //      Repositories
            /* ----------------------------------------------- */


            /* ----------------------------------------------- */
            #endregion

            #region Converters
            /* ----------------------------------------------- */
            //      Converters
            /* ----------------------------------------------- */


            /* ----------------------------------------------- */
            #endregion

            #region Mapper Value Converter
            /* ----------------------------------------------- */
            //       Mapper Value Converter
            /* ----------------------------------------------- */


            /* ----------------------------------------------- */
            #endregion

            _container = builder.Build();
        }

        #region Resolve
        public static object Resolve(Type typeName)
            => _container.Resolve(typeName);
        public static object Resolve(Type typeName, params object[] parameters)
            => _container.Resolve(typeName, GetParameterList(parameters));
        public static object Resolve(Type typeName, IEnumerable<TypedParameter> parameters)
            => _container.Resolve(typeName, parameters);
        public static object Resolve(Type typeName, IEnumerable<NamedParameter> parameters)
           => _container.Resolve(typeName, parameters);

        public static TService Resolve<TService>() where TService : class
            => _container.Resolve<TService>();

        public static TService Resolve<TService>(params object[] parameters) where TService : class
            => _container.Resolve<TService>(GetParameterList(parameters));
        public static TService Resolve<TService>(IEnumerable<TypedParameter> parameters) where TService : class
            => _container.Resolve<TService>(parameters);
        public static TService Resolve<TService>(IEnumerable<NamedParameter> parameters) where TService : class
            => _container.Resolve<TService>(parameters);
        #endregion


        #region Resolve Optionals
        public static object ResolveOptional(Type typeName) =>
            _container.ResolveOptional(typeName);
        public static object ResolveOptional(Type typeName, params object[] parameters)
            => _container.ResolveOptional(typeName, GetParameterList(parameters));

        public static TService ResolveOptional<TService>() where TService : class
            => _container.ResolveOptional<TService>();

        public static TService ResolveOptional<TService>(params object[] parameters) where TService : class
            => _container.ResolveOptional<TService>(GetParameterList(parameters));
        #endregion


        #region Private Methods
        private static IEnumerable<Parameter> GetParameterList(params object[] parameters)
            => parameters.Select(x => new TypedParameter(x.GetType(), x)).ToList();
        #endregion

    }
}
