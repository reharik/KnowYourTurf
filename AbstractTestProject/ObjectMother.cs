using System;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Enums;
using KnowYourTurf.Core.Localization;

namespace AbstractTestProject
{
    public static class ObjectMother
    {
//       
//        public static License ValidLicense(string name)
//        {
//            return new License
//                       {
//                           AssetType = new AssetType {Name = "License"},
//                           IssueDate = DateTime.Parse("1/5/1972"),
//                           LicenseType = new LicenseType{Name = "my licesnse"}
//                       };
//        }
    }

    public class TestObject : DomainEntity
    {
        public virtual string Name { get; set; }
        public virtual DateTime Time { get; set; }
        [ValueOfEnumeration(typeof(State))]
        public string State { get; set; }
    }
}