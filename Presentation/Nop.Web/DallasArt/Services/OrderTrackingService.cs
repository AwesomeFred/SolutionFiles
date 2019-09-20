using System.Linq;
using Nop.Core.Data;
using Nop.Core.Domain.DallasArt;

namespace Nop.Web.DallasArt.Services
{
    public class OrderTrackingService : IOrderTrackingService
    {
        
        
        
        private readonly IRepository<MemberEnrollment> _orderTrackingRepository;


        public OrderTrackingService( IRepository<MemberEnrollment> orderTrackingRepository)
        {
            _orderTrackingRepository = orderTrackingRepository;
        }


        /// <summary>
        /// Logs the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        public void Create(MemberEnrollment record)
        {
            _orderTrackingRepository.Insert(record);
        }

        public void Update(MemberEnrollment record)
        {
            _orderTrackingRepository.Update(record);
        }


        public MemberEnrollment GetById(int id)
        {
             return  _orderTrackingRepository.GetById(id);
        }


        public MemberEnrollment GetByOrderId(int orderId)
        {

            return _orderTrackingRepository.Table.SingleOrDefault(r => r.Id == orderId);   
        }


  


    
    
    }
}