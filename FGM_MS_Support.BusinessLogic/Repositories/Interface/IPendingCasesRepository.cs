using FGM_MS_Support.BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGM_MS_Support.BusinessLogic.Repositories.Interface
{
    public  interface IPendingCasesRepository
    {
        ResponseObject GetNBSyncPendingCases ();
    }
}
