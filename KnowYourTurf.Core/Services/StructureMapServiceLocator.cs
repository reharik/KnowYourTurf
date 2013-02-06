using System;
using System.Collections.Generic;
using System.Linq;
using CC.Core;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace KnowYourTurf.Core.Services
{
    public class StructureMapServiceLocator : ServiceLocatorImplBase
    {

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return key.IsEmpty()
                       ? ObjectFactory.GetInstance(serviceType)
                       : ObjectFactory.GetNamedInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return ObjectFactory.GetAllInstances(serviceType).Cast<object>().AsEnumerable();
        }
    }
}