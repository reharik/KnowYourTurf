using System;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core.Enumerations
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
        public static readonly ValidationRule Email = new ValidationRule { IsActive = true, Key = "email" };
        public static readonly ValidationRule Url = new ValidationRule { IsActive = true, Key = "url" };
        public static readonly ValidationRule Date = new ValidationRule { IsActive = true, Key = "date" };

    }

    [Serializable]
    public class ExtraViewModelData : Enumeration
    {
        public static readonly ExtraViewModelData Empty = new ExtraViewModelData { IsActive = false, Key = "" };
        public static readonly ExtraViewModelData CssRules = new ExtraViewModelData { IsActive = true, Key = "class" };
        public static readonly ExtraViewModelData InputMasks = new ExtraViewModelData { IsActive = true, Key = "mask" };
    }
    [Serializable]
    public class Status : Enumeration
    {
        public static readonly Status Empty = new Status { IsActive = false, Key = "" };
        public static readonly Status Active = new Status { IsActive = true, Key = "Active" };
        public static readonly Status InActive = new Status { IsActive = true, Key = "InActive" };
    }

    [Serializable]
    public class Source : Enumeration
    {
        public static readonly Source Empty = new Source { IsActive = false, Key = "" };
        public static readonly Source Street = new Source { IsActive = true, Key = "Street" };
        public static readonly Source Web = new Source { IsActive = true, Key = "Web" };
        public static readonly Source Ad = new Source { IsActive = true, Key = "Ad" };
    }

    [Serializable]
    public class RuleOperatorEnum : Enumeration
    {
        public static readonly RuleOperatorEnum Empty = new RuleOperatorEnum { IsActive = false, Key = "" };
        public static readonly RuleOperatorEnum And = new RuleOperatorEnum { IsActive = true, Key = "And", Value = "And" };
        public static readonly RuleOperatorEnum Or = new RuleOperatorEnum { IsActive = true, Key = "Or", Value = "Or" };
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
        public static readonly State IN = new State { IsActive = true, Key = "IN" };
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
        public static readonly State WY = new State { IsActive = true, Key = "WY" };
        public static readonly State AS = new State { IsActive = true, Key = "AS" };
        public static readonly State DC = new State { IsActive = true, Key = "DC" };
        public static readonly State FM = new State { IsActive = true, Key = "FM" };
        public static readonly State GU = new State { IsActive = true, Key = "GU" };
        public static readonly State MH = new State { IsActive = true, Key = "MH" };
        public static readonly State MP = new State { IsActive = true, Key = "MP" };
        public static readonly State PW = new State { IsActive = true, Key = "PW" };
        public static readonly State PR = new State { IsActive = true, Key = "PR" };
        public static readonly State VI = new State { IsActive = true, Key = "VI" };
        
    }

    [Serializable]
    public class Country : Enumeration
    {
        public static readonly Country Empty = new Country { IsActive = false, Key = "" };
        public static readonly Country USA = new Country { IsActive = true, Key = "USA" };
        public static readonly Country France = new Country { IsActive = true, Key = "France" };
    }

    [Serializable]
    public class DocumentFileType : Enumeration
    {
        public static readonly DocumentFileType Empty = new DocumentFileType { IsActive = false, Key = "" };
        public static readonly DocumentFileType Document = new DocumentFileType { IsActive = true, Key = "Document" };
        public static readonly DocumentFileType Image = new DocumentFileType { IsActive = true, Key = "Image"};
        public static readonly DocumentFileType Headshot = new DocumentFileType { IsActive = true, Key = "Headshot"};
    }

    [Serializable]
    public class AreaName : Enumeration
    {
        public static readonly AreaName Empty = new AreaName { IsActive = false, Key = "" };
        public static readonly AreaName Schedule = new AreaName { IsActive = true, Key = "Schedule" };
    }

    [Serializable]
    public class Minutes : Enumeration
    {
        public static readonly Minutes Empty = new Minutes { IsActive = false, Key = "" };
        public static readonly Minutes Zero = new Minutes { IsActive = true, Key = "00" };
        public static readonly Minutes Fifteen = new Minutes { IsActive = true, Key = "15" };
        public static readonly Minutes Thirty = new Minutes { IsActive = true, Key = "30" };
        public static readonly Minutes FortyFive = new Minutes { IsActive = true, Key = "45" };
    }

    [Serializable]
    public class Hours : Enumeration
    {
        public static readonly Hours Empty = new Hours {IsActive = false, Key = ""};
        public static readonly Hours One = new Hours { IsActive = true, Key = "1" };
        public static readonly Hours Two = new Hours { IsActive = true, Key = "2" };
        public static readonly Hours Three = new Hours { IsActive = true, Key = "3" };
        public static readonly Hours Four = new Hours { IsActive = true, Key = "4" };
        public static readonly Hours Five = new Hours { IsActive = true, Key = "5" };
        public static readonly Hours Six = new Hours { IsActive = true, Key = "6" };
        public static readonly Hours Seven = new Hours { IsActive = true, Key = "7" };
        public static readonly Hours Eight = new Hours { IsActive = true, Key = "8" };
        public static readonly Hours Nine = new Hours { IsActive = true, Key = "9" };
        public static readonly Hours Ten = new Hours { IsActive = true, Key = "10" };
        public static readonly Hours Eleven = new Hours { IsActive = true, Key = "11" };
        public static readonly Hours Twelve = new Hours { IsActive = true, Key = "12" };
    }

    [Serializable]
    public class AMPM : Enumeration
    {
        public static readonly AMPM Empty = new AMPM { IsActive = false, Key = "" };
        public static readonly AMPM AM = new AMPM { IsActive = true, Key = "AM" };
        public static readonly AMPM PM = new AMPM { IsActive = true, Key = "PM" };
    }

    [Serializable]
    public class SecurityUserGroups : Enumeration
    {
        public static readonly SecurityUserGroups Empty = new SecurityUserGroups { IsActive = false, Key = "" };
        public static readonly SecurityUserGroups Administrator = new SecurityUserGroups { IsActive = true, Key = "Administrator" };
        public static readonly SecurityUserGroups Trainer = new SecurityUserGroups { IsActive = true, Key = "Trainer" };
    }


}