using FluentMigrator;

namespace DBFluentMigration.Iteration_0
{
    [Migration(1)]
    public class CreateInitialDB : Migration
    {
        public override void Up()
        {
            //For EmailJobType
            Create.Table("EmailJobType").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("Status").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For EmailTemplate
            Create.Table("EmailTemplate").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Template").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For Chemical
            Create.Table("Chemical").InSchema("dbo")
                .WithColumn("ActiveIngredient").AsString().Nullable()
                .WithColumn("ActiveIngredientPercent").AsDecimal().Nullable()
                .WithColumn("EPAEstNumber").AsString().Nullable()
                .WithColumn("EPARegNumber").AsString().Nullable()
                .WithColumn("BaseProduct_id").AsInt32().PrimaryKey().NotNullable();

            //For Fertilizer
            Create.Table("Fertilizer").InSchema("dbo")
                .WithColumn("N").AsDouble().Nullable()
                .WithColumn("P").AsDouble().Nullable()
                .WithColumn("K").AsDouble().Nullable()
                .WithColumn("BaseProduct_id").AsInt32().PrimaryKey().NotNullable();

            //For Equipment
            Create.Table("Equipment").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("TotalHours").AsDouble().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("FileUrl").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable()
                .WithColumn("Vendor_id").AsInt32().Nullable();

            //For EquipmentToTask
            Create.Table("EquipmentToTask").InSchema("dbo")
                .WithColumn("Task_id").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("Equipment_id").AsInt32().PrimaryKey().NotNullable();

            //For Material
            Create.Table("Material").InSchema("dbo")
                .WithColumn("BaseProduct_id").AsInt32().PrimaryKey().NotNullable();

            //For Event
            Create.Table("Event").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ScheduledDate").AsDateTime().Nullable()
                .WithColumn("StartTime").AsDateTime().Nullable()
                .WithColumn("EndTime").AsDateTime().Nullable()
                .WithColumn("Notes").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("EventType_id").AsInt32().Nullable()
                .WithColumn("Field_id").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable()
                .WithColumn("Category_id").AsInt32().Nullable();

            //For EventType
            Create.Table("EventType").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("Status").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EventColor").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For security_UsersToUsersGroups
            Create.Table("security_UsersToUsersGroups").InSchema("dbo")
                .WithColumn("GroupId").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("UserId").AsInt32().PrimaryKey().NotNullable();

            //For UserLoginInfo
            Create.Table("UserLoginInfo").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("LoginName").AsString().Nullable()
                .WithColumn("Password").AsString().Nullable()
                .WithColumn("Status").AsString().Nullable()
                .WithColumn("ByPassToken").AsGuid().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For Field
            Create.Table("Field").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("Size").AsInt32().Nullable()
                .WithColumn("FileUrl").AsString().Nullable()
                .WithColumn("Status").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Abbreviation").AsString().Nullable()
                .WithColumn("FieldColor").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable()
                .WithColumn("Category_id").AsInt32().Nullable();

            //For security_UsersGroupsHierarchy
            Create.Table("security_UsersGroupsHierarchy").InSchema("dbo")
                .WithColumn("ParentGroup").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("ChildGroup").AsInt32().PrimaryKey().NotNullable();

            //For UserRole
            Create.Table("UserRole").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For Vendor
            Create.Table("Vendor").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Company").AsString().Nullable()
                .WithColumn("Address1").AsString().Nullable()
                .WithColumn("Address2").AsString().Nullable()
                .WithColumn("City").AsString().Nullable()
                .WithColumn("State").AsString().Nullable()
                .WithColumn("ZipCode").AsString().Nullable()
                .WithColumn("Country").AsString().Nullable()
                .WithColumn("Fax").AsString().Nullable()
                .WithColumn("LogoUrl").AsString().Nullable()
                .WithColumn("Notes").AsString().Nullable()
                .WithColumn("Phone").AsString().Nullable()
                .WithColumn("Status").AsString().Nullable()
                .WithColumn("Website").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For BaseProductToVendor
            Create.Table("BaseProductToVendor").InSchema("dbo")
                .WithColumn("Vendor_id").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("BaseProduct_id").AsInt32().PrimaryKey().NotNullable();

            //For UserRoleToUser
            Create.Table("UserRoleToUser").InSchema("dbo")
                .WithColumn("User_id").AsInt32().NotNullable()
                .WithColumn("UserRole_id").AsInt32().NotNullable();

            //For InventoryProduct
            Create.Table("InventoryProduct").InSchema("dbo")
                .WithColumn("Discriminator").AsString().NotNullable()
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Quantity").AsDouble().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("DatePurchased").AsDateTime().Nullable()
                .WithColumn("LastUsed").AsDateTime().Nullable()
                .WithColumn("Cost").AsDouble().Nullable()
                .WithColumn("UnitType").AsString().Nullable()
                .WithColumn("SizeOfUnit").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().NotNullable()
                .WithColumn("LastVendor_id").AsInt32().NotNullable()
                .WithColumn("Product_id").AsInt32().NotNullable()
                .WithColumn("CreatedBy_Id").AsInt32().NotNullable()
                .WithColumn("ChangedBy_Id").AsInt32().NotNullable();

            //For LocalizedEnumeration
            Create.Table("LocalizedEnumeration").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Culture").AsString().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("ValueType").AsString().Nullable()
                .WithColumn("Text").AsString().Nullable()
                .WithColumn("Tooltip").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For VendorContact
            Create.Table("VendorContact").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("FirstName").AsString().Nullable()
                .WithColumn("LastName").AsString().Nullable()
                .WithColumn("Address1").AsString().Nullable()
                .WithColumn("Address2").AsString().Nullable()
                .WithColumn("City").AsString().Nullable()
                .WithColumn("State").AsString().Nullable()
                .WithColumn("ZipCode").AsString().Nullable()
                .WithColumn("Country").AsString().Nullable()
                .WithColumn("Email").AsString().Nullable()
                .WithColumn("Fax").AsString().Nullable()
                .WithColumn("Status").AsString().Nullable()
                .WithColumn("Phone").AsString().Nullable()
                .WithColumn("Notes").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("Vendor_id").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For LocalizedProperty
            Create.Table("LocalizedProperty").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Culture").AsString().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("ParentType").AsString().Nullable()
                .WithColumn("Text").AsString().Nullable()
                .WithColumn("Tooltip").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For Weather
            Create.Table("Weather").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("HighTemperature").AsDouble().Nullable()
                .WithColumn("LowTemperature").AsDouble().Nullable()
                .WithColumn("WindSpeed").AsDouble().Nullable()
                .WithColumn("RainPrecipitation").AsDouble().Nullable()
                .WithColumn("Humidity").AsDouble().Nullable()
                .WithColumn("EvaporationRate").AsDouble().Nullable()
                .WithColumn("DewPoint").AsDouble().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For LocalizedText
            Create.Table("LocalizedText").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Culture").AsString().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Text").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For Photo
            Create.Table("Photo").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("FileUrl").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().NotNullable()
                .WithColumn("Field_id").AsInt32().NotNullable()
                .WithColumn("CreatedBy_Id").AsInt32().NotNullable()
                .WithColumn("ChangedBy_Id").AsInt32().NotNullable()
                .WithColumn("PhotoCategory_Id").AsInt32().Nullable();

