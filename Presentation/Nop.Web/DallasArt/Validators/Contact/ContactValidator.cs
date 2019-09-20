using FluentValidation;
using Nop.Core;
using Nop.Services.Localization;
using Nop.Web.DallasArt.ViewModels;
using Nop.Web.Framework.Validators;

namespace Nop.Web.DallasArt.Validators.Contact
{


    public class ContactRequestRegisterValidator : BaseNopValidator<ContactRequestViewModel>
    {
        public ContactRequestRegisterValidator(ILocalizationService localizationService, IWorkContext workContext)
        {
           // RuleFor(x => x.SelectedCategoryId).NotEmpty().WithMessage(localizationService.GetResource("DallasArt.Contact.Fields.TopicDropDown.Not.Selected"));
            RuleFor(x => x.Question).NotEmpty().WithMessage("Required");  //(localizationService.GetResource("Dallasart.Contact.Fields.Question.Required"));
        }

    }
}