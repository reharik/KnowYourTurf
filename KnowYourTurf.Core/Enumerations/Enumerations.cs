using System;
using CC.Core.Localization;

namespace KnowYourTurf.Core.Enums
{
    public class Enumerations{}

    [Serializable]
    public class ValidationRule : Enumeration
    {
        public static readonly ValidationRule Empty = new ValidationRule { IsActive = false, Key = "" };
        public static readonly ValidationRule Required = new ValidationRule { IsActive = true, Key = "required" };
        public static readonly ValidationRule Digits = new ValidationRule { IsActive = true, Key = "digits" };
        public static readonly ValidationRule Range = new ValidationRule { IsActive = true, Key = "range" };
        public static readonly ValidationRule Number = new ValidationRule { IsActive = true, Key = "number" };
    }

    [Serializable]
    public class ExtraViewModelData : Enumeration
    {
        public static readonly ExtraViewModelData Empty = new ExtraViewModelData { IsActive = false, Key = "" };
        public static readonly ExtraViewModelData CssRules = new ExtraViewModelData { IsActive = true, Key = "class" };
        public static readonly ExtraViewModelData InputMasks = new ExtraViewModelData { IsActive = true, Key = "mask" };
    }

  
    [Serializable]
    public class UnitType : Enumeration
    {
        public static readonly UnitType Bags = new UnitType { IsActive = true, Key = "Bag(s)" };
        public static readonly UnitType Buckets = new UnitType { IsActive = true, Key = "Bucket(s)" };
        public static readonly UnitType Ea = new UnitType { IsActive = true, Key = "Ea." };
        public static readonly UnitType Gal = new UnitType { IsActive = true, Key = "Gal." };
        public static readonly UnitType Lbs = new UnitType { IsActive = true, Key = "Lb(s)"};
        public static readonly UnitType Oz = new UnitType { IsActive = true, Key = "Oz." };
        public static readonly UnitType Tons = new UnitType { IsActive = true, Key = "Ton(s)" };
        public static readonly UnitType Cans = new UnitType { IsActive = true, Key = "Can(s)" };
    }
    [Serializable]
    public class Status : Enumeration
    {
        public static readonly Status Empty = new Status { IsActive = false, Key = "" };
        public static readonly Status Active = new Status { IsActive = true, Key = "Active" };
        public static readonly Status InActive = new Status { IsActive = true, Key = "InActive" };
    }

    [Serializable]
    public class UserType : Enumeration
    {
        public static readonly UserType Empty = new UserType { IsActive = false, Key = "" };
        public static readonly UserType Employee = new UserType { IsActive = true, Key = "Employee", Value = "2"};
        public static readonly UserType Administrator = new UserType { IsActive = true, Key = "Administrator", Value = "1" };
        public static readonly UserType Facilities = new UserType { IsActive = true, Key = "Facilities", Value = "3" };
        public static readonly UserType MultiTenant = new UserType { IsActive = true, Key = "MultiTenant", Value = "4" };
        public static readonly UserType KYTAdmin = new UserType { IsActive = true, Key = "KYTAdmin", Value = "5" };
    }

    [Serializable]
    public class EmailFrequency : Enumeration
    {
        public static readonly EmailFrequency Empty = new EmailFrequency { IsActive = false, Key = "" };
        public static readonly EmailFrequency Once = new EmailFrequency { IsActive = true, Key = "Once" };
        public static readonly EmailFrequency Daily = new EmailFrequency { IsActive = true, Key = "Daily" };
        public static readonly EmailFrequency Weekly = new EmailFrequency { IsActive = true, Key = "Weekly" };
    }

    [Serializable]
    public class EmployeeType : Enumeration
    {
        public static readonly EmployeeType Empty = new EmployeeType { IsActive = false, Key = "" };
        public static readonly EmployeeType FullTime = new EmployeeType { IsActive = true, Key = "Full Time" };
        public static readonly EmployeeType Student = new EmployeeType { IsActive = true, Key = "Student" };
    }

    [Serializable]
    public class DocumentFileType : Enumeration
    {
        public static readonly DocumentFileType Empty = new DocumentFileType { IsActive = false, Key = "" };
        public static readonly DocumentFileType doc = new DocumentFileType { IsActive = true, Key = "Microsoft Word Document", Value = "doc" };
        public static readonly DocumentFileType ppt = new DocumentFileType { IsActive = true, Key = "Microsoft PowerPoint", Value = "ppt" };
        public static readonly DocumentFileType xls = new DocumentFileType { IsActive = true, Key = "xls", Value = "Microsoft Excel" };
        public static readonly DocumentFileType pdf = new DocumentFileType { IsActive = true, Key = "pdf", Value = "Adobe Acrobat" };
        public static readonly DocumentFileType zip = new DocumentFileType { IsActive = true, Key = "zip", Value = "ZIP File" };
        public static readonly DocumentFileType htm = new DocumentFileType { IsActive = true, Key = "htm", Value = "HTML" };
        public static readonly DocumentFileType vsd = new DocumentFileType { IsActive = true, Key = "vsd", Value = "Microsoft Visio" };
        public static readonly DocumentFileType txt = new DocumentFileType { IsActive = true, Key = "txt", Value = "Text File" };
    }

    [Serializable]
    public class AreaName : CC.Core.Enumerations.AreaName
    {
        public static readonly AreaName Empty = new AreaName { IsActive = false, Key = "" };
        public static readonly AreaName Reportsx = new AreaName { IsActive = false, Key = "Reportsx" };
        public static readonly AreaName Permissions = new AreaName { IsActive = false, Key = "Permissions" };
    }

    [Serializable]
    public class SecurityUserGroups : Enumeration
    {
        public static readonly SecurityUserGroups Empty = new SecurityUserGroups { IsActive = false, Key = "" };
        public static readonly SecurityUserGroups Administrator = new SecurityUserGroups { IsActive = true, Key = "Administrator" };
        public static readonly SecurityUserGroups Employee = new SecurityUserGroups { IsActive = true, Key = "Employee" };
        public static readonly SecurityUserGroups Facilities = new SecurityUserGroups { IsActive = true, Key = "Facilities" };
        public static readonly SecurityUserGroups KYTAdmin = new SecurityUserGroups { IsActive = true, Key = "KYTAdmin" };
        public static readonly SecurityUserGroups MultiTenant = new SecurityUserGroups { IsActive = true, Key = "MultiTenant" };
    }

    [Serializable]
    public class RepoConfig : Enumeration
    {
        public static readonly RepoConfig Empty = new RepoConfig { IsActive = false, Key = "" };
        public static readonly RepoConfig NoFilters = new RepoConfig { IsActive = true, Key = "NoFilters" };
        public static readonly RepoConfig NoFiltersOrInterceptor = new RepoConfig { IsActive = true, Key = "NoFiltersOrInterceptor" };
        public static readonly RepoConfig NoFiltersSpecialInterceptor = new RepoConfig { IsActive = true, Key = "NoFiltersSpecialInterceptor" };

    }

    [Serializable]
    public class SuiteOnlineStatusType : Enumeration
    {
        public static readonly SuiteOnlineStatusType Online = new SuiteOnlineStatusType { IsActive = true, Key = "Online" };
        public static readonly SuiteOnlineStatusType Offline = new SuiteOnlineStatusType { IsActive = true, Key = "Offline" };
        public static readonly SuiteOnlineStatusType Testing = new SuiteOnlineStatusType { IsActive = true, Key = "Testing" };
    }

}