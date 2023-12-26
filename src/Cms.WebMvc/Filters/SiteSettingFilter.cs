using Cms.Infrastructure.Persistence.Interfaces;
using Cms.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cms.WebMvc.Filters
{
    public class SiteSettingFilter : IActionFilter
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IFAQRepository _faqRepository;

        public SiteSettingFilter(
            ISettingRepository settingRepository,
            IFAQRepository faqRepository)
        {
            _settingRepository = settingRepository;
            _faqRepository = faqRepository;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Controller is Controller controller)
            {
                controller.ViewData["SiteName"] = _settingRepository.GetByKey("SiteName").Value;
                controller.ViewData["LogoImageUrl"] = _settingRepository.GetByKey("LogoImageUrl").Value;
                
                controller.ViewData["HQAddress"] = _settingRepository.GetByKey("HQAddress").Value;
                controller.ViewData["HQWard"] = _settingRepository.GetByKey("HQWard").Value;
                controller.ViewData["HQDistrict"] = _settingRepository.GetByKey("HQDistrict").Value;
                controller.ViewData["HQProvince"] = _settingRepository.GetByKey("HQProvince").Value;
                controller.ViewData["HQPhone"] = _settingRepository.GetByKey("HQPhone").Value;
                controller.ViewData["HQEmail"] = _settingRepository.GetByKey("HQEmail").Value;
                controller.ViewData["HQGoogleMap"] = _settingRepository.GetByKey("HQGoogleMap").Value;

                var fags = _faqRepository.GetAllAsync().
                    Result.ToList();
                fags = fags.OrderBy(x =>x.Position).ToList();
                controller.ViewData["FAQs"] = fags;
            }
               
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