            //For security_Operations
            Create.Table("security_Operations").InSchema("dbo")
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Comment").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("ParentId").AsInt32().NotNullable();

            Create.Index("UQ__security__737584F60FAD2F12").OnTable("security_Operations").InSchema("dbo")
                .OnColumn("EntityId").Ascending()
                .OnColumn("Name").Ascending()
                .WithOptions().Unique();

            Create.Index("UQ__security__737584F659FA5E80").OnTable("security_Operations").InSchema("dbo")
                .OnColumn("EntityId").Ascending()
                .OnColumn("Name").Ascending()
                .WithOptions().Unique();

            //For PhotoCategory
            Create.Table("PhotoCategory").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("Status").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().NotNullable()
                .WithColumn("CreatedBy_Id").AsInt32().NotNullable()
                .WithColumn("ChangedBy_Id").AsInt32().NotNullable();

            //For EmailTemplatesToSubscribers
            Create.Table("EmailTemplatesToSubscribers").InSchema("dbo")
                .WithColumn("EmailJob_id").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("User_id").AsInt32().PrimaryKey().NotNullable();

            //For security_Permissions
            Create.Table("security_Permissions").InSchema("dbo")
                .WithColumn("Allow").AsBoolean().NotNullable()
                .WithColumn("Level").AsInt32().NotNullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("OperationId").AsInt32().NotNullable()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("UsersGroupId").AsInt32().NotNullable();

