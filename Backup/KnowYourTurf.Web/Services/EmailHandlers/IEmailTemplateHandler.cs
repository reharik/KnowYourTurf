using KnowYourTurf.Core.Domain;

namespace KnowYourTurf.Web.Services.EmailHandlers
{
    public interface IEmailTemplateHandler
    {
        void Execute(EmailJob emailJob);
    }
}