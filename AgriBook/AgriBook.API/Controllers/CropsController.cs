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
    public class CropsController : BaseController
    {
        // GET api/<controller>
        public IEnumerable<Crop> Get()
        {
            return Context.Crops.ToList();
        }

        // GET api/<controller>/5
        public Crop Get(int id)
        {
            return Context.Crops.FirstOrDefault(c => c.Id == id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Crop crop)
        {
            try
            {
                Context.Crops.Add(new Crop()
                {
                    Name = crop.Name,
                    Color = crop.Color,
                    Description = crop.Description,
                    ImageUrl = crop.ImageUrl,
                    ImageName = crop.ImageName
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
        public HttpResponseMessage Put(int id, [FromBody]Crop crop)
        {
            try
            {
                var existingRecord = Context.Crops.FirstOrDefault(c => c.Id == id);
                if (existingRecord != null)
                {
                    existingRecord.Name = crop.Name;
                    existingRecord.Color = crop.Color;
                    existingRecord.Description = crop.Description;
                    existingRecord.ImageUrl = crop.ImageUrl;
                    existingRecord.ImageName = crop.ImageName;
                    Context.Crops.AddOrUpdate(existingRecord);
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
                Context.Crops.Remove(Context.Crops.FirstOrDefault(c => c.Id == id));
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