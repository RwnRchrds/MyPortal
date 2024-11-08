using System;
using System.Threading.Tasks;

namespace MyPortal.Logic.Interfaces;

public interface IDocumentAccessController
{
    Task VerifyDirectoryAccess(Guid directoryId, bool edit);
    Task VerifyDocumentAccess(Guid documentId, bool edit);
}