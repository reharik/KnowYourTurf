using System;
using CC.Core.Enumerations;
using CC.Core.Localization;
using KnowYourTurf.Core.Domain;

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
        [ValueOf(typeof(State))]
        public string State { get; set; }
    }
}