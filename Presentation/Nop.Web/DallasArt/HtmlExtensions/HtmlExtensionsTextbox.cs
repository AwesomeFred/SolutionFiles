using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;


namespace Nop.Web.DallasArt
{
    public static class HtmlExtensionsTextBox
    {


        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>
            (
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            HtmlExtensionsCommon.Html5InputTypes type,
            object htmlAttrributes = null
            )
        {
            return BootstrapTextBoxFor
                (
                    htmlHelper,
                    expression,
                    type,
                    null, null,
                    false, false,
                    null,
                    htmlAttrributes
                );
        }

        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>
            (
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            HtmlExtensionsCommon.Html5InputTypes type,
            string cssClass = null,
            object htmlAttrributes = null
            )
        {
            return BootstrapTextBoxFor
            (
                htmlHelper,
                expression,
                type,
                null, null,
                false, false,
                cssClass,
                htmlAttrributes
            );
        }



        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>
        (
        this HtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TValue>> expression,
        HtmlExtensionsCommon.Html5InputTypes type,
        string title,
        string placeholder,
        bool isRequired,
        bool isAutoFocus,
        object htmlAttrributes = null
        )
        {
            return BootstrapTextBoxFor
            (
                htmlHelper,
                expression,
                type,
                title, placeholder,
                isRequired, isAutoFocus,
                null,
                htmlAttrributes
            );
        }


        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>
            (
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            HtmlExtensionsCommon.Html5InputTypes type,
            string title,
            string placeholder,
            bool isRequired,
            bool isAutoFocus,
            string cssClass = null,
            object htmlAttrributes = null
            )
        {
            // create route value dictionary
            var rvd = new RouteValueDictionary(HtmlHelper
                      .AnonymousObjectToHtmlAttributes(htmlAttrributes));

            // add all other types here
            rvd.Add("type", type.ToString());

            // other textbox parameters
            if (!string.IsNullOrWhiteSpace(title)) rvd.Add("title", title);
            if (!string.IsNullOrWhiteSpace(placeholder)) rvd.Add("placeholder", placeholder);
            if (isRequired) rvd.Add("required", "required");
            if (isAutoFocus) rvd.Add("autofocus", "autofocus");

            // if there is a cssClass make sure it contains form-control
            if (!string.IsNullOrWhiteSpace(cssClass))
            {
                cssClass = !cssClass.Contains("form-control")
                    ? "form-control " + cssClass
                    : cssClass;
            }
            else cssClass = "form-control";

            rvd.Add("class", cssClass);

            return htmlHelper.TextBoxFor(expression, rvd);


        }
    }
}