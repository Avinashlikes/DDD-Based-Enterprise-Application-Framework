﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Domain.Base.Aggregates;
using DomainServices.Base.CommandDomainServices;
using Infrastructure.Utilities;

namespace RestfulWebAPI.Base
{
    /// <summary>
    /// Go through  
    /// http://www.davepaquette.com/archive/2015/07/19/cancelling-long-running-queries-in-asp-net-mvc-and-web-api.aspx 
    /// to check how to send cancellation request from client side and check
    /// http://www.asp.net/mvc/overview/performance/using-asynchronous-methods-in-aspnet-mvc-4#CancelToken 
    /// to set AsyncTimeOut.Ideally AsyncTimeOut should be set in the DB for the Controler/Action and probably cached and retrieved from the cache
    /// and then retieve it from cache and then use it using some Global Custom Filter. Infact any Filter in general for which we apply some decorative 
    /// attribute should follow this approach.
    /// 
    /// TODO - Need to come up with implementations to send the appropriate response(not only Ok or Badrequest) back to the client
    /// as per the content that needs to be sent or the exception thrown.May need to change Domain Services accordingly.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RestfulAPICommand<TEntity> : BaseDisposableAPIController where TEntity : ICommandAggregateRoot
    {
        protected readonly ICommandDomainServiceAsync<TEntity> _commandDomainServiceAsync;

        public RestfulAPICommand(ICommandDomainServiceAsync<TEntity> commandDomainServiceAsync)
        {
            ContractUtility.Requires<ArgumentNullException>(commandDomainServiceAsync != null, "commandDomainService instance cannot be null");
            _commandDomainServiceAsync = commandDomainServiceAsync;
        }

        [HttpPost]
        public virtual async Task<IHttpActionResult> Post(TEntity item, CancellationToken token = default(CancellationToken))
        {
            bool isSuccess = await _commandDomainServiceAsync.InsertAsync(item, token);
            return isSuccess ? Ok() : BadRequest() as IHttpActionResult;
        }

        [HttpPut]
        public virtual async Task<IHttpActionResult> Put(TEntity item, CancellationToken token = default(CancellationToken))
        {
            bool isSuccess = await _commandDomainServiceAsync.UpdateAsync(item, token);
            return isSuccess ? Ok() : BadRequest() as IHttpActionResult;
        }

        [HttpDelete]
        public virtual async Task<IHttpActionResult> Delete(TEntity item, CancellationToken token = default(CancellationToken))
        {
            bool isSuccess = await _commandDomainServiceAsync.DeleteAsync(item, token);
            return isSuccess ? Ok() : BadRequest() as IHttpActionResult;
        }

        [HttpPost]
        public virtual async Task<IHttpActionResult> PostList(IEnumerable<TEntity> items, CancellationToken token = default(CancellationToken))
        {
            bool isSuccess = await _commandDomainServiceAsync.InsertAsync(items, token);
            return isSuccess ? Ok() : BadRequest() as IHttpActionResult;
        }

        [HttpPut]
        public virtual async Task<IHttpActionResult> PutList(IEnumerable<TEntity> items, CancellationToken token = default(CancellationToken))
        {
            bool isSuccess = await _commandDomainServiceAsync.UpdateAsync(items, token);
            return isSuccess ? Ok() : BadRequest() as IHttpActionResult;
        }

        [HttpDelete]
        public virtual async Task<IHttpActionResult> DeleteList(IEnumerable<TEntity> items, CancellationToken token = default(CancellationToken))
        {
            bool isSuccess = await _commandDomainServiceAsync.DeleteAsync(items, token);
            return isSuccess ? Ok() : BadRequest() as IHttpActionResult;
        }

        [HttpPost]
        public virtual async Task<IHttpActionResult> PostBulkList(IEnumerable<TEntity> items, CancellationToken token = default(CancellationToken))
        {
            bool isSuccess = await _commandDomainServiceAsync.BulkInsertAsync(items, token);
            return isSuccess ? Ok() : BadRequest() as IHttpActionResult;
        }

        [HttpPut]
        public virtual async Task<IHttpActionResult> PutBulkList(IEnumerable<TEntity> items, CancellationToken token = default(CancellationToken))
        {
            bool isSuccess = await _commandDomainServiceAsync.BulkUpdateAsync(items, token);
            return isSuccess ? Ok() : BadRequest() as IHttpActionResult;
        }

        [HttpDelete]
        public virtual async Task<IHttpActionResult> DeleteBulkList(IEnumerable<TEntity> items, CancellationToken token = default(CancellationToken))
        {
            bool isSuccess = await _commandDomainServiceAsync.DeleteAsync(items, token);
            return isSuccess ? Ok() : BadRequest() as IHttpActionResult;
        }

        #region Free Disposable Members

        protected override void FreeManagedResources()
        {
            base.FreeManagedResources();
            _commandDomainServiceAsync.Dispose();
        }

        #endregion
    }
}