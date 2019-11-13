using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;

namespace MyPortal.Services
{
    public class DocumentService : MyPortalService
    {
        public DocumentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task CreateDocument(Document document, string userId)
        {
            if (document.UploaderId == 0)
            {
                
                var uploader = await UnitOfWork.StaffMembers.GetByUserId(userId);

                if (uploader == null)
                {
                    throw new ServiceException(ExceptionType.NotFound,"Uploader not found");
                }

                document.UploaderId = uploader.Id;
            }

            else if (document.UploaderId != 0 && ! await UnitOfWork.StaffMembers.Any(x => x.Id == document.UploaderId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Uploader not found");
            }

            document.IsGeneral = true;

            document.Date = DateTime.Now;

            UnitOfWork.Documents.Add(document);

            await UnitOfWork.Complete();
        }

        public async Task CreatePersonalDocument(PersonDocument document, string userId)
        {
            if (!await UnitOfWork.People.Any(x => x.Id == document.PersonId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Person not found");
            }

            var uploader = await UnitOfWork.StaffMembers.GetByUserId(userId);

            if (uploader == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Staff member not found");
            }

            document.Document.IsGeneral = false;

            document.Document.Approved = true;

            document.Document.Date = DateTime.Now;

            document.Document.UploaderId = uploader.Id;

            var documentObject = document.Document;

            UnitOfWork.Documents.Add(documentObject);
            UnitOfWork.PersonDocuments.Add(document);

            await UnitOfWork.Complete();
        }

        public async Task DeleteDocument(int documentId)
        {
            var documentInDb = await GetDocumentById(documentId);

            if (documentInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Document not found");
            }

            UnitOfWork.Documents.Remove(documentInDb);

            await UnitOfWork.Complete();
        }

        public async Task DeletePersonalDocument(int documentId)
        {
            var staffDocument = await GetPersonalDocumentById(documentId);

            if (staffDocument == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Document not found");
            }

            var attachedDocument = staffDocument.Document;

            if (attachedDocument == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Document not found");
            }

            UnitOfWork.PersonDocuments.Remove(staffDocument);

            UnitOfWork.Documents.Remove(attachedDocument);

            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<Document>> GetAllGeneralDocuments()
        {
            return await UnitOfWork.Documents.GetGeneral();
        }

        public async Task<IEnumerable<Document>> GetApprovedGeneralDocuments()
        {
            return await UnitOfWork.Documents.GetApproved();
        }
        
        public async Task<Document> GetDocumentById(int documentId)
        {
            var document = await UnitOfWork.Documents.GetById(documentId);

            if (document == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Document not found");
            }

            return document;
        }
        
        public async Task<PersonDocument> GetPersonalDocumentById(int documentId)
        {
            var document = await UnitOfWork.PersonDocuments.GetById(documentId);

            if (document == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Document not found");
            }

            return document;
        }

        public async Task<IEnumerable<PersonDocument>> GetPersonalDocuments(int personId)
        {
            var documents = await UnitOfWork.PersonDocuments.GetByPerson(personId);

            return documents;
        }

        public async Task UpdateDocument(Document document)
        {
            var documentInDb = await GetDocumentById(document.Id);

            documentInDb.Description = document.Description;
            documentInDb.Url = document.Url;
            documentInDb.IsGeneral = true;
            documentInDb.Approved = document.Approved;

            await UnitOfWork.Complete();
        }
        
        public async Task UpdatePersonalDocument(PersonDocument document)
        {
            var documentInDb = await GetPersonalDocumentById(document.Id);

            documentInDb.Document.Description = document.Document.Description;
            documentInDb.Document.Url = document.Document.Url;
            documentInDb.Document.IsGeneral = false;
            documentInDb.Document.Approved = true;

            await UnitOfWork.Complete();
        }
    }
}