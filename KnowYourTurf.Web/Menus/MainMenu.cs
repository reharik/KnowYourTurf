using System.Collections.Generic;
using KnowYourTurf.Core.Enumerations;
using KnowYourTurf.Core.Html.Menu;
using KnowYourTurf.Web.Areas.Portfolio.Controllers;
using KnowYourTurf.Web.Config;
using KnowYourTurf.Web.Areas.Schedule.Controllers;
using KnowYourTurf.Web.Controllers;

namespace KnowYourTurf.Web.Menus
{
    public class MainMenu : IMenuConfig
    {
        private readonly IMenuBuilder _builder;

        public MainMenu(IMenuBuilder builder)
        {
            _builder = builder;
        }

        public IList<MenuItem> Build(bool withoutPermissions = false)
        {
            return DefaultMenubuilder(withoutPermissions);
        }

        private IList<MenuItem> DefaultMenubuilder(bool withoutPermissions = false)
        {
            return _builder
                .CreateNode<EmployeeDashboardController>(c => c.Display(null), WebLocalizationKeys.EMPLOYEE_DASHBOARD)
                .CreateNode<AppointmentCalendarController>(c => c.AppointmentCalendar(), WebLocalizationKeys.CALENDAR, AreaName.Schedule)
                .CreateNode(WebLocalizationKeys.ADMIN_TOOLS, "tools")
                    .HasChildren()
                        .CreateNode<TrainerListController>(c => c.ItemList(null), WebLocalizationKeys.TRAINERS)
                        .CreateNode<ClientListController>(c => c.ItemList(null), WebLocalizationKeys.CLIENTS)
                    .EndChildren()
                //                .CreateNode<PortfolioListController>(c => c.ItemList(null), WebLocalizationKeys.PORTFOLIOS,
//                                                     AreaName.Portfolio, "portfolios")
//                .CreateNode(WebLocalizationKeys.EMPTY_TOKEN, "separator")
//                    .HasChildren()
//                    .CreateNode<AllAssetsController>(c => c.AllAssetsList(null), WebLocalizationKeys.ALL_EXPERIENCE, AreaName.Portfolio, "assets")
//                    .CreateNode(WebLocalizationKeys.EMPTY_TOKEN, "separator")
//                    .CreateNode(WebLocalizationKeys.SECTION1)
//                        .HasChildren()
//                        .CreateNode<WorkHistoryListController>(c => c.ItemList(null), WebLocalizationKeys.WORK_HISTORY, AreaName.Portfolio)
//                        .CreateNode<MilitaryServiceListController>(c => c.ItemList(null), WebLocalizationKeys.MILITARY_SERVICE, AreaName.Portfolio)
//                        .CreateNode<ConsultingActivityListController>(c => c.ItemList(null), WebLocalizationKeys.CONSULTING_ACTIVITIES, AreaName.Portfolio)
//                        .EndChildren()
//                    .CreateNode(WebLocalizationKeys.SECTION2)
//                        .HasChildren()
//                        .CreateNode<EducationListController>(c => c.ItemList(null), WebLocalizationKeys.DEGREES, AreaName.Portfolio)
//                        .CreateNode<TrainingListController>(c => c.ItemList(null), WebLocalizationKeys.TRAINING_SEMINAR, AreaName.Portfolio)
//                        .CreateNode<ContinuingEducationListController>(c => c.ItemList(null), WebLocalizationKeys.CONTINUING_EDUCATION, AreaName.Portfolio)
//                        .EndChildren()
//                    .CreateNode(WebLocalizationKeys.SECTION3)
//                        .HasChildren()
//                        .CreateNode<LicenseListController>(c => c.ItemList(null), WebLocalizationKeys.LICENSES, AreaName.Portfolio)
//                        .CreateNode<ProfessionalCertificationListController>(c => c.ItemList(null), WebLocalizationKeys.PROFESSIONAL_CERTIFICATIONS, AreaName.Portfolio)
//                        .CreateNode<TechnicalCertificationListController>(c => c.ItemList(null), WebLocalizationKeys.TECHNICAL_CERTIFICATIONS, AreaName.Portfolio)
//                        .EndChildren()
//                    .CreateNode(WebLocalizationKeys.SECTION4)
//                        .HasChildren()
//                        .CreateNode<ClinicalExperienceListController>(c => c.ItemList(null), WebLocalizationKeys.CLINICAL_EXPERIENCE, AreaName.Portfolio)
//                        .CreateNode<MembershipListController>(c => c.ItemList(null), WebLocalizationKeys.MEMBERSHIPS, AreaName.Portfolio)
//                        .CreateNode<CommitteeListController>(c => c.ItemList(null), WebLocalizationKeys.COMMITTEES, AreaName.Portfolio)
//                        .EndChildren()
//                    .CreateNode(WebLocalizationKeys.SECTION5)
//                        .HasChildren()
//                        .CreateNode<GrantListController>(c => c.ItemList(null), WebLocalizationKeys.GRANTS, AreaName.Portfolio)
//                        .CreateNode<HonorListController>(c => c.ItemList(null), WebLocalizationKeys.HONORS, AreaName.Portfolio)
//                        .CreateNode<PublicationListController>(c => c.ItemList(null), WebLocalizationKeys.PUBLICATIONS, AreaName.Portfolio)
//                        .CreateNode<PresentationListController>(c => c.ItemList(null), WebLocalizationKeys.PRESENTATIONS, AreaName.Portfolio)
//                        .CreateNode<ResearchListController>(c => c.ItemList(null), WebLocalizationKeys.RESEARCH, AreaName.Portfolio)
//                        .CreateNode<TeachingExperienceListController>(c => c.ItemList(null), WebLocalizationKeys.TEACHING_EXPERIENCE, AreaName.Portfolio)
//                        .CreateNode<FellowshipListController>(c => c.ItemList(null), WebLocalizationKeys.FELLOWSHIPS, AreaName.Portfolio)
//                        .EndChildren()
//                    .CreateNode(WebLocalizationKeys.SECTION6)
//                        .HasChildren()
//                        .CreateNode<CommunityServiceListController>(c => c.ItemList(null), WebLocalizationKeys.COMMUNITY_SERVICE, AreaName.Portfolio)
//                        .CreateNode<ContractListController>(c => c.ItemList(null), WebLocalizationKeys.CONTRACTS, AreaName.Portfolio)
//                        .CreateNode<CoverTextListController>(c => c.ItemList(null), WebLocalizationKeys.COVER_TEXTS, AreaName.Portfolio)
//                        .CreateNode<FundedActivityListController>(c => c.ItemList(null), WebLocalizationKeys.FUNDED_ACTIVITIES, AreaName.Portfolio)
//                        .CreateNode<GoalListController>(c => c.ItemList(null), WebLocalizationKeys.GOALS, AreaName.Portfolio)
//                        .CreateNode<HealthPolicyListController>(c => c.ItemList(null), WebLocalizationKeys.HEALTH_POLICIES, AreaName.Portfolio)
//                        .CreateNode<InterviewListController>(c => c.ItemList(null), WebLocalizationKeys.INTERVIEWS, AreaName.Portfolio)
//                        .CreateNode<ReflectionListController>(c => c.ItemList(null), WebLocalizationKeys.REFLECTION_TITLE, AreaName.Portfolio)
//                        .CreateNode<ReviewListController>(c => c.ItemList(null), WebLocalizationKeys.REVIEWS, AreaName.Portfolio)
//                        .CreateNode<SoftwareListController>(c => c.ItemList(null), WebLocalizationKeys.SOFTWARE, AreaName.Portfolio)
//						.CreateNode<StudentActivityListController>(c => c.ItemList(null), WebLocalizationKeys.STUDENT_ACTIVITIES_TITLE, AreaName.Portfolio)
//                        .EndChildren()
//                    .EndChildren()
//                .CreateNode<DocumentListController>(c => c.ItemList(null), WebLocalizationKeys.FILES, AreaName.Portfolio)
//                .CreateNode(WebLocalizationKeys.EMPTY_TOKEN, "separator")
//                .CreateNode<ReflectionListController>(c => c.ItemList(null), WebLocalizationKeys.REFLECTIONS, AreaName.Portfolio)
//                .CreateNode<CoverTextListController>(c => c.ItemList(null), WebLocalizationKeys.COVER_TEXT, AreaName.Portfolio)
//                .CreateNode<HeadshotListController>(c => c.ItemList(null), WebLocalizationKeys.HEADSHOTS, AreaName.Portfolio)
//                //headshots
//                //.CreateNode<UserProfileController>(c => c.UserProfile(null), WebLocalizationKeys.MY_ACCOUNT)
//                .CreateNode(WebLocalizationKeys.EMPTY_TOKEN, "separator")
//                .CreateNode(WebLocalizationKeys.ADMIN_TOOLS, "tools")
//                    .HasChildren()
//                    .CreateNode<ComplianceItemSettingsController>(c => c.ComplianceItemSetting(null), WebLocalizationKeys.COMPLIANCE_SETTINGS, AreaName.Portfolio)
//                    .EndChildren()
                .MenuTree(withoutPermissions);
        }
    }
}













