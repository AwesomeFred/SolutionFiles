
using System;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Services.Customers;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.DallasArt;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Stores;
using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Stores;
using Nop.Web.DallasArt.Services;

using Nop.Web.Themes.PAA.ViewModels;


namespace Nop.Web.DallasArt.Classes
{
    public static class Utilities
    {
        public static string CheckTrue = @"<img src='/DallasArt/Images/Tools/active-true.gif' />";
        public static string CheckFalse = @"<img src='/DallasArt/Images/Tools/active-false.gif' />";

        public enum EnrollmentProduct
        {
            Center = 102,
            Satellite = 1110,
            Free=1118,
            Month =1128,
            Quarter=1129,
            Year=1130
        }

        public static bool IsEnrollment(int id)
        {
            switch (id)
            {
                case (int)EnrollmentProduct.Center:
                case (int)EnrollmentProduct.Free:
                case (int)EnrollmentProduct.Month:
                case (int)EnrollmentProduct.Quarter:
                case (int)EnrollmentProduct.Year:
              
                return true;

                default:
                    return false;
            }
        }



     

        //public enum CustomerAttribute
        //{
        //    SecondMember = 2,
        //    Url = 4,
        //    ArtWork = 5,
        //    Renewal = 6,
        //    ArtworkApproval = 11,
        //    PartnerEmail = 12,
        //    HomePageSlideTitle =13,
             
        //    HomePageApprovalYes = 1011,
        //    HomePageApprovalNo  = 1012,



        //}



        public static IList<int> ValidMemberRoleIds(ICustomerService customerService)
        {
            IList<int> validRoleIds = new List<int>();

            foreach (CustomerRole role in customerService.GetAllCustomerRoles())
            {
                switch (role.SystemName)
                {

                    case "AdultMembership":
                    case "FamilyMembership1":
                    case "FamilyMembership2":
                    case "HonoraryMembership":
                    case "SeniorFamilyMembership1":
                    case "SeniorFamilyMembership2":
                    case "SeniorMembership":
                    case "StudentMembership":
                    {
                        validRoleIds.Add(role.Id);
                        break;
                    }

                    default: continue;
                }

            }
            return validRoleIds;
        }






        public static string DateTimeToShortDate(DateTime? dateTime)
        {
            if (dateTime == null) return String.Empty;
            DateTime result = DateTime.Parse(dateTime.ToString());
            return result.ToShortDateString();
        }










        // helper classes


        public class CurrentCustomer
        {
            public Customer Customer { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string Email { get; set; }

            public bool IsRegistered { get; set; }

            public int Id { get; set; }

            //    public int SatelliteaId { get; set; }

        }

 

        public static int QueueEmailNotification(
                                                MessageTemplate messageTemplate,
                                                EmailAccount emailAccount,
                                                int languageId,
                                                 IList<Token> tokens,
                                                string toEmailAddress,
                                                string toName,
                                                string attachmentFilePath = null,
                                                string attachmentFileName = null,
                                                string replyToEmailAddress = null,
                                                string replyToName = null
                                               )
        {
            


            ITokenizer tokenizer = EngineContext.Current.Resolve<ITokenizer>();
            IQueuedEmailService queuedEmailService = EngineContext.Current.Resolve<IQueuedEmailService>();

            //retrieve localized message template data
            var bcc = messageTemplate.GetLocalized(mt => mt.BccEmailAddresses, languageId);
            var subject = messageTemplate.GetLocalized(mt => mt.Subject, languageId);
            var body = messageTemplate.GetLocalized(mt => mt.Body, languageId);

            //Replace subject and body tokens 
            var subjectReplaced = tokenizer.Replace(subject, tokens, false);
            var bodyReplaced = tokenizer.Replace(body, tokens, true);

            var email = new QueuedEmail
            {
                Priority = QueuedEmailPriority.High,
                From = emailAccount.Email,
                FromName = emailAccount.DisplayName,
                To = toEmailAddress,                                  
                ToName = toName,
                ReplyTo = replyToEmailAddress,
                ReplyToName = replyToName,
                CC = string.Empty,
                Bcc = bcc,
                Subject = subjectReplaced,
                Body = bodyReplaced,
                AttachmentFilePath = attachmentFilePath,
                AttachmentFileName = attachmentFileName,
                AttachedDownloadId = messageTemplate.AttachedDownloadId,
                CreatedOnUtc = DateTime.UtcNow,
                EmailAccountId = emailAccount.Id
            };
// ***fred***///
            queuedEmailService.InsertQueuedEmail(email);
            return email.Id;
        }



        public static string StripppedHttp(string url)
        {

            string store = string.Format("{0}{1}", GetStoreUrl(), url);

            return store
                .ToLower();
            //  .Replace("https:", string.Empty)
            //      .Replace("http:", string.Empty);
        }



       

