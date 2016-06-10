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
    public class MetricUnitsController : BaseController
    {
        // GET api/<controller>
        public IEnumerable<MetricUnit> Get()
        {
            return Context.MetricUnits.ToList();
        }

        // GET api/<controller>/5
        public MetricUnit Get(int id)
        {
            return Context.MetricUnits.FirstOrDefault(mu =>mu.Id == id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]MetricUnit metricUnit)
        {
            try
            {
                Context.MetricUnits.Add(new MetricUnit()
                {
                    Name = metricUnit.Name
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
        public HttpResponseMessage Put(int id, [FromBody]MetricUnit metricUnit)
        {
            try
            {
                var existingRecord = Context.MetricUnits.FirstOrDefault(mu => mu.Id == id);

                if (existingRecord != null)
                {
                    existingRecord.Name = metricUnit.Name;

                    Context.MetricUnits.AddOrUpdate(existingRecord);
                    Context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }

                throw new Exception("Metric unit with id " + id + " not found.");
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Conflict, ExceptionMessageHelper.GetErrorMessage(ex)));
            }
        }

        [HttpGet]
        public IEnumerable<MetricUnit> GetByType(int type)
        {
            return Context.MetricUnits.Where(mu => mu.Type == type);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Context.MetricUnits.Remove(Context.MetricUnits.FirstOrDefault(mu => mu.Id == id));
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