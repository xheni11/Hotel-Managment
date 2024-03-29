﻿using M19G1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M19G1.IBLL
{
    public interface IPersonalDriverService
    {
        List<TaxiServiceModel> GetAllDriverService();
        void GetMyDriverServiceUnCompleted(int idUser);
        void RouteTaken(int id);
        void RouteCompleted(int id);
    }
}
