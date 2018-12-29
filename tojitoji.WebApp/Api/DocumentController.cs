using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using tojitoji.Model.Models;
using tojitoji.Service;
using tojitoji.WebApp.Infrastructure.Core;
using tojitoji.WebApp.Infrastructure.Extensions;
using tojitoji.WebApp.Models;

namespace tojitoji.WebApp.Api
{
    [RoutePrefix("api/document")]
    public class DocumentController : ApiControllerBase
    {
        #region Initialize

        private IDocumentService _documentService;

        public DocumentController(IErrorService errorService, IDocumentService DocumentService)
            : base(errorService)
        {
            this._documentService = DocumentService;
        }

        #endregion Initialize

        [Route("getalldocument")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _documentService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Document>, IEnumerable<DocumentViewModel>>(model);                
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _documentService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Document>, IEnumerable<DocumentViewModel>>(query);

                var paginationSet = new PaginationSet<DocumentViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _documentService.GetById(id);
                var responseData = Mapper.Map<Document, DocumentViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, DocumentViewModel DocumentVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newDocument = new Document();
                    newDocument.UpdateDocument(DocumentVM);
                    _documentService.Add(newDocument);
                    _documentService.SaveChanges();

                    var responseData = Mapper.Map<Document, DocumentViewModel>(newDocument);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, DocumentViewModel DocumentVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbDocument = _documentService.GetById(DocumentVM.ID);

                    dbDocument.UpdateDocument(DocumentVM);

                    _documentService.Update(dbDocument);
                    _documentService.SaveChanges();

                    var responseData = Mapper.Map<Document, DocumentViewModel>(dbDocument);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldDocument = _documentService.Delete(id);
                    _documentService.SaveChanges();

                    var responseData = Mapper.Map<Document, DocumentViewModel>(oldDocument);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}