            //For PurchaseOrder
            Create.Table("PurchaseOrder").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("SubTotal").AsDouble().Nullable()
                .WithColumn("Completed").AsBoolean().Nullable()
                .WithColumn("Fees").AsDouble().Nullable()
                .WithColumn("Tax").AsDouble().Nullable()
                .WithColumn("Total").AsDouble().Nullable()
                .WithColumn("DateReceived").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("Vendor_id").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For security_UsersGroups
            Create.Table("security_UsersGroups").InSchema("dbo")
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("ParentId").AsInt32().NotNullable();

            Create.Index("UQ__security__737584F6123EB7A3").OnTable("security_UsersGroups").InSchema("dbo")
                .OnColumn("EntityId").Ascending()
                .OnColumn("Name").Ascending()
                .WithOptions().Unique();

            Create.Index("UQ__security__737584F61A2ABD85").OnTable("security_UsersGroups").InSchema("dbo")
                .OnColumn("EntityId").Ascending()
                .OnColumn("Name").Ascending()
                .WithOptions().Unique();

            //For EmployeeToTask
            Create.Table("EmployeeToTask").InSchema("dbo")
                .WithColumn("Task_id").AsInt32().NotNullable()
                .WithColumn("User_id").AsInt32().NotNullable();

            //For PurchaseOrderLineItem
            Create.Table("PurchaseOrderLineItem").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Price").AsDouble().Nullable()
                .WithColumn("QuantityOrdered").AsInt32().Nullable()
                .WithColumn("TotalReceived").AsInt32().Nullable()
                .WithColumn("SubTotal").AsDouble().Nullable()
                .WithColumn("Tax").AsDouble().Nullable()
                .WithColumn("Completed").AsBoolean().Nullable()
                .WithColumn("DateRecieved").AsDateTime().Nullable()
                .WithColumn("UnitType").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Taxable").AsBoolean().Nullable()
                .WithColumn("SizeOfUnit").AsInt32().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("Product_id").AsInt32().Nullable()
                .WithColumn("PurchaseOrder_id").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For Task
            Create.Table("Task").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("ScheduledDate").AsDateTime().Nullable()
                .WithColumn("ScheduledStartTime").AsDateTime().Nullable()
                .WithColumn("ScheduledEndTime").AsDateTime().Nullable()
                .WithColumn("ActualTimeSpent").AsString().Nullable()
                .WithColumn("Notes").AsString().Nullable()
                .WithColumn("Deleted").AsBoolean().Nullable()
                .WithColumn("Complete").AsBoolean().Nullable()
                .WithColumn("QuantityNeeded").AsDouble().Nullable()
                .WithColumn("QuantityUsed").AsDouble().Nullable()
                .WithColumn("UnitType").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("InventoryDecremented").AsBoolean().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("TaskType_id").AsInt32().Nullable()
                .WithColumn("Field_id").AsInt32().Nullable()
                .WithColumn("InventoryProduct_id").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable()
                .WithColumn("Category_id").AsInt32().Nullable();

            //For PhotoToField
            Create.Table("PhotoToField").InSchema("dbo")
                .WithColumn("Field_id").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("Photo_id").AsInt32().PrimaryKey().NotNullable();

            //For DocumentToField
            Create.Table("DocumentToField").InSchema("dbo")
                .WithColumn("Field_id").AsInt32().PrimaryKey().NotNullable()
                .WithColumn("Document_id").AsInt32().PrimaryKey().NotNullable();

            //For TaskType
            Create.Table("TaskType").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("Status").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For BaseProduct
            Create.Table("BaseProduct").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("InstantiatingType").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Notes").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For User
            Create.Table("User").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("EmployeeId").AsString().Nullable()
                .WithColumn("FirstName").AsString().Nullable()
                .WithColumn("LastName").AsString().Nullable()
                .WithColumn("Email").AsString().Nullable()
                .WithColumn("PhoneMobile").AsString().Nullable()
                .WithColumn("PhoneHome").AsString().Nullable()
                .WithColumn("Address1").AsString().Nullable()
                .WithColumn("Address2").AsString().Nullable()
                .WithColumn("City").AsString().Nullable()
                .WithColumn("State").AsString().Nullable()
                .WithColumn("ZipCode").AsString().Nullable()
                .WithColumn("BirthDate").AsDateTime().Nullable()
                .WithColumn("Notes").AsString().Nullable()
                .WithColumn("LanguageDefault").AsString().Nullable()
                .WithColumn("FileUrl").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EmergencyContact").AsString().Nullable()
                .WithColumn("EmergencyContactPhone").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("Company_id").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable()
                .WithColumn("UserLoginInfo_id").AsInt32().Nullable();

