﻿using M19G1.DAL;
using M19G1.DAL.Entities;
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
    public class AnonymousRequestService: IAnonymousRequestService
    {
        
        private readonly UnitOfWork _internalUnitOfWork;
        private readonly AnonymousRequestRepository _anonymousRepository;

        public AnonymousRequestService(UnitOfWork unitOfWork)
        {
            _internalUnitOfWork = unitOfWork;
            _anonymousRepository = _internalUnitOfWork.AnonymousRequestRepository;
        }

        public void CreateAnonymousRequest(int idUser)
        {
                _anonymousRepository.Insert(AnonymousRequestModelMapping.ToEntityToCreate(idUser));
                _internalUnitOfWork.Save();

        }
        public void ConfirmedAnonymous(int idAnonymous)
        {
            AnonymousRequest request= _anonymousRepository.GetByID(idAnonymous);
            request.Confirmed = true;
            _internalUnitOfWork.AnonymousRequestRepository.Update(request);
            _internalUnitOfWork.Save();

        }

        public List<AnonymousRequestModel> GetAllRequests()
        {
            return AnonymousRequestModelMapping.ToModel(_anonymousRepository.GetAll().Where(a=>a.Confirmed==false));
        }
        public AnonymousRequestModel GetRequestById(int id)
        {
            return AnonymousRequestModelMapping.ToModel(_anonymousRepository.GetByID(id));
        }

    }
}
