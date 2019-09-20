using System;
using Nop.Core;

namespace Nop.Web.DallasArt.Domain
{
    public class ContactRequest : BaseEntity
    {
        public virtual int ContectRequestId { get; set; }
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Email { get; set; }

        public virtual int CustomerId { get; set; }

        public virtual bool IsRegistered { get; set; }

        public virtual int Store { get; set; }

        public string IpAddress { get; set; }

        public virtual string Category { get; set; }

        public virtual string Question { get; set; }

        public virtual DateTime QuestionAsk { get; set; }

        public virtual DateTime? QuestionResolved { get; set; }
    }
}