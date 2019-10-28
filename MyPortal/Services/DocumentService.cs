using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
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
                
                var uploader = await _unitOfWork.StaffMembers.GetByUserIdAsync(userId);

                if (uploader == null)
                {
                    throw new ProcessException(ExceptionType.NotFound,"Uploader not found");
                }

                document.UploaderId = uploader.Id;
            }

            else if (document.UploaderId != 0 && ! await _unitOfWork.StaffMembers.AnyAsync(x => x.Id == document.UploaderId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Uploader not found");
            }

            document.IsGeneral = true;

            document.Date = DateTime.Now;

            _unitOfWork.Documents.Add(document);

            await _unitOfWork.Complete();
        }

        public async Task CreatePersonalDocument(PersonDocument document, string userId)
        {
            if (!await _unitOfWork.People.AnyAsync(x => x.Id == document.PersonId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Person not found");
            }

            var uploader = await _unitOfWork.StaffMembers.GetByUserIdAsync(userId);

            if (uploader == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Staff member not found");
            }

            document.Document.IsGeneral = false;

            document.Document.Approved = true;

            document.Document.Date = DateTime.Now;

            document.Document.UploaderId = uploader.Id;

            var documentObject = document.Document;

            _unitOfWork.Documents.Add(documentObject);
            _unitOfWork.PersonDocuments.Add(document);

            await _unitOfWork.Complete();
        }

        public async Task DeleteDocument(int documentId)
        {
            var documentInDb = await _unitOfWork.Documents.GetByIdAsync(documentId);

            if (documentInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Document not found");
            }

            documentInDb.Deleted = true;

            await _unitOfWork.Complete();
        }

        public async Task DeletePersonalDocument(int documentId)
        {
            var staffDocument = await _unitOfWork.PersonDocuments.GetByIdAsync(documentId);

            if (staffDocument == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Document not found");
            }

            var attachedDocument = staffDocument.Document;

            if (attachedDocument == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Document not found");
            }

            _unitOfWork.PersonDocuments.Remove(staffDocument);

            _unitOfWork.Documents.Remove(attachedDocument);

            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<DocumentDto>> GetAllGeneralDocumentsDto()
        {
            var documents = await GetAllGeneralDocuments();

            return documents.Select(Mapper.Map<Document, DocumentDto>);
        }

        public async Task<IEnumerable<GridDocumentDto>> GetAllGeneralDocumentsDataGrid()
        {
            var documents =  await GetAllGeneralDocuments();
            return documents.Select(Mapper.Map<Document, GridDocumentDto>);
        }

        public async Task<IEnumerable<Document>> GetAllGeneralDocuments()
        {
            return await _unitOfWork.Documents.GetAllDocuments();
        }

        public async Task<IEnumerable<DocumentDto>> GetApprovedGeneralDocumentsDto()
        {
            var documents = await GetApprovedGeneralDocuments();

            return documents.Select(Mapper.Map<Document, DocumentDto>);
        }

        public async Task<IEnumerable<GridDocumentDto>> GetApprovedGeneralDocumentsDataGrid()
        {
            var documents = await GetApprovedGeneralDocuments();

            return documents.Select(Mapper.Map<Document, GridDocumentDto>);
        }

        public async Task<IEnumerable<Document>> GetApprovedGeneralDocuments()
        {
            return await _unitOfWork.Documents.GetApprovedDocuments();
        }
        
        public async Task<DocumentDto> GetDocumentById(int documentId)
        {
            var document = await _unitOfWork.Documents.GetByIdAsync(documentId);

            if (document == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Document not found");
            }

            return Mapper.Map<Document, DocumentDto>(document);
        }
        
        public static async Task<PersonDocumentDto> GetPersonalDocumentById(int documentId,
            MyPortalDbContext context)
        {
            var document = await context.PersonDocuments.SingleOrDefaultAsync(x => x.Id == documentId);

            if (document == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Document not found");
            }

            return Mapper.Map<PersonDocument, PersonDocumentDto>(document);
        }

        public static async Task<IEnumerable<PersonDocumentDto>> GetPersonalDocuments(int personId,
            MyPortalDbContext context)
        {
            var documents = await GetPersonalDocumentsModel(personId, context);

            return documents.Select(Mapper.Map<PersonDocument, PersonDocumentDto>);
        }

        public static async Task<IEnumerable<GridPersonDocumentDto>> GetPersonalDocumentsDataGrid(int personId,
            MyPortalDbContext context)
        {
            var documents = await GetPersonalDocumentsModel(personId, context);

            return documents.Select(Mapper.Map<PersonDocument, GridPersonDocumentDto>);
        }

        public static async Task<IEnumerable<PersonDocument>> GetPersonalDocumentsModel(int personId,
                            MyPortalDbContext context)
        {
            var documents = await context.PersonDocuments.Where(x => !x.Document.Deleted && x.PersonId == personId)
                .ToListAsync();

            return documents;
        }
        
        public static async Task UpdateDocument(Document document, MyPortalDbContext context)
        {
            var documentInDb = await context.Documents.SingleOrDefaultAsync(x => x.Id == document.Id);

            if (documentInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Document not found");
            }
            
            documentInDb.Description = document.Description;
            documentInDb.Url = document.Url;
            documentInDb.IsGeneral = true;
            documentInDb.Approved = document.Approved;

            await context.SaveChangesAsync();
        }
        
        public static async Task UpdatePersonalDocument(PersonDocument document, MyPortalDbContext context)
        {
            var documentInDb = await context.PersonDocuments.SingleOrDefaultAsync(x => x.Id == document.Id);

            if (documentInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Document not found");
            }

            documentInDb.Document.Description = document.Document.Description;
            documentInDb.Document.Url = document.Document.Url;
            documentInDb.Document.IsGeneral = false;
            documentInDb.Document.Approved = true;

            await context.SaveChangesAsync();
        }
    }
}