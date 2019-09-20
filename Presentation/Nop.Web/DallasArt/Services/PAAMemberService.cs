using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Web.DallasArt.Classes;
using Nop.Web.DallasArt.ViewModels.Customer;


namespace Nop.Web.DallasArt.Services
{
    public class PaaMemberService
    {


        private readonly DateTime _fortyFiveDayAgo = DateTime.Today.AddDays(-45);



        private readonly HashSet<string> expiredMembers = new HashSet<string> {

"docj2912@yahoo.com",  //
"nlw1111@me.com",  //
"nan@exposedparts.com",//
"parneon1@gmail.com",//
"marilee@vergati.com",//
"lhsartist@gmail.com",//
"sandydrexel1@yahoo.com",//
"nsnotani@gmail.com",//
"ashlielthomas@hotmail.com",//
"jessicajitx@gmail.com",//
"kerrifryeck@gmail.com",//
"zhongjchen@yahoo.com", //
"stacya.davis@me.com",//
"analogof@tx.rr.com", //
"nerkutay@yahoo.com",//
"argomaniz@live.com",//
"frank.t@verizon.net", //
"dhurrga@gmail.com", //
"niexu1980@hotmail.com",//
"niexu1980@hotmail.com",//
"denanjums@sbcglobal.net",//
"phoenixtutorials@yahoo.com",//
"jgar802@sbcglobal.net",//
"wakane@sbcglobal.net",//
"ruqaili@hotmail.com",//
"aj_haskell@yahoo.com",//
"gerdareed1@yahoo.com",//
"Melissas@plano.gov",//
"shikha.mail@gmail.com",//
"rheidik@msn.com",//
"meraldalgic@gmail.com",//
"karlagoden@gmail.com",//
"jgham@att.net",//
"huiping.wang@gmail.com",//
"gdrevino@gmail.com",//
"dickmitchell10@gmail.com",//
"caressa_marquez@verizon.net",//
"artbyshermeen@yahoo.com",//
"fineartbyjulie@gmail.com",//
"shark928sa@gmail.com",
"kaywilliamson2005@yahoo.com",//
"kenw662@yahoo.com",//
"maureenwatkins10@yahoo.com",//
"51663626@99.com",//
"lynnwang0411@yahoo.com",//
"rodi_tima@yahoo.com",//
"mtifrea@gmail.com",//
"kaethe.thomas.2012@gmail.com",//
"eli.temchin@gmail.com",//
"pat.taylor.tx@gmail.com",//
"adam_smith1214@yahoo.com",//
"mandy_jane@juno.com",//
"mike123321123@yahoo.com",//
"sharon@serrago.com",//
"sid@shahabstudio.com",//
"elaheh52@yahoo.com",//
"reed-john@att.net",//
"sandy.ray@gte.net",//
"Mleighpierce@gmail.com",//
"kristenpenrodart@gmail.com",//
"judith.pafford@verizon.net",//
"laostman@verizon.net",//
"pinkyolson@gmail.com",//
"artist_mmp@yahoo.com",//
"michellelmixell@gmail.com",//
"vicki@mayhan.com",
"sretx@aol.com",  //  50
"jodymartinstudio@cloud.com",//
"diana_marcus@hotmail.com",//
"ismmahmood@yahoo.com",//
"these3andme@icloud.com",//
"rosrios1213@gmail.com",//
"rosrios1213@gmail.com",  //
"mike.korman@verizon.net", //
"nancy.korman@gte.net", //
"look21k@yahoo.com", //
"lovly_due1358@yahoo.com",//
"5613178@OO.com",          //
"kathleenh13@verizon.net",//
"MSKATHLEENW@aol.com", //
"leticiaherrera@leticiaherreraart.com",  //
"june_harris@msn.com",         //
"ahargis@visitdowntownplano.com",   //
"sherry@hallofangels.com",         //
"melissa@northtexastopteam.com",  //
"cmacgreen@yahoo.com",       //
"grayscarlett@yahoo.com",    //
"pty53indfw@hotmail.com",   //
"samkgoodlin@gmail.com",    //
"nurayf@yahoo.com",        //
"pamelins7712@aol.com",    //
"pamelins7712@aol.com",    //
"christadee@gmail.com",    //
"m.dickel@verizon.net",   //
"ladymarli007@gmail.com",  //
"motionart1958@gmail.com", //
"ginger.cruikshank@3-form.com", //
"beth@theartistexperience.com", //
"antwinette@hotmail.com",      //
"misookchung@sbcglobal.net",  //
"jechristy@gmail.com",       //
"aaronanddean@yahoo.com",    //
"dancharlesmd@gmail.com",    //
"chrisbrownartist@att.net",  //
"mbrigante@simon.com",       //
"pouran11@yahoo.com",        //
"art_sutra@yahoo.com",       //
"tulikabhatia1@gmail.com",   //
"juliebarbeau@me.com",       //
"nida.bangash@gmail.com",    //
"ramakbaghaie@yahoo.com",    //
"chantal_sl@yahoo.com",      //
"gd.anastascio@yahoo.com",  //
"h-amador@hotmail.com",     //
"nazaninfy@yahoo.com",      //
"mirthadallas@gmail.com",   //
"autos_paris@yahoo.com"    //
 };






