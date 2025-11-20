
using EZSocketNc.Extensions;
using EZSocketNc.Mqtts.Dtos;

using System;

namespace EZSocketNc.Db
{
    public class CmdHelper
    {
        public static CmdRetryEntity ToEntity(BaseMsg msg,string key)
        {
            var entity = new CmdRetryEntity()
            {
                Id = msg.Id,
            };
            entity.DataJson = msg.ToJSON();
            entity.Key = key;
            entity.RetryTimes = 1;
            entity.CreateTime = DateTime.Now;
            return entity;
        }
    }
}
