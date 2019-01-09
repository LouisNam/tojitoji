using AutoMapper;
using System;
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
    [RoutePrefix("api/purchaseorderdetailreturn")]
    public class PurchaseOrderDetailReturnController : ApiControllerBase
    {
        #region Initialize

        private IPurchaseOrderDetailReturnService _purchaseOrderDetailReturnService;

        public PurchaseOrderDetailReturnController(IErrorService errorService, IPurchaseOrderDetailReturnService purchaseOrderDetailReturnService)
            : base(errorService)
        {
            this._purchaseOrderDetailReturnService = purchaseOrderDetailReturnService;
        }

        #endregion Initialize

        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _purchaseOrderDetailReturnService.GetById(id);
                var responseData = Mapper.Map<PurchaseOrderDetailReturn, PurchaseOrderDetailReturnViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, PurchaseOrderDetailReturnViewModel purchaseOrderDetailVM)
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
                    var dbPurchaseOrderDetail = _purchaseOrderDetailReturnService.GetById(purchaseOrderDetailVM.ID);

                    dbPurchaseOrderDetail.UpdatePurchaseOrderDetailReturn(purchaseOrderDetailVM);

                    _purchaseOrderDetailReturnService.Update(dbPurchaseOrderDetail);
                    _purchaseOrderDetailReturnService.SaveChanges();

                    var responseData = Mapper.Map<PurchaseOrderDetailReturn, PurchaseOrderDetailReturnViewModel>(dbPurchaseOrderDetail);
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
                    var oldPurchaseOrderDetail = _purchaseOrderDetailReturnService.Delete(id);
                    _purchaseOrderDetailReturnService.SaveChanges();

                    var responseData = Mapper.Map<PurchaseOrderDetailReturn, PurchaseOrderDetailReturnViewModel>(oldPurchaseOrderDetail);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, PurchaseOrderDetailReturnViewModel purchaseOrderDetailVM)
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
                    var newPurchaseOrderDetailReturn = new PurchaseOrderDetailReturn();
                    newPurchaseOrderDetailReturn.UpdatePurchaseOrderDetailReturn(purchaseOrderDetailVM);
                    newPurchaseOrderDetailReturn.CreatedDate = DateTime.Now;
                    
                    _purchaseOrderDetailReturnService.Add(newPurchaseOrderDetailReturn);
                    _purchaseOrderDetailReturnService.SaveChanges();

                    var responseData = Mapper.Map<PurchaseOrderDetailReturn, PurchaseOrderDetailReturnViewModel>(newPurchaseOrderDetailReturn);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}