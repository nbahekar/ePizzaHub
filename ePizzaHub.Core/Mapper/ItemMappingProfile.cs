using AutoMapper;
using ePizzaHub.Infra.Models;
using ePizzaHub.Models.ApiModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Core.Mapper
{
    public class ItemMappingProfile: Profile
    {
        public ItemMappingProfile() {

            CreateMap<Item, GetItemResponse>();
        }
    }
}
