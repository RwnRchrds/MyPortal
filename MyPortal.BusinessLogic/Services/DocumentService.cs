using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class DocumentService : MyPortalService
    {
        public DocumentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public DocumentService() : base()
        {

        }

        public async Task CreateDocument(DocumentDto document)
        {
            document.IsGeneral = true;

            document.Date = DateTime.Now;

            UnitOfWork.Documents.Add(Mapper.Map<Document>(document));
        }

        public async Task CreatePersonAttachment(PersonAttachmentDto attachment)
        {
            if (!await UnitOfWork.People.Any(x => x.Id == attachment.PersonId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Person not found");
            }

            attachment.Document.IsGeneral = false;

            attachment.Document.Approved = true;

            attachment.Document.Date = DateTime.Now;

            var documentObject = attachment.Document;

            UnitOfWork.Documents.Add(Mapper.Map<Document>(documentObject));
            UnitOfWork.PersonAttachments.Add(Mapper.Map<PersonAttachment>(attachment));
        }

        public async Task DeleteDocument(int documentId)
        {
            var documentInDb = await UnitOfWork.Documents.GetById(documentId);

            if (documentInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Document not found.");
            }

            UnitOfWork.Documents.Remove(documentInDb);
        }

        public async Task DeletePersonAttachment(int documentId)
        {
            var staffDocument = await UnitOfWork.PersonAttachments.GetById(documentId);

            if (staffDocument == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Attachment not found");
            }

            var attachedDocument = staffDocument.Document;

            if (attachedDocument == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Document not found");
            }

            UnitOfWork.PersonAttachments.Remove(staffDocument);

            UnitOfWork.Documents.Remove(attachedDocument);
        }

        public async Task<IEnumerable<DocumentDto>> GetAllGeneralDocuments()
        {
            return (await UnitOfWork.Documents.GetGeneral()).Select(Mapper.Map<DocumentDto>).ToList();
        }

        public async Task<IEnumerable<DocumentDto>> GetApprovedGeneralDocuments()
        {
            return (await UnitOfWork.Documents.GetApproved()).Select(Mapper.Map<DocumentDto>).ToList();
        }
        
        public async Task<DocumentDto> GetDocumentById(int documentId)
        {
            var document = await UnitOfWork.Documents.GetById(documentId);

            if (document == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Document not found.");
            }

            return Mapper.Map<DocumentDto>(document);
        }
        
        public async Task<PersonAttachmentDto> GetPersonAttachmentById(int documentId)
        {
            var document = await UnitOfWork.PersonAttachments.GetById(documentId);

            if (document == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Document not found.");
            }

            return Mapper.Map<PersonAttachmentDto>(document);
        }

        public async Task<IEnumerable<PersonAttachmentDto>> GetPersonAttachments(int personId)
        {
            return (await UnitOfWork.PersonAttachments.GetByPerson(personId)).Select(Mapper.Map<PersonAttachmentDto>).ToList();
        }

        public async Task UpdateDocument(DocumentDto document)
        {
            var documentInDb = await GetDocumentById(document.Id);

            documentInDb.Description = document.Description;
            documentInDb.Url = document.Url;
            documentInDb.IsGeneral = true;
            documentInDb.Approved = document.Approved;
        }
        
        public async Task UpdatePersonAttachment(PersonAttachmentDto attachment)
        {
            var documentInDb = await GetPersonAttachmentById(attachment.Id);

            documentInDb.Document.Description = attachment.Document.Description;
            documentInDb.Document.Url = attachment.Document.Url;
            documentInDb.Document.IsGeneral = false;
            documentInDb.Document.Approved = true;
        }
    }
}