            //For Calculator
            Create.Table("Calculator").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().NotNullable()
                .WithColumn("CreatedBy_Id").AsInt32().NotNullable()
                .WithColumn("ChangedBy_Id").AsInt32().NotNullable();

            //For Category
            Create.Table("Category").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable()
                .WithColumn("Company_id").AsInt32().Nullable();

            //For Company
            Create.Table("Company").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("ZipCode").AsString().Nullable()
                .WithColumn("TaxRate").AsDouble().Nullable()
                .WithColumn("NumberOfCategories").AsInt32().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For Document
            Create.Table("Document").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("FileUrl").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("DocumentCategory_id").AsInt32().Nullable()
                .WithColumn("Field_id").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For DocumentCategory
            Create.Table("DocumentCategory").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("Status").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable();

            //For EmailJob
            Create.Table("EmailJob").InSchema("dbo")
                .WithColumn("ChangedDate").AsDateTime().Nullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("IsDeleted").AsBoolean().Nullable()
                .WithColumn("Name").AsString().Nullable()
                .WithColumn("Subject").AsString().Nullable()
                .WithColumn("Sender").AsString().Nullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("Frequency").AsString().Nullable()
                .WithColumn("Status").AsString().Nullable()
                .WithColumn("EntityId").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("CompanyId").AsInt32().Nullable()
                .WithColumn("EmailTemplate_id").AsInt32().Nullable()
                .WithColumn("CreatedBy_Id").AsInt32().Nullable()
                .WithColumn("ChangedBy_Id").AsInt32().Nullable()
                .WithColumn("EmailJobType_id").AsInt32().Nullable();


