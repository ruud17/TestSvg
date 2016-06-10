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
    public class FertilizersController : BaseController
    {
        // GET api/<controller>
        public IEnumerable<Fertilizer> Get()
        {
            return Context.Fertilizers.ToList();
        }

        // GET api/<controller>/5
        public Fertilizer Get(int id)
        {
            return Context.Fertilizers.FirstOrDefault(f => f.Id == id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Fertilizer fertilizer)
        {
            try
            {
                Context.Fertilizers.Add(new Fertilizer()
                {
                    Name = fertilizer.Name,
                    Description = fertilizer.Description
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
        public HttpResponseMessage Put(int id, [FromBody]Fertilizer fertilizer)
        {
            try
            {
                var existingRecord = Context.Fertilizers.FirstOrDefault(f => f.Id == id);
                if (existingRecord != null)
                {
                    existingRecord.Name = fertilizer.Name;
                    existingRecord.Description = fertilizer.Description;
                    Context.Fertilizers.AddOrUpdate(existingRecord);
                    Context.SaveChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK, true);
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
                Context.Fertilizers.Remove(Context.Fertilizers.FirstOrDefault(f => f.Id == id));
                Context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.NoContent, true);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, ExceptionMessageHelper.GetErrorMessage(ex)));
            }
        }
    }
}