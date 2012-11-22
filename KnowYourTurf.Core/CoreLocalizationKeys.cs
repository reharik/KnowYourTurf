using CC.Core.Localization;

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
        public static readonly StringToken FIELD_MUST_BE_NUMBER = new CoreLocalizationKeys("FIELD_MUST_BE_NUMBER", "{0} Field Requires a Number");
        public static readonly StringToken SITE_NAME = new CoreLocalizationKeys("SITE_NAME", "Know Your Turf");
        public static readonly StringToken SELECT_ITEM = new CoreLocalizationKeys("SELECT_ITEM", "-- Please Select --");

        public static readonly StringToken EMPLOYEE_HAS_TASKS_IN_FUTURE = new CoreLocalizationKeys("EMPLOYEE_HAS_TASKS_IN_FUTURE", "Employee Has {0} Task(s) Scheduled in the Future");
        public static readonly StringToken DELETE_EMPLOYEE = new CoreLocalizationKeys("DELETE_EMPLOYEE", "Delete Employee");

        public static readonly StringToken FIELD_HAS_TASKS_IN_FUTURE = new CoreLocalizationKeys("FIELD_HAS_TASKS_IN_FUTURE", "Field Has {0} Task(s) Scheduled in the Future");
        public static readonly StringToken FIELD_HAS_EVENTS_IN_FUTURE = new CoreLocalizationKeys("FIELD_HAS_EVENTS_IN_FUTURE", "Field Has {0} Event(s) Scheduled in the Future");
        public static readonly StringToken DELETE_FIELD = new CoreLocalizationKeys("DELETE_FIELD", "Delete Field");
        public static readonly StringToken QUANTITY_USED_REQUIRED = new CoreLocalizationKeys("QUANTITY_USED_REQUIRED", "Quantity Used is a required Field when completeing a task");
        public static readonly StringToken SUCCESSFUL_SAVE = new CoreLocalizationKeys("SUCCESSFUL_SAVE", "This operation has completed successfully");

        public static readonly StringToken KNOWYOURTURF_DAILY_TASKS = new CoreLocalizationKeys("KNOWYOURTURF_DAILY_TASKS", "Know your turf Daily Tasks");
        public static readonly StringToken DAILY_TASK_SUMMARY = new CoreLocalizationKeys("DAILY_TASK_SUMMARY", "Daily Task summary");
        public static readonly StringToken EQUIPMENT_MAINTENANCE_NOTIFICATION = new CoreLocalizationKeys("EQUIPMENT_MAINTENANCE_NOTIFICATION", "Equipment Maintenance Notification");
        public static readonly StringToken BUISNESS_RULE = new CoreLocalizationKeys("BUISNESS_RULE", "Business Rule");
        
    }
}