            //Foreign Key List 
            Create.ForeignKey("FK546B820F8C596B01").FromTable("EmailJobType").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK546B820F20B5AF9B").FromTable("EmailJobType").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9E50581C8C596B01").FromTable("EmailTemplate").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9E50581C20B5AF9B").FromTable("EmailTemplate").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK27BDC5F6FC68D351").FromTable("Chemical").InSchema("dbo").ForeignColumns("BaseProduct_id").ToTable("BaseProduct").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKCAAF5E64FC68D351").FromTable("Fertilizer").InSchema("dbo").ForeignColumns("BaseProduct_id").ToTable("BaseProduct").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKDEB29F8A8C596B01").FromTable("Equipment").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKDEB29F8A20B5AF9B").FromTable("Equipment").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKDEB29F8A768BE715").FromTable("Equipment").InSchema("dbo").ForeignColumns("Vendor_id").ToTable("Vendor").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK5B0F8632DD09CEF5").FromTable("EquipmentToTask").InSchema("dbo").ForeignColumns("Equipment_id").ToTable("Equipment").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK5B0F86329836496F").FromTable("EquipmentToTask").InSchema("dbo").ForeignColumns("Task_id").ToTable("Task").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKB6D28E23FC68D351").FromTable("Material").InSchema("dbo").ForeignColumns("BaseProduct_id").ToTable("BaseProduct").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK27CB76028C596B01").FromTable("Event").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK27CB760220B5AF9B").FromTable("Event").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK27CB760232D346DD").FromTable("Event").InSchema("dbo").ForeignColumns("EventType_id").ToTable("EventType").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK27CB76027AF94981").FromTable("Event").InSchema("dbo").ForeignColumns("Field_id").ToTable("Field").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKB032D96A8C596B01").FromTable("EventType").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKB032D96A20B5AF9B").FromTable("EventType").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK7654C67EDBB3D07F").FromTable("security_UsersToUsersGroups").InSchema("dbo").ForeignColumns("UserId").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK7654C67E8EB1A08D").FromTable("security_UsersToUsersGroups").InSchema("dbo").ForeignColumns("GroupId").ToTable("security_UsersGroups").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKA685463220B5AF9B").FromTable("UserLoginInfo").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKA68546328C596B01").FromTable("UserLoginInfo").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9126EE228C596B01").FromTable("Field").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9126EE2220B5AF9B").FromTable("Field").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9126EE22A46AFF29").FromTable("Field").InSchema("dbo").ForeignColumns("Category_id").ToTable("Category").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKCD82CCE25BCBBC14").FromTable("security_UsersGroupsHierarchy").InSchema("dbo").ForeignColumns("ChildGroup").ToTable("security_UsersGroups").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKCD82CCE2919F22D6").FromTable("security_UsersGroupsHierarchy").InSchema("dbo").ForeignColumns("ParentGroup").ToTable("security_UsersGroups").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK4F2578B120B5AF9B").FromTable("UserRole").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK4F2578B18C596B01").FromTable("UserRole").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK30D39A020B5AF9B").FromTable("Vendor").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK30D39A08C596B01").FromTable("Vendor").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK41C8A9C9768BE715").FromTable("BaseProductToVendor").InSchema("dbo").ForeignColumns("Vendor_id").ToTable("Vendor").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK41C8A9C9FC68D351").FromTable("BaseProductToVendor").InSchema("dbo").ForeignColumns("BaseProduct_id").ToTable("BaseProduct").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9CDDA1CF4210CF8F").FromTable("UserRoleToUser").InSchema("dbo").ForeignColumns("UserRole_id").ToTable("UserRole").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9CDDA1CF63E72CDB").FromTable("UserRoleToUser").InSchema("dbo").ForeignColumns("User_id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9E1B01A58C596B01").FromTable("InventoryProduct").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9E1B01A520B5AF9B").FromTable("InventoryProduct").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9E1B01A512D28416").FromTable("InventoryProduct").InSchema("dbo").ForeignColumns("Product_id").ToTable("BaseProduct").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9E1B01A528BE30D9").FromTable("InventoryProduct").InSchema("dbo").ForeignColumns("LastVendor_id").ToTable("Vendor").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK27E02DF68C596B01").FromTable("LocalizedEnumeration").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK27E02DF620B5AF9B").FromTable("LocalizedEnumeration").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK6A8399B0768BE715").FromTable("VendorContact").InSchema("dbo").ForeignColumns("Vendor_id").ToTable("Vendor").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK6A8399B020B5AF9B").FromTable("VendorContact").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK6A8399B08C596B01").FromTable("VendorContact").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK57B6865E8C596B01").FromTable("LocalizedProperty").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK57B6865E20B5AF9B").FromTable("LocalizedProperty").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK5269293E20B5AF9B").FromTable("Weather").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK5269293E8C596B01").FromTable("Weather").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK364F45208C596B01").FromTable("LocalizedText").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK364F452020B5AF9B").FromTable("LocalizedText").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK3BB7C328C596B01").FromTable("Photo").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK3BB7C3220B5AF9B").FromTable("Photo").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK_Photo_PhotoCategory").FromTable("Photo").InSchema("dbo").ForeignColumns("PhotoCategory_Id").ToTable("PhotoCategory").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK77852F468851360").FromTable("security_Operations").InSchema("dbo").ForeignColumns("ParentId").ToTable("security_Operations").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKED86B64E8C596B01").FromTable("PhotoCategory").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKED86B64E20B5AF9B").FromTable("PhotoCategory").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK5890D2DD532CB363").FromTable("EmailTemplatesToSubscribers").InSchema("dbo").ForeignColumns("EmailJob_id").ToTable("EmailJob").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK5890D2DD63E72CDB").FromTable("EmailTemplatesToSubscribers").InSchema("dbo").ForeignColumns("User_id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKAE00533EDBB3D07F").FromTable("security_Permissions").InSchema("dbo").ForeignColumns("UserId").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKAE00533E2957FE83").FromTable("security_Permissions").InSchema("dbo").ForeignColumns("OperationId").ToTable("security_Operations").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKAE00533E5DD228E6").FromTable("security_Permissions").InSchema("dbo").ForeignColumns("UsersGroupId").ToTable("security_UsersGroups").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK8BCCD4A5768BE715").FromTable("PurchaseOrder").InSchema("dbo").ForeignColumns("Vendor_id").ToTable("Vendor").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK8BCCD4A58C596B01").FromTable("PurchaseOrder").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK8BCCD4A520B5AF9B").FromTable("PurchaseOrder").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK354957DEFCFA86A8").FromTable("security_UsersGroups").InSchema("dbo").ForeignColumns("ParentId").ToTable("security_UsersGroups").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK1B8C947A63E72CDB").FromTable("EmployeeToTask").InSchema("dbo").ForeignColumns("User_id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK1B8C947A9836496F").FromTable("EmployeeToTask").InSchema("dbo").ForeignColumns("Task_id").ToTable("Task").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK89D81FB635D27A53").FromTable("PurchaseOrderLineItem").InSchema("dbo").ForeignColumns("PurchaseOrder_id").ToTable("PurchaseOrder").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK89D81FB612D28416").FromTable("PurchaseOrderLineItem").InSchema("dbo").ForeignColumns("Product_id").ToTable("BaseProduct").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK89D81FB620B5AF9B").FromTable("PurchaseOrderLineItem").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK89D81FB68C596B01").FromTable("PurchaseOrderLineItem").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKA59D71ED7AF94981").FromTable("Task").InSchema("dbo").ForeignColumns("Field_id").ToTable("Field").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKA59D71EDAA87C6F").FromTable("Task").InSchema("dbo").ForeignColumns("InventoryProduct_id").ToTable("InventoryProduct").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKA59D71EDDF108C47").FromTable("Task").InSchema("dbo").ForeignColumns("TaskType_id").ToTable("TaskType").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKA59D71ED8C596B01").FromTable("Task").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKA59D71ED20B5AF9B").FromTable("Task").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK1CB8A1D17AF94981").FromTable("PhotoToField").InSchema("dbo").ForeignColumns("Field_id").ToTable("Field").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK1CB8A1D163771C93").FromTable("PhotoToField").InSchema("dbo").ForeignColumns("Photo_id").ToTable("Photo").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK3149B5067AF94981").FromTable("DocumentToField").InSchema("dbo").ForeignColumns("Field_id").ToTable("Field").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK3149B50631645407").FromTable("DocumentToField").InSchema("dbo").ForeignColumns("Document_id").ToTable("Document").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK786C40858C596B01").FromTable("TaskType").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK786C408520B5AF9B").FromTable("TaskType").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKAE1B102E8C596B01").FromTable("BaseProduct").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKAE1B102E20B5AF9B").FromTable("BaseProduct").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK564EC989F10E77C5").FromTable("User").InSchema("dbo").ForeignColumns("UserLoginInfo_id").ToTable("UserLoginInfo").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK564EC98920B5AF9B").FromTable("User").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK564EC9898C596B01").FromTable("User").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK564EC98974070A17").FromTable("User").InSchema("dbo").ForeignColumns("Company_id").ToTable("Company").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK5DB88128C596B01").FromTable("Calculator").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK5DB881220B5AF9B").FromTable("Calculator").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKFA88A7FA8C596B01").FromTable("Category").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKFA88A7FA20B5AF9B").FromTable("Category").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKFA88A7FA74070A17").FromTable("Category").InSchema("dbo").ForeignColumns("Company_id").ToTable("Company").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK2646DB978C596B01").FromTable("Company").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK2646DB9720B5AF9B").FromTable("Company").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK57D3D6478C596B01").FromTable("Document").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK57D3D64720B5AF9B").FromTable("Document").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK57D3D6473C3C4C2B").FromTable("Document").InSchema("dbo").ForeignColumns("DocumentCategory_id").ToTable("DocumentCategory").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9873FDAF8C596B01").FromTable("DocumentCategory").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FK9873FDAF20B5AF9B").FromTable("DocumentCategory").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKA5B4730F2E41685B").FromTable("EmailJob").InSchema("dbo").ForeignColumns("EmailJobType_id").ToTable("EmailJobType").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKA5B4730F871F84B1").FromTable("EmailJob").InSchema("dbo").ForeignColumns("EmailTemplate_id").ToTable("EmailTemplate").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKA5B4730F8C596B01").FromTable("EmailJob").InSchema("dbo").ForeignColumns("CreatedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");

            Create.ForeignKey("FKA5B4730F20B5AF9B").FromTable("EmailJob").InSchema("dbo").ForeignColumns("ChangedBy_Id").ToTable("User").InSchema("dbo").PrimaryColumns("EntityId");
        }

        public override void Down()
        {
            Delete.ForeignKey("FK546B820F8C596B01");
            Delete.ForeignKey("FK546B820F20B5AF9B");
            Delete.ForeignKey("FK9E50581C8C596B01");
            Delete.ForeignKey("FK9E50581C20B5AF9B");
            Delete.ForeignKey("FK27BDC5F6FC68D351");
            Delete.ForeignKey("FKCAAF5E64FC68D351");
            Delete.ForeignKey("FKDEB29F8A8C596B01");
            Delete.ForeignKey("FKDEB29F8A20B5AF9B");
            Delete.ForeignKey("FKDEB29F8A768BE715");
            Delete.ForeignKey("FK5B0F8632DD09CEF5");
            Delete.ForeignKey("FK5B0F86329836496F");
            Delete.ForeignKey("FKB6D28E23FC68D351");
            Delete.ForeignKey("FK27CB76028C596B01");
            Delete.ForeignKey("FK27CB760220B5AF9B");
            Delete.ForeignKey("FK27CB760232D346DD");
            Delete.ForeignKey("FK27CB76027AF94981");
            Delete.ForeignKey("FKB032D96A8C596B01");
            Delete.ForeignKey("FKB032D96A20B5AF9B");
            Delete.ForeignKey("FK7654C67EDBB3D07F");
            Delete.ForeignKey("FK7654C67E8EB1A08D");
            Delete.ForeignKey("FKA685463220B5AF9B");
            Delete.ForeignKey("FKA68546328C596B01");
            Delete.ForeignKey("FK9126EE228C596B01");
            Delete.ForeignKey("FK9126EE2220B5AF9B");
            Delete.ForeignKey("FK9126EE22A46AFF29");
            Delete.ForeignKey("FKCD82CCE25BCBBC14");
            Delete.ForeignKey("FKCD82CCE2919F22D6");
            Delete.ForeignKey("FK4F2578B120B5AF9B");
            Delete.ForeignKey("FK4F2578B18C596B01");
            Delete.ForeignKey("FK30D39A020B5AF9B");
            Delete.ForeignKey("FK30D39A08C596B01");
            Delete.ForeignKey("FK41C8A9C9768BE715");
            Delete.ForeignKey("FK41C8A9C9FC68D351");
            Delete.ForeignKey("FK9CDDA1CF4210CF8F");
            Delete.ForeignKey("FK9CDDA1CF63E72CDB");
            Delete.ForeignKey("FK9E1B01A58C596B01");
            Delete.ForeignKey("FK9E1B01A520B5AF9B");
            Delete.ForeignKey("FK9E1B01A512D28416");
            Delete.ForeignKey("FK9E1B01A528BE30D9");
            Delete.ForeignKey("FK27E02DF68C596B01");
            Delete.ForeignKey("FK27E02DF620B5AF9B");
            Delete.ForeignKey("FK6A8399B0768BE715");
            Delete.ForeignKey("FK6A8399B020B5AF9B");
            Delete.ForeignKey("FK6A8399B08C596B01");
            Delete.ForeignKey("FK57B6865E8C596B01");
            Delete.ForeignKey("FK57B6865E20B5AF9B");
            Delete.ForeignKey("FK5269293E20B5AF9B");
            Delete.ForeignKey("FK5269293E8C596B01");
            Delete.ForeignKey("FK364F45208C596B01");
            Delete.ForeignKey("FK364F452020B5AF9B");
            Delete.ForeignKey("FK3BB7C328C596B01");
            Delete.ForeignKey("FK3BB7C3220B5AF9B");
            Delete.ForeignKey("FK_Photo_PhotoCategory");
            Delete.ForeignKey("FK77852F468851360");
            Delete.ForeignKey("FKED86B64E8C596B01");
            Delete.ForeignKey("FKED86B64E20B5AF9B");
            Delete.ForeignKey("FK5890D2DD532CB363");
            Delete.ForeignKey("FK5890D2DD63E72CDB");
            Delete.ForeignKey("FKAE00533EDBB3D07F");
            Delete.ForeignKey("FKAE00533E2957FE83");
            Delete.ForeignKey("FKAE00533E5DD228E6");
            Delete.ForeignKey("FK8BCCD4A5768BE715");
            Delete.ForeignKey("FK8BCCD4A58C596B01");
            Delete.ForeignKey("FK8BCCD4A520B5AF9B");
            Delete.ForeignKey("FK354957DEFCFA86A8");
            Delete.ForeignKey("FK1B8C947A63E72CDB");
            Delete.ForeignKey("FK1B8C947A9836496F");
            Delete.ForeignKey("FK89D81FB635D27A53");
            Delete.ForeignKey("FK89D81FB612D28416");
            Delete.ForeignKey("FK89D81FB620B5AF9B");
            Delete.ForeignKey("FK89D81FB68C596B01");
            Delete.ForeignKey("FKA59D71ED7AF94981");
            Delete.ForeignKey("FKA59D71EDAA87C6F");
            Delete.ForeignKey("FKA59D71EDDF108C47");
            Delete.ForeignKey("FKA59D71ED8C596B01");
            Delete.ForeignKey("FKA59D71ED20B5AF9B");
            Delete.ForeignKey("FK1CB8A1D17AF94981");
            Delete.ForeignKey("FK1CB8A1D163771C93");
            Delete.ForeignKey("FK3149B5067AF94981");
            Delete.ForeignKey("FK3149B50631645407");
            Delete.ForeignKey("FK786C40858C596B01");
            Delete.ForeignKey("FK786C408520B5AF9B");
            Delete.ForeignKey("FKAE1B102E8C596B01");
            Delete.ForeignKey("FKAE1B102E20B5AF9B");
            Delete.ForeignKey("FK564EC989F10E77C5");
            Delete.ForeignKey("FK564EC98920B5AF9B");
            Delete.ForeignKey("FK564EC9898C596B01");
            Delete.ForeignKey("FK564EC98974070A17");
            Delete.ForeignKey("FK5DB88128C596B01");
            Delete.ForeignKey("FK5DB881220B5AF9B");
            Delete.ForeignKey("FKFA88A7FA8C596B01");
            Delete.ForeignKey("FKFA88A7FA20B5AF9B");
            Delete.ForeignKey("FKFA88A7FA74070A17");
            Delete.ForeignKey("FK2646DB978C596B01");
            Delete.ForeignKey("FK2646DB9720B5AF9B");
            Delete.ForeignKey("FK57D3D6478C596B01");
            Delete.ForeignKey("FK57D3D64720B5AF9B");
            Delete.ForeignKey("FK57D3D6473C3C4C2B");
            Delete.ForeignKey("FK9873FDAF8C596B01");
            Delete.ForeignKey("FK9873FDAF20B5AF9B");
            Delete.ForeignKey("FKA5B4730F2E41685B");
            Delete.ForeignKey("FKA5B4730F871F84B1");
            Delete.ForeignKey("FKA5B4730F8C596B01");
            Delete.ForeignKey("FKA5B4730F20B5AF9B");

            Delete.Index("UQ__security__737584F60FAD2F12");
            Delete.Index("UQ__security__737584F659FA5E80");
            Delete.Index("UQ__security__737584F6123EB7A3");
            Delete.Index("UQ__security__737584F61A2ABD85");

            Delete.Table("EmailJob");
            Delete.Table("DocumentCategory");
            Delete.Table("Document");
            Delete.Table("Company");
            Delete.Table("Category");
            Delete.Table("Calculator");
            Delete.Table("User");
            Delete.Table("BaseProduct");
            Delete.Table("TaskType");
            Delete.Table("DocumentToField");
            Delete.Table("PhotoToField");
            Delete.Table("Task");
            Delete.Table("PurchaseOrderLineItem");
            Delete.Table("EmployeeToTask");
            Delete.Table("security_UsersGroups");
            Delete.Table("PurchaseOrder");
            Delete.Table("security_Permissions");
            Delete.Table("EmailTemplatesToSubscribers");
            Delete.Table("PhotoCategory");
            Delete.Table("security_Operations");
            Delete.Table("Photo");
            Delete.Table("LocalizedText");
            Delete.Table("Weather");
            Delete.Table("LocalizedProperty");
            Delete.Table("VendorContact");
            Delete.Table("LocalizedEnumeration");
            Delete.Table("InventoryProduct");
            Delete.Table("UserRoleToUser");
            Delete.Table("BaseProductToVendor");
            Delete.Table("Vendor");
            Delete.Table("UserRole");
            Delete.Table("security_UsersGroupsHierarchy");
            Delete.Table("Field");
            Delete.Table("UserLoginInfo");
            Delete.Table("security_UsersToUsersGroups");
            Delete.Table("EventType");
            Delete.Table("Event");
            Delete.Table("Material");
            Delete.Table("EquipmentToTask");
            Delete.Table("Equipment");
            Delete.Table("Fertilizer");
            Delete.Table("Chemical");
            Delete.Table("EmailTemplate");
            Delete.Table("EmailJobType");
        }
    }
}


