using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Attributes;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.Interfaces;


namespace Nop.Web.DallasArt.ViewModels
{
    [Validator(typeof(CoordinatorValidation))]
    public class CoordinatorViewModel : ICoordinator
    {
        private readonly  List<DropDownOption> _options = new List<DropDownOption>();


        public CoordinatorViewModel(ICustomerAttributeService customerAttributeService, bool display)
        {
            HydrateOption(customerAttributeService);
            Display = display;
        }

        private void HydrateOption(ICustomerAttributeService customerAttributeService)
        {
           List<CustomerAttributeValue>  values = new List<CustomerAttributeValue>();

            foreach (CustomerAttribute a in customerAttributeService.GetAllCustomerAttributes())
            {
                if (a.Name != "Coordinator")  continue;

                foreach (  var v in a.CustomerAttributeValues.OrderByDescending(x=>x.Id))
                {
                    _options.Insert(0, new DropDownOption { Id = v.Id  , Name = v.Name });
                }
            }
        }

        [DisplayName("Center Prayer Coordinator")]
        public List<DropDownOption> Options { get { return _options; } }
        public string SelectedAnswer { get; set; }


        public bool Display { get; set; }

        [DisplayName("Center Prayer Coordinator")]
        public virtual string OptionCoordinator { get; set; }

        public SelectList OptionSelectList { get;  set; }
        public SelectListItem OptionSelectedOption { get; set; }
    }




    public class CoordinatorValidation : AbstractValidator<CoordinatorViewModel>
    {
        public CoordinatorValidation(ILocalizationService localizationService)
        {
            RuleFor(x => x.SelectedAnswer).NotEmpty().WithMessage("Select A Value From List!");
        }
    }

}
