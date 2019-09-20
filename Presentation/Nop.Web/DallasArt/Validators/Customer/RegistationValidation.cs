using FluentValidation;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Localization;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.ViewModels.Customer;
//using Nop.Web.Themes.Evantell.ViewModels.Customer;


namespace Nop.Web.DallasArt.Validators.Customer
{
    public class RegistrationValidation : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationValidation(ILocalizationService localizationService, IWorkContext workContext , IDallasArtContext dallasArtContext)
        {
            if (workContext.CurrentCustomer.IsRegistered()) return;
 
            if (dallasArtContext.IsRegistation)
            {
                RuleFor(x => x.CenterNameRequest).NotEmpty().WithMessage("Center Name is Required!");
         
                RuleFor(x => x.CenterGroupIdRequest).NotEmpty().WithMessage("CenterId is Required!");
            }

            if (dallasArtContext.IsEnrollment)
            {
                if (!dallasArtContext.IsPresetEnrollment)
                {
                    RuleFor(x => x.CenterNameRequest).NotEmpty().WithMessage("Center Name is Required!");
 
                  
                
                }

                RuleFor(x => x.EnrollmentType).NotEmpty().WithMessage("Center Enrollment Type is Required!");
            }


        }
    }
}