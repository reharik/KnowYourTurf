using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using StructureMap;

namespace KnowYourTurf.Web.Config
{
    public class StructureMapControllerFactory : IControllerFactory
    {
        private readonly IContainer _container;
        private readonly IControllerFactory _innerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapControllerFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public StructureMapControllerFactory(IContainer container)
            : this(container, new DefaultControllerFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureMapControllerFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="innerFactory">The inner factory.</param>
        public StructureMapControllerFactory(IContainer container, IControllerFactory innerFactory)
        {
            _container = container;
            _innerFactory = innerFactory;
        }

        /// <summary>
        /// Creates the specified controller by using the specified request context.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>The controller.</returns>
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                return _container.GetInstance<IController>(controllerName.ToLowerInvariant());
            }
            catch (Exception)
            {
                return null; //._innerFactory.CreateController(requestContext, controllerName);
            }
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        /// <summary>
        /// Releases the specified controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public void ReleaseController(IController controller)
        {
            GC.SuppressFinalize(controller);
        }
    }

    public class StructureMapDependencyResolver : IDependencyResolver
    {
        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return _container.TryGetInstance(serviceType);
            }
            else
            {
                return _container.GetInstance(serviceType);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances<object>()

                .Where(s => s.GetType() == serviceType);
        }

        private readonly IContainer _container;
    }

    public class StructureMapControllerActivator : IControllerActivator
    {
        public StructureMapControllerActivator(IContainer container)
        {
            _container = container;
        }

        private IContainer _container;

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return _container.GetInstance(controllerType) as IController;
        }
    }

}