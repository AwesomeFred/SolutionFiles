using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nop.Web.DallasArt.Interfaces;

namespace Nop.Web.DallasArt.ViewModels
{


    public class Size
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }

        public int SizeZ { get; set; }
    }


    public class FadingSlide
    {
        public Guid Guid { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public System.Drawing.Size Size { get; set; }

        public decimal Price { get; set; }

    }



    public  static   class FadingSlides 
    {
        public  static List<FadingSlide>  FadingSlidesList()
        {

          var  slides = new List<FadingSlide>();

            slides.Add(new FadingSlide { Email="jreedwrites@aol.com", FirstName = "Jody", LastName = "Reed", Title = "FACES OF COMPASSION - VERY VERACRUZ" } );

            slides.Add(new FadingSlide { Email = "arts@vongduane.com", FirstName = "VONGDUANE", LastName = "MANIVONG", Title = "TIME PERSISTENCE" });
            slides.Add(new FadingSlide { Email = "gpruitt14@gmail.com", FirstName = "GABRIELLE", LastName = "PRUITT", Title = "DOUBLE ROSE" });
            slides.Add(new FadingSlide { Email = "argomaniz@live.com", FirstName = "JOSEPH", LastName = "ARGOMANIZ", Title = "STREAMING" });
            slides.Add(new FadingSlide { Email = "wpdlk@verizon.net", FirstName = "WAYNE", LastName = "PODULKA", Title = "AUTUMN FOREST" });
            slides.Add(new FadingSlide {  Email = "hollan@hollanholmes.com", FirstName = "HOLLAN", LastName = "HOLMES", Title = "ABANDONED" });
            slides.Add(new FadingSlide {  Email = "ginger.cruikshank@3-form.com", FirstName = "GINGER", LastName = "CRUIKSHANK", Title = "ABSTRACT THINKING" });
            slides.Add(new FadingSlide {  Email = "gene@legacyportraitpainters.com", FirstName = "GENE",   LastName = "DILLARD", Title = "ANNETTE" });
            slides.Add(new FadingSlide {  Email = "beverlytheriault@tx.rr.com", FirstName = "BEVERLY", LastName = "THERLAULT", Title = "AUTUMN LIGHT" });
            slides.Add(new FadingSlide {  Email = "ruth@thespidersweb.org", FirstName = "RUTH",    LastName = "SIDDIQI", Title = "BLOOMING BALL CACTUS" });
            slides.Add(new FadingSlide {  Email = "kenw662@yahoo.com", FirstName = "KEN",     LastName = "WHEATLEY", Title = "CELESTIAL SUNRISE" });
            slides.Add(new FadingSlide {  Email = "ismmahmood@yahoo.com", FirstName = "ISMAIL", LastName = "MAHMOOD", Title = "DANNY" });
            slides.Add(new FadingSlide {  Email = "lynnwang0411@yahoo.com", FirstName = "LINGHUI", LastName = "WANG", Title = "DEVINE LIGHT" });
            slides.Add(new FadingSlide {  Email = "rodi_tima@yahoo.com", FirstName = "RODICA", LastName = "TIMARU", Title = "FLEETING BEAUTY" });
            slides.Add(new FadingSlide {  Email = "judith.pafford@verizon.net", FirstName = "JUDITH", LastName = "PAFFORD", Title = "FUSION" });
                                         
            slides.Add(new FadingSlide {  Email = "wtarrant@tx.rr.com", FirstName = "ANDREA", LastName = "STIGDON", Title = "GRANDMA'S HANDS" });
            slides.Add(new FadingSlide {  Email = "karlmeltonart@gmail.com", FirstName = "KARL", LastName = "MELTON", Title = "HOPE" });
            slides.Add(new FadingSlide {  Email = "seanbritt13@verizon.net", FirstName = "SEAN", LastName = "BRITT", Title = "INDEPENDENCE PASS RIDGE" });
            slides.Add(new FadingSlide {  Email = "estherbobdallas@yahoo.com", FirstName = "ESTER", LastName = "RITZ", Title = "LOST AND FOUND PLANO" });
            slides.Add(new FadingSlide {  Email = "soheyla100@gmail.com", FirstName = "SOHEYLA", LastName = "RASHIDYAN", Title = "MAKING DECISIONS" });
            slides.Add(new FadingSlide {  Email = "dianabeck@verizon.net", FirstName = "DIANA", LastName = "BECK", Title = "MORNING MIST" });
            slides.Add(new FadingSlide {  Email = "ness@utdallas.edu", FirstName = "JOHN", LastName = "VAN NESS", Title = "MOUNTAINS AT L'ESTAQUE AFTER CEZANNE" });
            slides.Add(new FadingSlide {  Email = "diana_marcus@hotmail.com", FirstName = "DIANA", LastName = "MARCUS", Title = "NIGHT" });
            slides.Add(new FadingSlide {  Email = "reed-john@att.net", FirstName = "JOHN", LastName = "REED", Title = "OLLA 12" });
            slides.Add(new FadingSlide {  Email = "fineartbyjulie@gmail.com", FirstName = "JULIE", LastName = "MORTILLARO", Title = "RUNNING FREE" });
                                         
            slides.Add(new FadingSlide {  Email = "brenda@brendaguyton.com", FirstName = "BRENDA", LastName = "GUYTON", Title = "SAVAGE WISDOM" });
            slides.Add(new FadingSlide {  Email = "jamesgilbreathartist@gmail.com", FirstName = "JAMES", LastName = "GILBREATH", Title = "TEXAS WEATHER" });
            slides.Add(new FadingSlide {  Email = "kristenpenrodart@gmail.com", FirstName = "KRISTEN", LastName = "PENROD", Title = "THE DREAMER" });
            slides.Add(new FadingSlide {  Email = "pinkyolson@gmail.com", FirstName = "CYD", LastName = "OLSON", Title = "THE SURFER DUDE" });
            slides.Add(new FadingSlide {  Email = "kaywilliamson2005@yahoo.com", FirstName = "KAY", LastName = "WILLIIAMSON", Title = "VERNAZZA, A CINQUE TERRE VILLAGE" });
            slides.Add(new FadingSlide {  Email = "dillarddon@yahoo.com", FirstName = "DON", LastName = "DILLARD", Title = "WACO SUSPENSION BRIDGE" });
            slides.Add(new FadingSlide {  Email = "maureenjistel@gmail.com", FirstName = "MAUREEN", LastName = "JISTEL", Title = "WANT TO PLAY BALL" });
            slides.Add(new FadingSlide {  Email = "davoblow@aol.com", FirstName = "DAVID", LastName = "BLOW", Title = "WINTER ICE" });
          

            return slides;

        }
    }
    
}