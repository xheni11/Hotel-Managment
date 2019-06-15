using M19G1.DAL;
using M19G1.DAL.Mapping;
using M19G1.DAL.Repository;
using M19G1.IBLL;
using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.BLL
{
    public class LogService: ILogService
    {
        private readonly UnitOfWork _internalUnitOfWork;
        private readonly LogRepository _logRepository;


        public LogService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
            _logRepository = _internalUnitOfWork.LogRepository;
        }
        public LogModel GetLogById(int id)
        {
            return LogModelMapping.ToModel(_logRepository.GetByID(id));
        }
        public List<LogModel> GetAllLogs()
        {
            return LogModelMapping.ToModel(_logRepository.GetAll());
        }
    }
}
