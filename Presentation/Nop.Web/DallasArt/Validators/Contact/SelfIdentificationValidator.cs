using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.Interfaces.Contact;
using Nop.Web.Framework.Validators;

namespace Nop.Web.DallasArt.Validators.Contact
{
    public class SelfIdentificationValidator : BaseNopValidator<ISelfIdentificationViewModel>
    {

        public SelfIdentificationValidator(
            ILocalizationService localizationService, 
            IDallasArtContext dallasArtContext
            )
        {

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Required");   //(localizationService.GetResource("DallasArt.Contact.Fields.FirstName.Required"));
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Required");    //(localizationService.GetResource("DallasArt.Contact.Fields.LastName.Required"));


            if (dallasArtContext.NeedsEmail)
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("Required");   //(localizationService.GetResource("DallasArt.Contact.Fields.Email.Required"));
                RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid");   //(localizationService.GetResource("DallasArt.Contact.Fields.Email.Address.Invalid"));

                RuleFor(x => x.ConfirmEmail).NotEmpty().WithMessage("Required");   //(localizationService.GetResource("DallasArt.Contact.Fields.EmailConfirm.Required"));
                RuleFor(x => x.ConfirmEmail).Equal(x => x.Email).WithMessage("Email Mismatch");   //(localizationService.GetResource("Dallasart.Contact.Fields.Email.ConfirmEmail.EnteredFields.DoNotMatch"));
            }

            if (dallasArtContext.NeedsPhone)
            { 
                RuleFor(x => x.Phone).NotEmpty().WithMessage("Required");   //(localizationService.GetResource("Dallasart.Contact.Fields.PhoneNumber.Required"));
              //  RuleFor(x => x.Phone).Matches(@"(?:\d{3})[- ]?\d{3}[- ]?\d{4}").WithMessage("Phone Number must Be entered with dashes (xxx-xxx-xxxx)")  ;
                //RuleFor(x => x.Phone).Matches(@"(?:\(\d{3}\)|\d{3})[- ]?\d{3}[- ]?\d{4}");
              
                RuleFor(x => x.ConfirmPhone).NotEmpty().WithMessage("Required");   //(localizationService.GetResource("Dallasart.Contact.Fields.ConfirmPhoneNumber.Required"));
               // RuleFor(x => x.ConfirmPhone).Matches(@"(?:\d{3})[- ]?\d{3}[- ]?\d{4}").WithMessage("Phone Number must Be entered with dashes (xxx-xxx-xxxx)");
                
                RuleFor(x => x.ConfirmPhone).Equal(x => x.Phone).WithMessage("Phone # Mismatch");   //(localizationService.GetResource("Dallasart.Contact.Fields.PhoneNumber.ConfirmPhoneNumber.EnteredFields.DoNotMatch"));
            }
            
            if (dallasArtContext.NeedsPassword)
            { 
 
                RuleFor(x => x.Password).NotEmpty().WithMessage(localizationService.GetResource("DallasArt.Contact.Fields.Password.Required"));
                RuleFor(x => x.Password).Length(8,100).WithMessage(localizationService.GetResource("Dallasart.contact.fields.password.min.length"));
                RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage(localizationService.GetResource("DallasArt.Contact.Fields.ConfirmPassword.Required"));
                RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage(localizationService.GetResource("Dallasart.Contact.Fields.Password.ConfirmPassword.EnteredFields.donotmatch"));
            }

            if (
                    dallasArtContext.IsRegistration
                 || dallasArtContext.IsEnrollment
               )
            { 
                RuleFor(x => x.SelectedAnswer).NotEmpty().WithMessage(localizationService.GetResource("Dallasart.contact.fields.question.response.yesno"));
            }


            if (dallasArtContext.NeedMembershipStatus)
            {
                RuleFor(x => x.RadioButtionList.SelectedRole).GreaterThan(0).WithMessage("Please Select Member or Non-Member Status");
            }
        }
    }
}