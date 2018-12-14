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
    [RoutePrefix("api/inventorytransaction")]
    public class InventoryTransactionController : ApiControllerBase
    {
        #region Initialize

        private IInventoryTransactionService _inventoryTransactionService;

        public InventoryTransactionController(IErrorService errorService, IInventoryTransactionService inventoryTransactionService)
            : base(errorService)
        {
            this._inventoryTransactionService = inventoryTransactionService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _inventoryTransactionService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<InventoryTransaction>, IEnumerable<InventoryTransactionViewModel>>(query);

                var paginationSet = new PaginationSet<InventoryTransactionViewModel>()
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
                var model = _inventoryTransactionService.GetById(id);
                var responseData = Mapper.Map<InventoryTransaction, InventoryTransactionViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, InventoryTransactionViewModel inventoryTransactionVM)
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
                    var newInventoryTransaction = new InventoryTransaction();
                    newInventoryTransaction.UpdateInventoryTransaction(inventoryTransactionVM);
                    _inventoryTransactionService.Add(newInventoryTransaction);
                    _inventoryTransactionService.SaveChanges();

                    var responseData = Mapper.Map<InventoryTransaction, InventoryTransactionViewModel>(newInventoryTransaction);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, InventoryTransactionViewModel inventoryTransactionVM)
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
                    var dbInventoryTransaction = _inventoryTransactionService.GetById(inventoryTransactionVM.ID);

                    dbInventoryTransaction.UpdateInventoryTransaction(inventoryTransactionVM);

                    _inventoryTransactionService.Update(dbInventoryTransaction);
                    _inventoryTransactionService.SaveChanges();

                    var responseData = Mapper.Map<InventoryTransaction, InventoryTransactionViewModel>(dbInventoryTransaction);
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
                    var oldInventoryTransaction = _inventoryTransactionService.Delete(id);
                    _inventoryTransactionService.SaveChanges();

                    var responseData = Mapper.Map<InventoryTransaction, InventoryTransactionViewModel>(oldInventoryTransaction);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}