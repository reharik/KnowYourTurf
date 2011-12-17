using System;
using KnowYourTurf.Core.Localization;

namespace KnowYourTurf.Web
{
    public class WebLocalizationKeys: StringToken
    {
        protected WebLocalizationKeys(string key) : this(key, null)
        {
        }

        protected WebLocalizationKeys(string key, string default_EN_US_Text)
            : base(key, default_EN_US_Text)
        {
        }

        public static readonly StringToken EMPLOYEE_DASHBOARD = new WebLocalizationKeys("EMPLOYEE_DASHBOARD", "Employee Dashboard");
        public static readonly StringToken CALENDAR = new WebLocalizationKeys("CALENDAR", "Calendar");
        public static readonly StringToken TRAINERS = new WebLocalizationKeys("TRAINERS", "Trainers");
        public static readonly StringToken TRAINER = new WebLocalizationKeys("TRAINER", "Trainer");
        public static readonly StringToken CLIENTS = new WebLocalizationKeys("CLIENTS", "Clients");
        public static readonly StringToken CLIENT = new WebLocalizationKeys("CLIENT", "Client");
        public static readonly StringToken NEW_TRAINER = new WebLocalizationKeys("NEW_TRAINER", "New Trainer");
        public static readonly StringToken NEW_CLIENT = new WebLocalizationKeys("NEW_CLIENT", "New Client");
        public static readonly StringToken CLIENT_INFORMATION = new WebLocalizationKeys("CLIENT_INFORMATION", "Client Information");
        public static readonly StringToken TRAINER_INFORMATION = new WebLocalizationKeys("TRAINER_INFORMATION", "Trainer Information");






        public static readonly StringToken ADMIN_TOOLS = new WebLocalizationKeys("ADMIN_TOOLS", "Admin Tools");
        public static readonly StringToken REQUIRED = new WebLocalizationKeys("REQUIRED", "Required");
        public static readonly StringToken SIGN_IN = new WebLocalizationKeys("SIGN_IN", "Sign In");
        public static readonly StringToken LOG_OUT = new WebLocalizationKeys("LOG_OUT", "Logout");
        public static readonly StringToken WELCOME = new WebLocalizationKeys("WELCOME", "Welcome ,");
        public static readonly StringToken USER_ROLES = new WebLocalizationKeys("USER_ROLES", "UserRoles");
        public static readonly StringToken ACCOUNT_LOGIN = new WebLocalizationKeys("ACCOUNT_LOGIN", "Account LogIn");
        public static readonly StringToken INVALID_USERNAME_OR_PASSWORD = new WebLocalizationKeys("INVALID_USERNAME_OR_PASSWORD", "Invalid Username or Password");
        public static readonly StringToken PLEASE_ENTER_YOUR_USERNAME_AND_PASSWORD_BELOW_KEY = new WebLocalizationKeys("PLEASE_ENTER_YOUR_USERNAME_AND_PASSWORD_BELOW_KEY", "Please enter your Username and Password below");
        public static readonly StringToken ENTER_YOUR_EMAIL_ADDRESS_AND_WE_WILL_SEND_YOU_YOUR_INFORMATION = new WebLocalizationKeys("ENTER_YOUR_EMAIL_ADDRESS_AND_WE_WILL_SEND_YOU_YOUR_INFORMATION", "Enter your email address and we will send you your information");
        public static readonly StringToken UNABLE_TO_FIND_EMAIL_ADDRESS = new WebLocalizationKeys("UNABLE_TO_FIND_EMAIL_ADDRESS", "Unable to find the email address you entered");
        public static readonly StringToken YOUR_PASSWORD_ADDRESS_HAS_BEEN_SENT_TO_YOU = new WebLocalizationKeys("YOUR_PASSWORD_ADDRESS_HAS_BEEN_SENT_TO_YOU", "Your password has been emailed to you.");
        public static readonly StringToken FORGOT_YOUR_PASSWORD = new WebLocalizationKeys("FORGOT_YOUR_PASSWORD", "Forgot your password?");
        public static readonly StringToken FORGOT_YOUR_PASSWORD_POPUP_TITLE = new WebLocalizationKeys("FORGOT_YOUR_PASSWORD_POPUP_TITLE", "Forgotten Password");
        public static readonly StringToken MY_ACCOUNT= new WebLocalizationKeys("USER_PROFILE", "My Account");
        public static readonly StringToken PROFILE = new WebLocalizationKeys("PROFILE", "Profile");
        public static readonly StringToken HELP = new WebLocalizationKeys("HELP", "Help");

