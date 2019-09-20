using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.DallasArt.Interfaces
{
    public interface ICoordinator
    {
        bool Display { get; set; }
 
        string OptionCoordinator { get; set; }
   
        SelectList OptionSelectList { get; }
       
        SelectListItem OptionSelectedOption { get; set; }
    
    }
}