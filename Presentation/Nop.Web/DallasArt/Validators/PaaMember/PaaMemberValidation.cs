using FluentValidation;
using Nop.Core;
using Nop.Services.Localization;
using Nop.Web.DallasArt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Nop.Web.DallasArt.Validators.PaaMember
{
    public class PaaMemberValidator : AbstractValidator<IPaaMember>
    {


        public PaaMemberValidator(ILocalizationService localizationService, IWorkContext workContext, IDallasArtContext dallasArtContext)
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Address Is Required");
            RuleFor(x => x.SelectedRole).NotEqual((Services.MembershipService.SelectedRole)0).WithMessage("Please Select Membership Type");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name Is Required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name Is Required");

            RuleFor(x => x.StreetAddress).NotEmpty().WithMessage("Street Address Is Required");
            RuleFor(x => x.City).NotEmpty().WithMessage("City Name Is Required");

            RuleFor(x => x.SelectState).NotEqual( (Classes.Enums.States) 0).WithMessage("Please Select State");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("ZipCode is Required");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone Number is Required");

            RuleFor(x => x.Renewal).NotEmpty().WithMessage("Renewal Date is Required");

        }







    }
}