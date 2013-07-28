using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Services;
using NHibernate;
using NHibernate.Type;
using StructureMap;

namespace KnowYourTurf.Core.Config
{
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

        private static bool OnSave(object item, object[] state, string[] propertyNames)
        {
            var getSettingsFromPrincipal = ObjectFactory.GetInstance<ISessionContext>();
            var systemClock = ObjectFactory.Container.GetInstance<ISystemClock>();

            var domainEntity = item as DomainEntity;
            if (domainEntity != null)
            {
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    if ("ChangeDate".Equals(propertyNames[i]))
                    {
                        state[i] = systemClock.Now;
                    }
                    if (!domainEntity.CreateDate.HasValue && "CreateDate".Equals(propertyNames[i]))
                    {
                        state[i] = systemClock.Now;
                    }
                    if ("ChangedBy".Equals(propertyNames[i]))
                    {
                        state[i] = getSettingsFromPrincipal.GetUserEntityId();
                    }
                    if ("CompanyId".Equals(propertyNames[i]))
                    {
                        state[i] = getSettingsFromPrincipal.GetCompanyId();
                    }
                }
                return true;
            }
           
            var entity = item as Entity;
            if (entity != null)
            {
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    if ("ChangeDate".Equals(propertyNames[i]))
                    {
                        state[i] = systemClock.Now;
                    }
                    if (!entity.CreateDate.HasValue && "CreateDate".Equals(propertyNames[i]))
                    {
                        state[i] = systemClock.Now;
                    }
                    if ("ChangedBy".Equals(propertyNames[i]))
                    {
                        state[i] = getSettingsFromPrincipal.GetUserEntityId();
                    }
                }
                return true;
            }
            return false;

        }
    }
}