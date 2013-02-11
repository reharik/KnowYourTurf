using System;
using System.Collections.Generic;
using System.Reflection;
using FubuCore.Conversion;
using FubuCore;
using StoryTeller;
using StoryTeller.Engine;

namespace KnowYourTurf.StoryTeller
{
    // TODO -- get tests around this thing.  Used heavy everyday already, but still
    public class GrammarSystem : ISystem
    {
        public virtual object Get(Type type)
        {
            if (type.IsConcreteWithDefaultCtor())
            {
                return Activator.CreateInstance(type);
            }

            throw new NotSupportedException("Get<T> is not supported by this ISystem:  " + GetType().FullName);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IExecutionContext CreateContext()
        {
            throw new NotImplementedException();
        }

        public void Recycle()
        {
            throw new NotImplementedException();
        }
    }


}