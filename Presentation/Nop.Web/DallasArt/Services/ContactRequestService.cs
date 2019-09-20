using System;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.DallasArt;

using Nop.Web.DallasArt.Interfaces;
using Nop.Web.DallasArt.ViewModels;


namespace Nop.Web.DallasArt.Services
{

    public class ContactRequestService : IContactRequestService
    {
        private readonly IRepository<ContactRequest> _contactRequestRepository;
        private readonly IWebHelper _webHelper;

        public ContactRequestService(
                                       IRepository<ContactRequest> contactRequestRepository, 
                                       IWebHelper webHelper 
 
                                     )
        {
            this._contactRequestRepository = contactRequestRepository;
            this._webHelper = webHelper;
        }







        public bool CreateContactRecord(ContactRequestViewModel model)
        {
 
            var s = model.SelfIdentificationViewModel;

            var category = model.SelectedOption;
                
            //    model.CategorySelectListItems
            // .Single(item => item.Value == model.SelectedCategoryId.ToString()) ;

            ContactRequest c = new ContactRequest
            {
                CustomerId = model.CurrentCustomer.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Phone = s.Phone,
                Email = s.Email,
                IsRegistered = s.Member,
                Store = s.Store,
                Question = model.Question,
                Category = category.Text,
                QuestionAsk = DateTime.Now,
                IpAddress = _webHelper.GetCurrentIpAddress(),
           //     CenterName = s.CenterName,
           //     CenterId = s.CenterId
            };

            _contactRequestRepository.Insert(c);
          
            return true;
        }
    }
}








