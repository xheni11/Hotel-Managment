using M19G1.Models;
using M19G1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M19G1.Mapping
{
    public class LogViewModelMapping
    {
        public static LogViewModel ToViewModel(LogModel log)
        {
            return new LogViewModel
            {
                Id = log.Id,
                Date = log.Date,
                Exception = log.Exception,
                Level = log.Level,
                Logger = log.Logger,
                Message = log.Message,
                Thread = log.Thread

            };
        }
        public static List<LogViewModel> ToViewModel(IEnumerable<LogModel> role)
        {
            return role.Select(ToViewModel).ToList();
        }
    }
}