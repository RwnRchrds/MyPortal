using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.DocumentProvision;

namespace MyPortal.Logic.DocumentProviders
{
    public class LocalDocumentProvider : IDocumentProvider
    {
        public Task<FileMetadata> GetMetadata(string fileId)
        {
            throw new NotImplementedException();
        }

        public Task UploadFile(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFile(string fileId)
        {
            throw new NotImplementedException();
        }
    }
}
