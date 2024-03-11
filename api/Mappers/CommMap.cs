using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommMap
    {
        public static CommDto ToCommDto(this Comment comModel)
        {
            return new CommDto
            {
                Id = comModel.Id,
                Title = comModel.Title,
                Content = comModel.Content,
                Createdon = comModel.Createdon,
                StockId = comModel.StockId,

            };
        }
    }
}