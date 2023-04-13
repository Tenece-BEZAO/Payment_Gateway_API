using AutoMapper;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;
using Payment_Gateway.Shared.DataTransferObjects.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.MappingProfiles.MappingConfiguration
{
    public class WalletMappingProfile: Profile
    {
        public WalletMappingProfile()
        {
            CreateMap<Wallet, WalletDto>();
        }
    }
}