        public static readonly StringToken DASHBOARD = new WebLocalizationKeys("DASHBOARD", "Dashboard");

        public static readonly StringToken FIELD_REQUIRED = new WebLocalizationKeys("FIELD_REQUIRED", "{0} Field is Required");
        public static readonly StringToken USER_COMPLIANCE_ITEM_NOT_IN_USER_COMPLIANCE_SET = new WebLocalizationKeys("USER_COMPLIANCE_ITEM_NOT_IN_USER_COMPLIANCE_SET", "User Assistant Item is not in the User Assistant Set");
        public static readonly StringToken DELETE_ITEM = new WebLocalizationKeys("DELETE_ITEM", "Delete this item!");
        public static readonly StringToken EDIT_ITEM = new WebLocalizationKeys("EDIT_ITEM", "Edit this item!");
        public static readonly StringToken DISPLAY_ITEM = new WebLocalizationKeys("DISPLAY_ITEM", "Display this item!");
        public static readonly StringToken USER_HAS_DATA = new WebLocalizationKeys("USER_HAS_DATA", "User Has Data");
        public static readonly StringToken TASK_NAME = new WebLocalizationKeys("TASK_NAME", "Task Name");
        public static readonly StringToken NAME = new WebLocalizationKeys("NAME", "Name");
        public static readonly StringToken ADD_NEW_ITEM = new WebLocalizationKeys("ADD_NEW_ITEM", "Add New Item");
        public static readonly StringToken UPLOAD_NEW_DOCUMENT = new WebLocalizationKeys("UPLOAD_NEW_DOCUMENT", "Upload New Document");
        public static readonly StringToken SELECT_UPLOAD_FILE = new WebLocalizationKeys("SELECT_UPLOAD_FILE", "Select File to Upload");
        public static readonly StringToken DELETE_PHOTO = new WebLocalizationKeys("DELETE_PHOTO", "Delete Photo");

        public static readonly StringToken REMOVE_DOCUMENT = new WebLocalizationKeys("REMOVE_DOCUMENT", "(X) Remove Document");
        public static readonly StringToken DOCUMENTS = new WebLocalizationKeys("DOCUMENTS", "Documents");
        public static readonly StringToken FILES = new WebLocalizationKeys("FILES", "Files");
        public static readonly StringToken FILE = new WebLocalizationKeys("FILE", "File");
        public static readonly StringToken FILE_CATEGORY = new WebLocalizationKeys("FILE_CATEGORY", "File Category");
        public static readonly StringToken DOCUMENT = new WebLocalizationKeys("DOCUMENT", "Document");
        public static readonly StringToken PHOTO = new WebLocalizationKeys("PHOTO", "Photo");
        public static readonly StringToken PHOTOS = new WebLocalizationKeys("PHOTOS", "Photos");
        public static readonly StringToken SELECT_EXISTING_DOCUMENT = new WebLocalizationKeys("SELECT_EXISTING_DOCUMENT", "Select Existing Document");
        public static readonly StringToken UPLOAD_AND_SELECT = new WebLocalizationKeys("UPLOAD_AND_SELECT", "Upload and Select");
        public static readonly StringToken SELECT_ITEMS = new WebLocalizationKeys("SELECT_ITEMS", "Select checked items");
        public static readonly StringToken REMOVE_ITEMS = new WebLocalizationKeys("REMOVE_ITEMS", "Remove checked items");
        public static readonly StringToken REMOVE_ITEM = new WebLocalizationKeys("REMOVE_ITEM", "(X)Remove");
        public static readonly StringToken CATEGORY = new WebLocalizationKeys("CATEGORY", "Category");
        public static readonly StringToken FILETYPE = new WebLocalizationKeys("FILETYPE", "File Type");
        public static readonly StringToken SEARCH = new WebLocalizationKeys("SEARCH", "Search");
        public static readonly StringToken CLEAR = new WebLocalizationKeys("CLEAR", "Clear");
        public static readonly StringToken STATE = new WebLocalizationKeys("STATE", "State");
        public static readonly StringToken CANCEL = new WebLocalizationKeys("CANCEL", "Cancel");
        public static readonly StringToken RETURN = new WebLocalizationKeys("RETURN", "Return");
        public static readonly StringToken ADD = new WebLocalizationKeys("ADD", "Add");

