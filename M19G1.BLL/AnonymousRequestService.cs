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
        public void CreateAnonymousRequest(int idUser)
        {
                _internalUnitOfWork.AnonymousRequestRepository.Insert(AnonymousRequestMapping.ToEntityToCreate(idUser));
                _internalUnitOfWork.Save();

        }
        public void ConfirmedAnonymous(int idAnonymous)
        {
            AnonymousRequest request=_internalUnitOfWork.AnonymousRequestRepository.GetByID(idAnonymous);
            request.Confirmed = true;
            _internalUnitOfWork.AnonymousRequestRepository.Update(request);
            _internalUnitOfWork.Save();

        }
    }
}
