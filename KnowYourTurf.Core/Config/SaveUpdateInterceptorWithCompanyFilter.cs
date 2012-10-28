using System;
using CC.Core.Services;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using NHibernate;
using NHibernate.Type;
using StructureMap;

namespace KnowYourTurf.Core.Config
{
    public class SaveUpdateInterceptorWithCompanyFilter : EmptyInterceptor
    {
        public override bool OnFlushDirty(object entity,
                                          object id,
              object[] currentState,
              object[] previousState,
              string[] propertyNames,
              IType[] types)
        {
            return OnSave(entity, currentState, propertyNames);
        }
        public override bool OnSave(object entity,
                                    object id,
        object[] state,
        string[] propertyNames,
        IType[] types)
        {
            return OnSave(entity, state, propertyNames);
        }

        private static bool OnSave(object entity, object[] state, string[] propertyNames)
        {
            var domainEntity = entity as DomainEntity;
            if (domainEntity == null) return false;
            if (entity is DomainEntity)
            {
                var sessionContext = ObjectFactory.Container.GetInstance<ISessionContext>();
                var currentUser = sessionContext.GetCurrentUser();
                var systemClock = ObjectFactory.Container.GetInstance<ISystemClock>();
                var getCompanyIdService = ObjectFactory.GetInstance<IGetCompanyIdService>();
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    if ("ChangedDate".Equals(propertyNames[i]))
                    {
                        state[i] = systemClock.Now;
                    }
                    if (!domainEntity.CreatedDate.HasValue && "CreatedDate".Equals(propertyNames[i]))
                    {
                        state[i] = systemClock.Now;
                    }
                    if ("CompanyId".Equals(propertyNames[i]))
                    {
                        state[i] = getCompanyIdService.Execute();
                    }
                    if (domainEntity.CreatedBy ==null && "CreatedBy".Equals(propertyNames[i]))
                    {
                        state[i] = currentUser;
                    }
                    if ("ChangedBy".Equals(propertyNames[i]))
                    {
                        state[i] = currentUser;
                    }
                }
                return true;
            }
            return false;
        }
    }
    public class SaveUpdateInterceptor : EmptyInterceptor
    {
        public override bool OnFlushDirty(object entity,
                                          object id,
              object[] currentState,
              object[] previousState,
              string[] propertyNames,
              IType[] types)
        {
            return OnSave(entity, currentState, propertyNames);
        }
        public override bool OnSave(object entity,
                                    object id,
        object[] state,
        string[] propertyNames,
        IType[] types)
        {
            return OnSave(entity, state, propertyNames);
        }

        private static bool OnSave(object entity, object[] state, string[] propertyNames)
        {
            var domainEntity = entity as DomainEntity;
            if (domainEntity == null) return false;
            if (entity is DomainEntity)
            {
                var systemClock = ObjectFactory.Container.GetInstance<ISystemClock>();
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    if ("LastModified".Equals(propertyNames[i]))
                    {
                        state[i] = systemClock.Now;
                    }
                    if (!domainEntity.CreatedDate.HasValue && "DateCreated".Equals(propertyNames[i]))
                    {
                        state[i] = systemClock.Now;
                    }
                }
                return true;
            }
            return false;
        }
    }
}