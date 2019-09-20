using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Nop.Web.DallasArt
{
    public static class HtmlExtensionsButton
    {

        public static MvcHtmlString BootstrapButton(
            this HtmlHelper htmlHelper,
            string innerHtml,
            object htmlAttributes = null
            )
        {
            return BootstrapButton(
                htmlHelper, innerHtml,
                null, null, null,
                false, false,
                HtmlExtensionsCommon.HtmlButtonTypes.Submit,
                htmlAttributes);

        }


        public static MvcHtmlString BootstrapButton(
            this HtmlHelper htmlHelper,
            string innerHtml,
            string cssClass,
            object htmlAttributes = null
            )
        {
            return BootstrapButton(
            htmlHelper, innerHtml,
            cssClass,
            null, null,
            false, false,
            HtmlExtensionsCommon.HtmlButtonTypes.Submit,
            htmlAttributes);
        }


        // add more overrides as needed


        public static MvcHtmlString BootstrapButton(
            this HtmlHelper htmlHelper,
            string innerHtml,
            string cssClass,
            string name,
            string title,
            bool isFormNoValidate = false,
            bool isAutoFocus = false,
            HtmlExtensionsCommon.HtmlButtonTypes buttonType =
                    HtmlExtensionsCommon.HtmlButtonTypes.Submit,
            object htmlAttributes = null
            )
        {
            TagBuilder tb = new TagBuilder("button");


            if (!string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = !cssClass.Contains("btn-")
                           ? "btn-primary " + cssClass
                           : "btn-primary ";
            }

            tb.AddCssClass(cssClass);

            tb.AddCssClass("btn");

            if (!string.IsNullOrWhiteSpace(title)) tb.MergeAttribute("title", title);

            if (isFormNoValidate) tb.MergeAttribute("isFormNoValidate", "isFormNoValidate");

            if (isAutoFocus) tb.MergeAttribute("isAutoFocus", "isAutoFocus");

            // optional data-dictionary  
            tb.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            // optional name/id
            HtmlExtensionsCommon.AddName(tb, name, string.Empty);

            tb.InnerHtml = innerHtml;

            switch (buttonType)
            {
                case HtmlExtensionsCommon.HtmlButtonTypes.Submit:
                    tb.MergeAttribute("type", "submit");
                    break;
                case HtmlExtensionsCommon.HtmlButtonTypes.Button:
                    tb.MergeAttribute("type", "submit");
                    break;
                case HtmlExtensionsCommon.HtmlButtonTypes.Reset:
                    tb.MergeAttribute("type", "submit");
                    break;
            }

            return MvcHtmlString.Create(tb.ToString());
        }
    }
}