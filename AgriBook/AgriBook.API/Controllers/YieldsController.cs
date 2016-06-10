using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgriBook.DB.Models;

namespace AgriBook.API.Controllers
{
    public class YieldsController : BaseController
    {
        // GET api/<controller>
        public IEnumerable<Yield> Get()
        {
            return Context.Amounts.OfType<Yield>();
        }

        // GET api/<controller>/5
        public Yield Get(int id)
        {
            return Context.Amounts.OfType<Yield>().FirstOrDefault(y => y.AmountId == id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Yield yield)
        {
            try
            {
                Context.Amounts.Add(new Yield()
                {
                    Name = yield.Name,
                    Quantity = yield.Quantity,
                    MetricUnit = yield.MetricUnit
                });
                Context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, true);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ExceptionMessageHelper.GetErrorMessage(ex)));
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Yield yield)
        {
            try
            {
                var existingRecord = Context.Amounts.OfType<Yield>().FirstOrDefault(y => y.AmountId == id);

                if (existingRecord != null)
                {
                    existingRecord.Name = yield.Name;
                    existingRecord.Quantity = yield.Quantity;
                    existingRecord.MetricUnit = yield.MetricUnit;

                    Context.Amounts.AddOrUpdate(existingRecord);
                    Context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }

                throw new Exception("Yield with id " + id + " not found.");
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, ExceptionMessageHelper.GetErrorMessage(ex)));
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Context.Amounts.Remove(Context.Amounts.OfType<Yield>().FirstOrDefault(y => y.AmountId == id));
                Context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.NoContent, true);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ExceptionMessageHelper.GetErrorMessage(ex)));
            }
        }
    }
}