using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Core
{
    public class CoreLocalizationKeys: StringToken
    {
        protected CoreLocalizationKeys(string key) : this(key, null)
        {
        }

        protected CoreLocalizationKeys(string key, string default_EN_US_Text)
            : base(key, default_EN_US_Text)
        {
        }

        public static readonly StringToken FIELD_REQUIRED = new CoreLocalizationKeys("FIELD_REQUIRED", "{0} Field is Required");
        public static readonly StringToken CONFIRMATION_PASSWORD_MUST_MATCH = new CoreLocalizationKeys("CONFIRMATION_PASSWORD_MUST_MATCH", "Confirmation password must match");
        public static readonly StringToken VALID_EMAIL_FORMAT = new CoreLocalizationKeys("VALID_EMAIL_FORMAT", "{0} Must be a valid Email Address");
        public static readonly StringToken VALID_DATE_FORMAT = new CoreLocalizationKeys("VALID_DATE_FORMAT", "{0} Must be a valid Date");
        public static readonly StringToken VALID_RANGE = new CoreLocalizationKeys("VALID_RANGE", "{0} Must be between {1} and {2}");
        public static readonly StringToken FIELD_MUST_BE_NUMBER = new CoreLocalizationKeys("FIELD_MUST_BE_NUMBER", "{0} Field Requires a Number");
        public static readonly StringToken SITE_NAME = new CoreLocalizationKeys("SITE_NAME", "Know Your Turf");
        public static readonly StringToken SELECT_ITEM = new CoreLocalizationKeys("SELECT_ITEM", "-- Please Select --");
        public static readonly StringToken INVALID_USERASSISTANTITEM = new CoreLocalizationKeys("INVALID_USERASSISTANTITEM", "Some of these items have invalid information");
        public static readonly StringToken INSUFFICIENT_ITEM_INSTANCES = new CoreLocalizationKeys("INSUFFICIENT_ITEM_INSTANCES", "This item requires {0} instances, but only has {1} instances");
        public static readonly StringToken ACTOR_IS_NOT_THE_PERSON_BEING_TESTED = new CoreLocalizationKeys("ACTOR_IS_NOT_THE_PERSON_BEING_TESTED", "The Actor is not the person being tested.");
        public static readonly StringToken ACTOR_IS_THE_PERSON_BEING_TESTED = new CoreLocalizationKeys("ACTOR_IS_THE_PERSON_BEING_TESTED", "The Actor is the person being tested.");
        public static readonly StringToken THE_PERSON_BEING_TESTED_HAS_NOT_SUBMITTED_THIER_ANSWERS = new CoreLocalizationKeys("THE_PERSON_BEING_TESTED_HAS_NOT_SUBMITTED_THIER_ANSWERS", "The person being tested has not subitted thier answers.");
        public static readonly StringToken THE_PERSON_BEING_TESTED_HAS_SUBMITTED_THIER_ANSWERS = new CoreLocalizationKeys("THE_PERSON_BEING_TESTED_HAS_SUBMITTED_THIER_ANSWERS", "The person being tested has subitted thier answers.");
        public static readonly StringToken THE_CHECKLIST_HAS_NOT_BEEN_COMPLETED = new CoreLocalizationKeys("THE_CHECKLIST_HAS_NOT_BEEN_COMPLETED", "The Checklist has not been completed.");
        public static readonly StringToken THE_CHECKLIST_HAS_NOT_BEEN_FINIALIZED = new CoreLocalizationKeys("THE_CHECKLIST_HAS_NOT_BEEN_FINIALIZED", "The Checklist has not been finalized.");
        public static readonly StringToken THE_CHECKLIST_HAS_BEEN_FINIALIZED = new CoreLocalizationKeys("THE_CHECKLIST_HAS_BEEN_FINIALIZED", "The Checklist has been finalized.");
        public static readonly StringToken THE_CHECKLIST_HAS_BEEN_COMPLETED = new CoreLocalizationKeys("THE_CHECKLIST_HAS_BEEN_COMPLETED", "The Checklist has been completed.");
        public static readonly StringToken SUCCESSFUL_SAVE = new CoreLocalizationKeys("SUCCESSFUL_SAVE", "This operation has completed successfully");
        public static readonly StringToken DECISION_CRITICAL_EMAIL_CONFIRMATION = new CoreLocalizationKeys("DECISION_CRITICAL_EMAIL_CONFIRMATION", "Decision Critical Email Confirmation");
        public static readonly StringToken VALID_URL_FORMAT = new CoreLocalizationKeys("VALID_URL_FORMAT", "{0} Must be a valid Url");
        public static readonly StringToken INVALID_YEAR_MESSAGE = new CoreLocalizationKeys("INVALID_YEAR_MESSAGE", "Enter a Valid Year");
        public static readonly StringToken PRESENT = new CoreLocalizationKeys("PRESENT", "Present");
        public static readonly StringToken TRANSACTION_DECLINED = new CoreLocalizationKeys("TRANSACTION_DECLINED", "Transaction Declined");
        public static readonly StringToken TRANSACTION_SUCCESSFUL = new CoreLocalizationKeys("TRANSACTION_SUCCESSFUL", "Your transaction has been successfully Processed.  You will be recieving an Email at the address proviced shortly.  " +
                                                                                                                       "The email will contain a link that will direct you to your new account at Decision Critical!");
        public static readonly StringToken THANK_YOU_FOR_SUBSCRIBING = new CoreLocalizationKeys("THANK_YOU_FOR_SUBSCRIBING", "Thank you for joining Decision Critical!");


        public static readonly StringToken BUISNESS_RULE = new CoreLocalizationKeys("BUISNESS_RULE", "Business Rule");

    }
}