        public static readonly StringToken LAST_NAME = new WebLocalizationKeys("LAST_NAME", "Last Name");
        public static readonly StringToken FIRST_NAME = new WebLocalizationKeys("FIRST_NAME", "First Name");
        public static readonly StringToken REQUIRED_BY_SYSTEM = new WebLocalizationKeys("REQUIRED_BY_SYSTEM", "Is Required By the System");

        public static readonly StringToken YOU_HAVE_NOT_ADDED_ANY = new WebLocalizationKeys("YOU_HAVE_NOT_ADDED_ANY", "You have not added any {0} ");
        public static readonly StringToken SELECT_ONE_OR_MORE_OR = new WebLocalizationKeys("SELECT_ONE_OR_MORE", "Select one or more or ...");
        public static readonly StringToken ADD_A_NEW = new WebLocalizationKeys("ADD_A_NEW", "Add a new {0}");
        public static readonly StringToken ADD_A_NEW_ONE = new WebLocalizationKeys("ADD_A_NEW_ONE", "add a new one");

        public static readonly StringToken RESET_PASSWORD = new WebLocalizationKeys("RESET_PASSWORD", "Reset Password");
        public static readonly StringToken NEW_PASSWORD = new WebLocalizationKeys("NEW_PASSWORD", "New Password");
        public static readonly StringToken CONFIRM_NEW_PASSWORD = new WebLocalizationKeys("CONFIRM_NEW_PASSWORD", "Confirm New Password");
        public static readonly StringToken LOGIN_NAME = new WebLocalizationKeys("LOGIN_NAME", "Login Name");
        public static readonly StringToken PLEASE_SELECT_A_PASSWORD = new WebLocalizationKeys("PLEASE_SELECT_A_PASSWORD", "Hello {0}, please select a new password");

        public static readonly StringToken SELECT_COLOR_TO_ASSOCIATE_WITH_THIS_PERSON = new WebLocalizationKeys("SELECT_COLOR_TO_ASSOCIATE_WITH_THIS_PERSON", "Select a color to Associate with this Person");
        public static readonly StringToken ERROR_UNEXPECTED = new WebLocalizationKeys("ERROR_UNEXPECTED", "Unexpected Error");
        public static readonly StringToken APPOINTMENT_INFORMATION = new WebLocalizationKeys("APPOINTMENT_INFORMATION", "Appointment Information");
        public static readonly StringToken REMOVE = new WebLocalizationKeys("REMOVE", "Remove");
        public static readonly StringToken NEW = new WebLocalizationKeys("NEW", "New");
        public static readonly StringToken PERSONAL_INFORMATION = new WebLocalizationKeys("PERSONAL_INFORMATION", "Personal Information");
        public static readonly StringToken INITIAL = new WebLocalizationKeys("INITIAL", "Initial");
        public static readonly StringToken PASSWORD = new WebLocalizationKeys("PASSWORD", "Password");
        public static readonly StringToken SAVE = new WebLocalizationKeys("SAVE", "Save");

        public static readonly StringToken HOUR = new WebLocalizationKeys("HOUR", "Hour");
        public static readonly StringToken MINUTES = new WebLocalizationKeys("MINUTES", "Minutes");
        public static readonly StringToken AMPM = new WebLocalizationKeys("AMPM", "AM/PM");

        public static readonly StringToken STARTTIME= new WebLocalizationKeys("STARTTIME", "Start Time");
        public static readonly StringToken ENDTIME = new WebLocalizationKeys("ENDTIME", "End Time");


    }
}