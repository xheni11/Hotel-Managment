﻿using System;
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
    }
}
