using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Logics
{
    public interface IFileManagerLogic
    {
        Task Upload(FileModel model);

        Task<byte[]> Get(string imageName);

        Task Delete(string imageName);
    }
}
