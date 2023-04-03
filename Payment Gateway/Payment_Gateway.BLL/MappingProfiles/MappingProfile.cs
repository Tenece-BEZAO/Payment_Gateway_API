using AutoMapper;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using PayStack.Net;

namespace Payment_Gateway.BLL.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<User, UserForRegistrationDto>();
            CreateMap<TransactionRequestDto, Transaction>();
            CreateMap<Transaction, TransactionVerifyResponse>();
            CreateMap<InitiateTransferRequest, InitiateTransferResponse>();
            CreateMap<Wallet, DepositPaymentRequest>();


            /*CreateMap<UserProfileDto, CustomerProfile>();
            CreateMap<CustomerProfile, UserProfileDto>();*/


        }
    }
}
