using AutoMapper;
using Payment_Gateway.BLL.Interfaces;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;

namespace Payment_Gateway.BLL.Implementation
{
    public class TransactionService : ITransactionService 
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IMapper _mapper;
        private readonly IRepository<Transaction> _transRepo;
        private readonly IUnitOfWork _unitOfWork;


        public TransactionService(IServiceFactory serviceFactory, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _serviceFactory = serviceFactory;
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
            _mapper = _serviceFactory.GetService<IMapper>();
            _transRepo = _unitOfWork.GetRepository<Transaction>();  
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            IEnumerable<Transaction> transactionsList = await _transRepo.GetAllAsync();
            if (!transactionsList.Any())
                throw new InvalidOperationException("Transaction list is empty");
            
            return transactionsList;

        }

        public async Task<Transaction> GetTransactions(int Id)
        {
            Transaction transactionList = await _transRepo.GetSingleByAsync(x => x.Id == Id);
            if (transactionList == null)
                throw new InvalidOperationException("No transactions with that Id");
            return transactionList;
        }

        public Task<IEnumerable<Transaction>> GetTransactionsByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<AppointmentDto>> SearchAppointmentsAsync(string searchTerm, DateTime? startDate, DateTime? endDate)
        //{
        //    //var query = _appointmentReposito.GetByAsync(a => a.EnrolleeName.ToLower().Contains(searchTerm.ToLower()) ||
        //    //                                                  a.Reason.ToLower().Contains(searchTerm.ToLower()));

        //    var transaction = await _transRepo.GetByAsync(a => a.User.ToString().Contains(searchTerm.ToLower()) ||
        //                                                       a.TrxRef.ToLower().Contains(searchTerm.ToLower()));
        //    if (startDate.HasValue && endDate.HasValue)
        //    {
        //        transaction = transaction.Where(t => t.CreatedAt >= startDate);
        //        transaction = transaction.Where(t => t.CreatedAt >= endDate);
        //    }


        //    //if (startDate.HasValue)
        //    //{
        //    //    query = query.Where(a => a.AppointmentDate >= startDate.Value);
        //    //}

        //    //if (endDate.HasValue)
        //    //{
        //    //    query = query.Where(a => a.AppointmentDate <= endDate.Value);
        //    //}

        //    var appointments = await query.OrderBy(a => a.AppointmentDate).ToListAsync();

        //    var appointmentDtos = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        //    return appointmentDtos;
        //}


        //public async Task<Transaction> GetTransactions(int Id)
        //{
        //    Transaction transactionList = await _transRepo.GetSingleByAsync(x => x.Id == Id);
        //    if (transactionList == null)
        //        throw new InvalidOperationException("no transactions found with this id");

        //    return transaction;
        //}
    }

}
