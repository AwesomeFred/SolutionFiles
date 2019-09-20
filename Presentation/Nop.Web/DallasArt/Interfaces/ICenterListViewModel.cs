using System;
using System.Collections.Generic;
using Nop.Web.DallasArt.Interfaces.Customer;


namespace Nop.Web.DallasArt.Interfaces
{
    public interface ICenterListViewModel
    {

        IEnumerable<ICenter> Centers { get; }


    }
}