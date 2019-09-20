using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Infrastructure;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Web.DallasArt.Interfaces;


namespace Nop.Web.DallasArt.Services
{

    public class CustomerServices : ICustomerServices
    {
        private ICustomerAttributeService _attributeService;

        public static string Center { get { return "Center Name"; } }
        public static string CenterId { get { return "Center Enrollment Id"; } }

        public class AttributeValuePair
        {
            public string AttributeName { get; set; }
            public int ValueId { get; set; }
            public string ValueText { get; set; }
        }


        public CustomerServices(ICustomerAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        private static AttributeValuePair BuildAVP(
                          AttributeControlType attributeControlType,
                          CustomerAttribute customerAttribute, string strValue
             )
        {
            switch (attributeControlType)
            {
                case AttributeControlType.DropdownList:
                case AttributeControlType.RadioList:
                case AttributeControlType.Checkboxes:

                    var productVariantAttributeValue =
                         customerAttribute.CustomerAttributeValues
                            .Single(pvav => pvav.Id == Int32.Parse(strValue));

                    var attributeValue = new AttributeValuePair()
                    {
                        AttributeName = customerAttribute.Name,
                        ValueId = productVariantAttributeValue.Id,
                        ValueText = productVariantAttributeValue.Name
                    };
                    return attributeValue;


                default: attributeValue = new AttributeValuePair()
                {
                    AttributeName = customerAttribute.Name,
                    ValueId = 0,
                    ValueText = strValue
                };
                    return attributeValue;
            }
        }


        public static IList<AttributeValuePair> GetCustomerAttributes(Customer customer)
        {
            if (customer == null) return null;

            string selectedCustomerAttributes =
                customer.GetAttribute<string>(SystemCustomerAttributeNames.CustomCustomerAttributes);

            // start a list
            var attributeValueList = new List<AttributeValuePair>();

            // nothing found ?? return empty list;
            if (string.IsNullOrWhiteSpace(selectedCustomerAttributes))
            {
                return attributeValueList;
            }

            // get the list of existing  customerAttributes
            var customerAttributeService = EngineContext.Current.Resolve<ICustomerAttributeService>();
            var customerAttributes = customerAttributeService.GetAllCustomerAttributes();

            // get the list and quit if it is empty 
            XDocument doc = XDocument.Parse(selectedCustomerAttributes);
            if (doc.Root == null) return null;

            // pull  attributes from the xml
            var selectedAttributes = doc.Root.Descendants("CustomerAttribute");

            // roll through the array, parsing the xml into the model   
            foreach (var selectedAttribute in selectedAttributes)
            {
                int selectedId = Int32.Parse(selectedAttribute.Attributes("ID").First().Value);

                attributeValueList.AddRange(from value in selectedAttribute.Descendants("CustomerAttributeValue").Elements("Value")
                                            where selectedId != 0
                                            let customerAttribute = customerAttributes.SingleOrDefault(pva => pva.Id == selectedId)
                                            where customerAttribute != null
                                            select BuildAVP(customerAttribute.AttributeControlType, customerAttribute, value.Value));
            }

            return attributeValueList;
        }



        public static string GetCustomerAttribute(Customer customer, string attributeName)
        {
            AttributeValuePair attribute = GetCustomerAttributes(customer)
                .FirstOrDefault(n => Equals(n.AttributeName.Trim().ToLower(), attributeName.Trim().ToLower()));
            return attribute != null ? attribute.ValueText : null;
        }










    }
}


