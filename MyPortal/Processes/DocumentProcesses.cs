using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class DocumentProcesses
    {
        public static ProcessResponse<object> CreateDocument(Document document, string userId, MyPortalDbContext context)
        {
            var IsUriValid = Uri.TryCreate(document.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!IsUriValid)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "The URL entered is invalid", null);
            }

            var uploader = new StaffMember();

            var uploaderId = document.UploaderId;

            if (uploaderId == 0)
            {
                
                uploader = context.StaffMembers.SingleOrDefault(x => x.Person.UserId == userId);
                if (uploader == null)
                {
                    return new ProcessResponse<object>(ResponseType.NotFound, "Staff member not found", null);
                }
            }

            if (uploaderId != 0)
            {
                uploader = context.StaffMembers.SingleOrDefault(x => x.Id == uploaderId);
            }

            if (uploader == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Staff member not found", null);
            }

            document.UploaderId = uploader.Id;

            document.IsGeneral = true;

            document.Date = DateTime.Now;

            context.Documents.Add(document);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Document uploaded", null);
        }

        public static ProcessResponse<object> CreatePersonalDocument(PersonDocument document, string uploaderId, MyPortalDbContext context)
        {
            var person = context.Persons.SingleOrDefault(x => x.Id == document.PersonId);

            var uploader = PeopleProcesses.GetStaffFromUserId(uploaderId, context).ResponseObject;

            if (person == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Person not found", null);
            }

            if (uploader == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Uploader not found", null);
            }

            document.Document.IsGeneral = false;

            document.Document.Approved = true;

            document.Document.Date = DateTime.Now;

            document.Document.UploaderId = uploader.Id;

            var isUriValid = Uri.TryCreate(document.Document.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "The URL entered is not valid", null);
            }

            var documentObject = document.Document;

            context.Documents.Add(documentObject);
            context.PersonDocuments.Add(document);

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Document uploaded", null);
        }

        public static ProcessResponse<object> DeleteDocument(int documentId, MyPortalDbContext context)
        {
            var documentInDb = context.Documents.SingleOrDefault(x => x.Id == documentId);

            if (documentInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Document not found", null);
            }

            documentInDb.Deleted = true;
            //context.Documents.Remove(documentInDb); //Delete from database
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Document deleted", null);
        }

        public static ProcessResponse<object> DeletePersonalDocument(int documentId, MyPortalDbContext context)
        {
            var staffDocument = context.PersonDocuments.Single(x => x.Id == documentId);

            if (staffDocument == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Document not found", null);
            }

            var attachedDocument = staffDocument.Document;

            if (attachedDocument == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Document object not found", null);
            }

            context.PersonDocuments.Remove(staffDocument);

            context.Documents.Remove(attachedDocument);

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Document deleted", null);
        }

        public static ProcessResponse<IEnumerable<DocumentDto>> GetAllGeneralDocuments(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<DocumentDto>>(ResponseType.Ok, null,
                GetAllGeneralDocuments_Model(context).ResponseObject
                    .Select(Mapper.Map<Document, DocumentDto>));
        }

        public static ProcessResponse<IEnumerable<GridDocumentDto>> GetAllGeneralDocuments_DataGrid(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridDocumentDto>>(ResponseType.Ok, null,
                GetAllGeneralDocuments_Model(context).ResponseObject
                    .Select(Mapper.Map<Document, GridDocumentDto>));
        }

        public static ProcessResponse<IEnumerable<Document>> GetAllGeneralDocuments_Model(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<Document>>(ResponseType.Ok, null,
                context.Documents.Where(x => !x.Deleted && x.IsGeneral).OrderBy(x => x.Description).ToList());
        }

        public static ProcessResponse<IEnumerable<DocumentDto>> GetApprovedGeneralDocuments(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<DocumentDto>>(ResponseType.Ok, null,
                GetApprovedGeneralDocuments_Model(context).ResponseObject
                    .Select(Mapper.Map<Document, DocumentDto>));
        }

        public static ProcessResponse<IEnumerable<GridDocumentDto>> GetApprovedGeneralDocuments_DataGrid(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridDocumentDto>>(ResponseType.Ok, null,
                GetApprovedGeneralDocuments_Model(context).ResponseObject
                    .Select(Mapper.Map<Document, GridDocumentDto>));
        }

        public static ProcessResponse<IEnumerable<Document>> GetApprovedGeneralDocuments_Model(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<Document>>(ResponseType.Ok, null, context.Documents
                .Where(x => x.IsGeneral && x.Approved && !x.Deleted)
                .ToList());
        }
        public static ProcessResponse<DocumentDto> GetDocumentById(int documentId, MyPortalDbContext context)
        {
            var document = context.Documents
                .Single(x => x.Id == documentId);

            if (document == null)
            {
                return new ProcessResponse<DocumentDto>(ResponseType.NotFound, "Document not found", null);
            }

            return new ProcessResponse<DocumentDto>(ResponseType.Ok, null, Mapper.Map<Document, DocumentDto>(document));
        }
        public static ProcessResponse<PersonDocumentDto> GetPersonalDocumentById(int documentId,
            MyPortalDbContext context)
        {
            var document = context.PersonDocuments.SingleOrDefault(x => x.Id == documentId);

            if (document == null)
            {
                return new ProcessResponse<PersonDocumentDto>(ResponseType.NotFound, "Document not found", null);
            }

            return new ProcessResponse<PersonDocumentDto>(ResponseType.Ok, null,
                Mapper.Map<PersonDocument, PersonDocumentDto>(document));
        }

        public static ProcessResponse<IEnumerable<PersonDocumentDto>> GetPersonalDocuments(int personId,
            MyPortalDbContext context)
        {
            var documents = GetPersonalDocuments_Model(personId, context).ResponseObject
                .Select(Mapper.Map<PersonDocument, PersonDocumentDto>);

            return new ProcessResponse<IEnumerable<PersonDocumentDto>>(ResponseType.Ok, null, documents);
        }

        public static ProcessResponse<IEnumerable<GridPersonDocumentDto>> GetPersonalDocuments_DataGrid(int personId,
            MyPortalDbContext context)
        {
            var documents = GetPersonalDocuments_Model(personId, context).ResponseObject
                .Select(Mapper.Map<PersonDocument, GridPersonDocumentDto>);

            return new ProcessResponse<IEnumerable<GridPersonDocumentDto>>(ResponseType.Ok, null, documents);
        }

        public static ProcessResponse<IEnumerable<PersonDocument>> GetPersonalDocuments_Model(int personId,
                            MyPortalDbContext context)
        {
            var documents = context.PersonDocuments.Where(x => !x.Document.Deleted && x.PersonId == personId).ToList();

            return new ProcessResponse<IEnumerable<PersonDocument>>(ResponseType.Ok, null, documents);
        }
        public static ProcessResponse<object> UpdateDocument(Document document, MyPortalDbContext context)
        {
            var documentInDb = context.Documents.SingleOrDefault(x => x.Id == document.Id);

            if (documentInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Document nor found", null);
            }

            var isUriValid = Uri.TryCreate(document.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "The URL entered is not valid", null);
            }

            documentInDb.Description = document.Description;
            documentInDb.Url = document.Url;
            documentInDb.IsGeneral = true;
            documentInDb.Approved = document.Approved;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Document updated", null);
        }
        public static ProcessResponse<object> UpdatePersonalDocument(PersonDocument document, MyPortalDbContext context)
        {
            var documentInDb = context.PersonDocuments.Single(x => x.Id == document.Id);

            if (documentInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Document not found", null);
            }

            var isUriValid = Uri.TryCreate(document.Document.Url, UriKind.Absolute, out var uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isUriValid)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "The URL entered is not valid", null);
            }

            documentInDb.Document.Description = document.Document.Description;
            documentInDb.Document.Url = document.Document.Url;
            documentInDb.Document.IsGeneral = false;
            documentInDb.Document.Approved = true;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Document updated", null);
        }
    }
}