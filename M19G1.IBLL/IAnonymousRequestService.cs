﻿using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.IBLL
{
    public interface IAnonymousRequestService
    {
        void CreateAnonymousRequest(int idUser);
        void ConfirmedAnonymous(int idAnonymous);
        List<AnonymousRequestModel> GetAllRequests();
        AnonymousRequestModel GetRequestById(int id);
        int GetUserId(int id);
    }
}