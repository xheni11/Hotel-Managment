using M19G1.DAL.Entities;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.DAL.Mapping
{
    public class LogModelMapping
    {
        public static LogModel ToModel(Log4NetLog log)
        {
            return new LogModel
            {
                Id = log.Id,
                Date=log.Date,
                Exception=log.Exception,
                Level=log.Level,
                Logger=log.Logger,
                Message=log.Message,
                Thread=log.Thread

            };
        }
        public static List<LogModel> ToModel(IEnumerable<Log4NetLog> role)
        {
            return role.Select(ToModel).ToList();
        }

    }
}
