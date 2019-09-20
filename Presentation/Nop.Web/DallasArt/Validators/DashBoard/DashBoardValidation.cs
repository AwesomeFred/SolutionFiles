using FluentValidation;
using Nop.Core;
using Nop.Services.Localization;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Interfaces.DashBoard;



namespace Nop.Web.DallasArt.Validators.DashBoard
{
    public class DashBoardValidation : AbstractValidator<IMembershipInstructionsViewModel>
    {
        public DashBoardValidation(ILocalizationService localizationService, IWorkContext workContext, IDallasArtContext dallasArtContext)
        {
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email Address Is Required");
        }
    }
}