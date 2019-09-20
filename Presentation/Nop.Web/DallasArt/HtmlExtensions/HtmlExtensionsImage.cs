using System.Web.Mvc;

namespace Nop.Web.DallasArt
{
    public static class HtmlExtensionsImage
    {



        public static MvcHtmlString Image(
            this HtmlHelper htmlHelper,
            string src,
            string altText,
            object htmlAttributes = null) => htmlHelper.Image
                (src,
                    altText,
                    string.Empty,
                    string.Empty,
                    htmlAttributes
                );

        public static MvcHtmlString Image(this
            HtmlHelper htmlHelper,
            string src,
            string altText,
            string cssClass,
            object htmlAttributes = null) => htmlHelper.Image
                (src,
                    altText,
                    cssClass,
                    string.Empty,
                    htmlAttributes
                );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="src">Required</param>
        /// <param name="altText">Required</param>
        /// <param name="cssClass">Seperate by spaces   </param>
        /// <param name="name">Optional</param>
        /// <param name="htmlAttributes"> use underscore (data_xxx)</param>
        /// <returns></returns>
        public static MvcHtmlString Image(this
           HtmlHelper htmlHelper,
           string src,
           string altText,
           string cssClass,
           string name,
           object htmlAttributes = null    
           )
        {
            TagBuilder tb = new TagBuilder("img");

            // required name, alt
            tb.MergeAttribute("src", src);
            tb.MergeAttribute("alt", altText);

            // optional classes
            if (!string.IsNullOrWhiteSpace(cssClass))
            {
                tb.AddCssClass(cssClass);
            }

            // optional name/id
            HtmlExtensionsCommon.AddName( tb, name, string.Empty );

            // optional data-dictionary  
            tb.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            // html encode the string
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }


    }
}