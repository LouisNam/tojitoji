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
using tojitoji.WebApp.Models;

namespace tojitoji.WebApp.Api
{
    [RoutePrefix("api/documenttype")]
    public class DocumentTypeController : ApiControllerBase
    {
        #region Initialize

        private IDocumentTypeService _documentTypeService;

        public DocumentTypeController(IErrorService errorService, IDocumentTypeService documentTypeService)
            : base(errorService)
        {
            this._documentTypeService = documentTypeService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _documentTypeService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<DocumentType>, IEnumerable<DocumentTypeViewModel>>(query);

                var paginationSet = new PaginationSet<DocumentTypeViewModel>()
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

        //[Route("getbyid/{id}")]
        //[HttpGet]
        //public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        var model = _documentTypeService.GetDetail(id);
        //        var responseData = Mapper.Map<DocumentType, DocumentTypeViewModel>(model);
        //        var response = request.CreateResponse(HttpStatusCode.OK, responseData);
        //        return response;
        //    });
        //}

        //[Route("create")]
        //[HttpPost]
        //public HttpResponseMessage Create(HttpRequestMessage request, DocumentTypeViewModel DocumentTypeVM)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (!ModelState.IsValid)
        //        {
        //            response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var newDocumentType = new DocumentType();
        //            newDocumentType.UpdateDocumentType(DocumentTypeVM);
        //            _documentTypeService.Add(newDocumentType);
        //            _documentTypeService.SaveChanges();

        //            var responseData = Mapper.Map<DocumentType, DocumentTypeViewModel>(newDocumentType);
        //            response = request.CreateResponse(HttpStatusCode.Created, responseData);
        //        }

        //        return response;
        //    });
        //}

        //[Route("update")]
        //[HttpPut]
        //public HttpResponseMessage Update(HttpRequestMessage request, DocumentTypeViewModel DocumentTypeVM)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (!ModelState.IsValid)
        //        {
        //            response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var dbDocumentType = _documentTypeService.GetById(DocumentTypeVM.ID);

        //            dbDocumentType.UpdateDocumentType(DocumentTypeVM);

        //            _documentTypeService.Update(dbDocumentType);
        //            _documentTypeService.SaveChanges();

        //            var responseData = Mapper.Map<DocumentType, DocumentTypeViewModel>(dbDocumentType);
        //            response = request.CreateResponse(HttpStatusCode.Created, responseData);
        //        }

        //        return response;
        //    });
        //}

        //[Route("delete")]
        //[HttpDelete]
        //public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        if (!ModelState.IsValid)
        //        {
        //            response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //        }
        //        else
        //        {
        //            var oldDocumentType = _documentTypeService.Delete(id);
        //            _documentTypeService.SaveChanges();

        //            var responseData = Mapper.Map<DocumentType, DocumentTypeViewModel>(oldDocumentType);
        //            response = request.CreateResponse(HttpStatusCode.Created, responseData);
        //        }

        //        return response;
        //    });
        //}
    }
}
