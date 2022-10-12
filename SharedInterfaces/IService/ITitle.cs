using Data.Model.Common;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharedInterfaces.IService
{
   
    public interface ITitle
    {
        Task<ResponseModel<TitleList>> PostTitle(TitleList Data);
    }
}
