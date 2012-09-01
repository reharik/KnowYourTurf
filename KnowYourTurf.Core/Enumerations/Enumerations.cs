using System;
using KnowYourTurf.Core.Localization;

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
    public class State : Enumeration
    {
        public static readonly State Empty = new State { IsActive = false, Key = "" };
        public static readonly State AL = new State { IsActive = true, Key = "AL" };
        public static readonly State AK = new State { IsActive = true, Key = "AK" };
        public static readonly State AZ = new State { IsActive = true, Key = "AZ" };
        public static readonly State AR = new State { IsActive = true, Key = "AR" };
        public static readonly State CA = new State { IsActive = true, Key = "CA" };
        public static readonly State CO = new State { IsActive = true, Key = "CO" };
        public static readonly State CT = new State { IsActive = true, Key = "CT" };
        public static readonly State DE = new State { IsActive = true, Key = "DE" };
        public static readonly State FL = new State { IsActive = true, Key = "FL" };
        public static readonly State GA = new State { IsActive = true, Key = "GA" };
        public static readonly State HI = new State { IsActive = true, Key = "HI" };
        public static readonly State ID = new State { IsActive = true, Key = "ID" };
        public static readonly State IL = new State { IsActive = true, Key = "IL" };
        public static readonly State IA = new State { IsActive = true, Key = "IA" };
        public static readonly State KS = new State { IsActive = true, Key = "KS" };
        public static readonly State KY = new State { IsActive = true, Key = "KY" };
        public static readonly State LA = new State { IsActive = true, Key = "LA" };
        public static readonly State ME = new State { IsActive = true, Key = "ME" };
        public static readonly State MD = new State { IsActive = true, Key = "MD" };
        public static readonly State MA = new State { IsActive = true, Key = "MA" };
        public static readonly State MI = new State { IsActive = true, Key = "MI" };
        public static readonly State MN = new State { IsActive = true, Key = "MN" };
        public static readonly State MS = new State { IsActive = true, Key = "MS" };
        public static readonly State MO = new State { IsActive = true, Key = "MO" };
        public static readonly State MT = new State { IsActive = true, Key = "MT" };
        public static readonly State NE = new State { IsActive = true, Key = "NE" };
        public static readonly State NV = new State { IsActive = true, Key = "NV" };
        public static readonly State NH = new State { IsActive = true, Key = "NH" };
        public static readonly State NJ = new State { IsActive = true, Key = "NJ" };
        public static readonly State NM = new State { IsActive = true, Key = "NM" };
        public static readonly State NY = new State { IsActive = true, Key = "NY" };
        public static readonly State NC = new State { IsActive = true, Key = "NC" };
        public static readonly State ND = new State { IsActive = true, Key = "ND" };
        public static readonly State OH = new State { IsActive = true, Key = "OH" };
        public static readonly State OK = new State { IsActive = true, Key = "OK" };
        public static readonly State OR = new State { IsActive = true, Key = "OR" };
        public static readonly State PA = new State { IsActive = true, Key = "PA" };
        public static readonly State RI = new State { IsActive = true, Key = "RI" };
        public static readonly State SC = new State { IsActive = true, Key = "SC" };
        public static readonly State SD = new State { IsActive = true, Key = "SD" };
        public static readonly State TN = new State { IsActive = true, Key = "TN" };
        public static readonly State TX = new State { IsActive = true, Key = "TX" };
        public static readonly State UT = new State { IsActive = true, Key = "UT" };
        public static readonly State VT = new State { IsActive = true, Key = "VT" };
        public static readonly State VA = new State { IsActive = true, Key = "VA" };
        public static readonly State WA = new State { IsActive = true, Key = "WA" };
        public static readonly State WV = new State { IsActive = true, Key = "WV" };
        public static readonly State WI = new State { IsActive = true, Key = "WI" };
    }

    [Serializable]
    public class Country : Enumeration
    {
        public static readonly Country Empty = new Country { IsActive = false, Key = "" };
        public static readonly Country USA = new Country { IsActive = true, Key = "USA" };
        public static readonly Country Canada = new Country { IsActive = true, Key = "Canada" };
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
    public class AreaName : Enumeration
    {
        public static readonly AreaName Empty = new AreaName { IsActive = false, Key = "" };
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

}