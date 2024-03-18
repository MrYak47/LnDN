using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
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

        public static Comment ToCommFromCreate(this CreateCommDto comCreate, int stockId)
        {
            return new Comment
            {

                Title = comCreate.Title,
                Content = comCreate.Content,
                StockId= stockId,

            };
        }

         public static Comment ToCommFromUpdate(this UpdateCommReqDto UpComm)
        {
            return new Comment
            {

                Title = UpComm.Title,
                Content = UpComm.Content,

            };
        }
    }
}