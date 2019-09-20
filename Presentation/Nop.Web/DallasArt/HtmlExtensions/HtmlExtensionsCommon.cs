using System.Web.Mvc;

namespace Nop.Web.DallasArt
{
    public static class HtmlExtensionsCommon
    {


        public enum Html5InputTypes
        {
           
            text,
            color,
            date,
            datetime,
            email,
            month,
            number,
            password,
            range,
            search,
            tel,
            time,
            url,
            week
                 
        }




        public enum HtmlButtonTypes
        {
            Submit,
            Button,
            Reset
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="tb">Tage Builder to add to </param>
        /// <param name="name">Required Name</param>
        /// <param name="id">Optional Id </param>
        public static void AddName( TagBuilder tb, string name, string id  )
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = TagBuilder.CreateSanitizedId(name);

                if (string.IsNullOrWhiteSpace(id))
                {
                    tb.GenerateId(name);
                }
                else
                {
                    tb.MergeAttribute("id", TagBuilder.CreateSanitizedId(id));
                }
            }
            tb.MergeAttribute("name", name);
        }
    }
}