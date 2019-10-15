using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class DocumentProcesses
    {
        public static async Task CreateDocument(Document document, string userId, MyPortalDbContext context)
        {
            if (document.UploaderId == 0)
            {
                
                var uploader = await context.StaffMembers.SingleOrDefaultAsync(x => x.Person.UserId == userId);
                if (uploader == null)
                {
                    throw new ProcessException(ExceptionType.NotFound,"Uploader not found");
                }

                document.UploaderId = uploader.Id;
            }

            else if (document.UploaderId != 0 && ! await context.StaffMembers.AnyAsync(x => x.Id == document.UploaderId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Uploader not found");
            }

            document.IsGeneral = true;

            document.Date = DateTime.Now;

            context.Documents.Add(document);
            await context.SaveChangesAsync();
        }

        public static async Task CreatePersonalDocument(PersonDocument document, string userId, MyPortalDbContext context)
        {
            if (!await context.Persons.AnyAsync(x => x.Id == document.PersonId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Person not found");
            }

            var uploader = await context.StaffMembers.SingleOrDefaultAsync(x => x.Person.UserId == userId);

            if (uploader == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Staff member not found");
            }

            document.Document.IsGeneral = false;

            document.Document.Approved = true;

            document.Document.Date = DateTime.Now;

            document.Document.UploaderId = uploader.Id;

            var documentObject = document.Document;

            context.Documents.Add(documentObject);
            context.PersonDocuments.Add(document);

            await context.SaveChangesAsync();
        }

        public static async Task DeleteDocument(int documentId, MyPortalDbContext context)
        {
            var documentInDb = await context.Documents.SingleOrDefaultAsync(x => x.Id == documentId);

            if (documentInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Document not found");
            }

            documentInDb.Deleted = true;
            
            //Delete from database
            //context.Documents.Remove(documentInDb);
            
            await context.SaveChangesAsync();
        }

        public static async Task DeletePersonalDocument(int documentId, MyPortalDbContext context)
        {
            var staffDocument = await context.PersonDocuments.SingleOrDefaultAsync(x => x.Id == documentId);

            if (staffDocument == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Document not found");
            }

            var attachedDocument = staffDocument.Document;

            if (attachedDocument == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Document not found");
            }

            context.PersonDocuments.Remove(staffDocument);

            context.Documents.Remove(attachedDocument);

            await context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<DocumentDto>> GetAllGeneralDocuments(MyPortalDbContext context)
        {
            var documents = await GetAllGeneralDocumentsModel(context);

            return documents.Select(Mapper.Map<Document, DocumentDto>);
        }

        public static async Task<IEnumerable<GridDocumentDto>> GetAllGeneralDocumentsDataGrid(MyPortalDbContext context)
        {
            var documents =  await GetAllGeneralDocumentsModel(context);
            return documents.Select(Mapper.Map<Document, GridDocumentDto>);
        }

        public static async Task<IEnumerable<Document>> GetAllGeneralDocumentsModel(MyPortalDbContext context)
        {
            return await context.Documents.Where(x => !x.Deleted && x.IsGeneral).OrderBy(x => x.Description)
                .ToListAsync();
        }

        public static async Task<IEnumerable<DocumentDto>> GetApprovedGeneralDocuments(MyPortalDbContext context)
        {
            var documents = await GetApprovedGeneralDocumentsModel(context);

            return documents.Select(Mapper.Map<Document, DocumentDto>);
        }

        public static async Task<IEnumerable<GridDocumentDto>> GetApprovedGeneralDocumentsDataGrid(MyPortalDbContext context)
        {
            var documents = await GetApprovedGeneralDocumentsModel(context);

            return documents.Select(Mapper.Map<Document, GridDocumentDto>);
        }

        public static async Task<IEnumerable<Document>> GetApprovedGeneralDocumentsModel(MyPortalDbContext context)
        {
            return await context.Documents.Where(x => x.IsGeneral && x.Approved && !x.Deleted).ToListAsync();
        }
        
        public static async Task<DocumentDto> GetDocumentById(int documentId, MyPortalDbContext context)
        {
            var document = await context.Documents.SingleOrDefaultAsync(x => x.Id == documentId);

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