        public static IList<CustomerServices.AttributeValuePair> GetCustomerAttributes(Customer customer , int storeId = 0)
        {
            if (customer == null) return null;



            if (customer.Id == 3689)

            {

               // var x = 5;


            }





            var attributeValueList = new List<CustomerServices.AttributeValuePair>();

            string allCustomerAttributesFromStore = customer.GetAttribute<string>(SystemCustomerAttributeNames.CustomCustomerAttributes , storeId);

            if (string.IsNullOrWhiteSpace(allCustomerAttributesFromStore)) return attributeValueList; // none ? return empty list

            var customerAttributeService = EngineContext.Current.Resolve<ICustomerAttributeService>();

            var customerAttributes = customerAttributeService.GetAllCustomerAttributes();

            XDocument doc = XDocument.Parse(allCustomerAttributesFromStore);
            if (doc.Root == null) return attributeValueList;

            foreach (var selectedAttribute in  doc.Root.Descendants("CustomerAttribute"))
            {

                int selectedId = int.Parse(selectedAttribute.Attributes("ID").First().Value);

                foreach (var value in selectedAttribute.Descendants("CustomerAttributeValue").Elements("Value"))
                {
                    if (selectedId == 0) continue;

                    var customerAttribute = customerAttributes.SingleOrDefault(pva => pva.Id == selectedId);
                    if (customerAttribute == null) continue;

                    try
                    {
                        switch (customerAttribute.AttributeControlType)
                        {

                            case AttributeControlType.DropdownList:
                            case AttributeControlType.RadioList:
                            case AttributeControlType.Checkboxes:
                                var productVariantAttributeValue =
                                    customerAttribute.CustomerAttributeValues.Single(
                                        pvav => pvav.Id == int.Parse(value.Value));
                                var attributeValue = new CustomerServices.AttributeValuePair()
                                {
                                    AttributeName = customerAttribute.Name,
                                    ValueId = productVariantAttributeValue.Id,
                                    ValueText = productVariantAttributeValue.Name
                                };
                                attributeValueList.Add(attributeValue);
                                break;

                            default:
                                attributeValue = new CustomerServices.AttributeValuePair()
                                {
                                    AttributeName = customerAttribute.Name,
                                    ValueId = selectedId,
                                    ValueText = value.Value
                                };
                                attributeValueList.Add(attributeValue);
                                break;


                        }
                    }
#pragma warning disable CS0168 // Variable is declared but never used
                    catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
                    {

                    }
                }
            }

            return attributeValueList;
        }


        //public static SelfIdentificationViewModel SelfIdentificationViewModel
        //              (

        //                IWorkContext workContext,
        //                ICustomerService customerService,
        //                ICustomerAttributeService customerAttributeService,
        //                bool showCaptcha
        //              )
        //{
        //    return null;


        //}



        public static string CorrectViewPath(string path)
        {
           string storeName = EngineContext.Current.Resolve<IStoreContext>().CurrentStore.Name;

            return $"~/Themes/{storeName}/Views/{path}.cshtml";
        }


        public static string HelpLocationLink(Store store)
        {

            const string action = "Contact/ContactUs";
            string url = store.Url.Replace("http:", "").Replace("https", "");

            return string.Format("<a href='{0}{1}'>Contact Page</a>", url, action);

        }

        public static string TruncateLongString(string str, int maxLength)
        {
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }

        public static string ViewPath(String store, string path)
        {
            return string.Format("~/Themes/{0}/Views/RegisterCustomer/{1}.cshtml", store, path);
        }

        public static string ViewPath(String store, string path, string view)
        {
            return string.Format("~/Themes/{0}/Views/{1}/{2}.cshtml", store, path, view);
        }


        public static CustomerServices.AttributeValuePair GetAttributeValuePair(Customer customer, string name)
        {
            return (from v in GetCustomerAttributes(customer)
                    where String.Equals(v.AttributeName, name, StringComparison.CurrentCultureIgnoreCase)
                    select v).FirstOrDefault();
        }


        public static bool SetupFromInvitation(MemberEnrollment location, ref  RegistrationViewModel model)
        {
            if (location != null)
            {
          
                // check location is setup as satellite center
           //     if (location.SingleLocationCenter || !location.SatelliteCenter)
           //     return false;
                
                
           //     model.CenterGroupIdRequest = location.CenterGroupId;
          //      model.CenterNameRequest = location.CenterName.Replace("_", " ");
         //       model.PrimaryCenterName = location.PrimaryCenterName.Replace("_", " ");
         //       model.PrimaryCenterGroupId = location.PrimaryCenterGroupId;
                model.EzLinkRequest = true;

                return true;
            }
            return false;
        }

        public static string GetStoreUrl(int storeId = 0, bool useSsl = false)
        {
            Store currentStore = EngineContext.Current.Resolve<IStoreContext>().CurrentStore;
            Store orderStore = EngineContext.Current.Resolve<IStoreService>().GetStoreById(storeId);
            Store store = orderStore ?? currentStore;    // use orderStore, if available

            if (store == null)
                throw new Exception("No store could be loaded");

            return useSsl ? store.SecureUrl : store.Url;
        }

        public static string BuildStoreUrlLink(string path, string text, int store = 0, string target = "_blank")
        {
            return string.Format("<a href='{0}{1}' target='{3}'>{2}</a>", GetStoreUrl(store), path, text, target);
        }
    }
}