        public IList<MemberListViewModel>  ExcludeNonMemberRoles(ICustomerService customerService, int storeId)
        {
            int[] roleIds = Utilities.ValidMemberRoleIds(customerService).ToArray();

            int count = 0;
            int total = 0;

            IList<MemberListViewModel> expiredList = new List<MemberListViewModel>(); 

            List<Customer> customer = new List<Customer>();

            IList<Customer> members = customerService.GetAllCustomers().Where(e =>  expiredMembers.Contains(e.Email)).ToList();

            int a = expiredMembers.Count;
            int b = members.Count;





            foreach (var member in members)
            {


                foreach (CustomerServices.AttributeValuePair customerAttr
                           in Utilities.GetCustomerAttributes(member))
                {

                    switch (customerAttr.ValueId)
                    {

                        case (int)Utilities.CustomerAttribute.Renewal:
                            {
                                DateTime expires;
                              //  bool expired;// = false;
                                string joe = customerAttr.ValueText;


                                if (DateTime.TryParse(customerAttr.ValueText, out expires))
                                {

                                    if ( _fortyFiveDayAgo >=  expires )
                                    {
                                        var p = new MemberListViewModel {
                                            
                                            Email = member.Email,
                                            FirstName = member.GetAttribute<string>(SystemCustomerAttributeNames.FirstName, storeId),
                                            LastName = member.GetAttribute<string>(SystemCustomerAttributeNames.LastName, storeId),
                                            StreetAddress = member.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress, storeId),
                                            City = member.GetAttribute<string>(SystemCustomerAttributeNames.City, storeId),
                                            State = member.GetAttribute<string>(SystemCustomerAttributeNames.StateProvinceId, storeId),
                                            ZipCode = member.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode, storeId),
                                            Phone = member.GetAttribute<string>(SystemCustomerAttributeNames.Phone, storeId),
                                            Renewal = expires

                                        };

                                        expiredList.Add(p);
                                        count++;
                                        // expired = true;
                                    }
                                }

                                else
                                {

                                    var name = member.Email;


                                }



                                break;
                            }
                    }

                }
                total++;
            }

            var list = expiredList;

            return  expiredList;
        }











        public IList<MemberListViewModel> AllMembers(ICustomerService customerService, int storeId)
        {
            IList<MemberListViewModel> membersList = new List<MemberListViewModel>();

            int[] roleIds = Utilities.ValidMemberRoleIds(customerService).ToArray();


            IPagedList<Core.Domain.Customers.Customer> members =
                customerService.GetAllCustomers(customerRoleIds: roleIds, pageIndex: 0, pageSize: 500);

            var count = members.TotalCount;

            foreach (Core.Domain.Customers.Customer member in members)
            {


                if (member.Id == 3689)

                {

                    //   var x = 5;


                }

                MemberListViewModel ml = new MemberListViewModel();


                // assume first non-system role
                foreach (var role in member.CustomerRoles)
                {
                    if (role.IsSystemRole) continue;
                    ml.MembershipType = role.SystemName;
                    break;
                }


                // add custom customer attributes
                foreach (CustomerServices.AttributeValuePair customerAttr
                              in Utilities.GetCustomerAttributes(member))
                {

                    switch (customerAttr.ValueId)
                    {

                        case (int)Utilities.CustomerAttribute.SecondMember:
                            {
                                ml.NonPayingMember = customerAttr.ValueText == "Yes";

                                break;
                            }

                        case (int)Utilities.CustomerAttribute.Url:
                            {
                                ml.Url = customerAttr.ValueText;

                                break;
                            }

                        case (int)Utilities.CustomerAttribute.ArtWork:
                            {

                                ml.ArtWork = customerAttr.ValueText;

                                break;
                            }


                        case (int)Utilities.CustomerAttribute.Renewal:
                            {

                                try
                                {
                                    ml.Renewal = DateTime.Parse(customerAttr.ValueText);

                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);




                                    throw new Exception("Bad Customer Renewal Date For " + member.Email);
                                }




                                break;
                            }

                    }

                }


                ml.Email = member.Email;

                ml.FirstName = member.GetAttribute<string>(SystemCustomerAttributeNames.FirstName, storeId);
                ml.LastName = member.GetAttribute<string>(SystemCustomerAttributeNames.LastName, storeId);
                ml.Company = member.GetAttribute<string>(SystemCustomerAttributeNames.Company, storeId);

                ml.StreetAddress = member.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress, storeId);
                ml.StreetAddress2 = member.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2, storeId);

                ml.City = member.GetAttribute<string>(SystemCustomerAttributeNames.City, storeId);
                ml.ZipCode = member.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode, storeId);

                ml.State = member.GetAttribute<string>(SystemCustomerAttributeNames.StateProvinceId, storeId);


                ml.Phone = member.GetAttribute<string>(SystemCustomerAttributeNames.Phone, storeId);



                //     var x = ml;


                membersList.Add(ml);





                //  Debug.WriteLine(member.Email);
            }




            return membersList;

        }





    }
}