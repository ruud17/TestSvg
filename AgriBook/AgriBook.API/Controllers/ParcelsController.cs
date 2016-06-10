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
    public class ParcelsController : BaseController
    {
        // GET api/<controller>
        [System.Web.Http.HttpGet]
        public IEnumerable<Parcel> GetByYear(int plantingYear)
        {
            ActionContext.Request.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});

            List<Parcel> result = null;

            if (plantingYear != 0)
            {
                var dbQuery = from parcel in Context.Parcels
                              select new
                              {
                                  parcel,
                                  plantings = from planting in parcel.Plantings
                                              where planting.Season.Year == plantingYear || planting.Season == DateTime.MinValue
                                              select planting
                              };

                result = dbQuery.AsEnumerable().Select(dbq => dbq.parcel).ToList();
                foreach (var parcel in result)
                {
                    var tempList = parcel.Plantings.ToList();
                    tempList.RemoveAll(pl => pl.Season != DateTime.MinValue && pl.Season.ToString("yyyy") != plantingYear.ToString());
                    parcel.Plantings = tempList;
                }
            }
            else
            {
                result = Context.Parcels.ToList();
            }

            return result;
        }

        // GET api/<controller>/5
        [System.Web.Http.HttpGet]
        public Parcel Get(int id, bool includePlantings)
        {
            return includePlantings ? Context.Parcels.Include("Plantings").Include("ParcelAreas").FirstOrDefault(p => p.Id == id) : Context.Parcels.FirstOrDefault(p => p.Id == id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Parcel parcel)
        {
            try
            {
                foreach (var parcelArea in parcel.ParcelAreas)
                {
                    parcelArea.MetricUnit = Context.MetricUnits.FirstOrDefault(mu => mu.Id == parcelArea.MetricUnit.Id);
                }
                Context.Parcels.Add(parcel);

                Context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, true);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ExceptionMessageHelper.GetErrorMessage(ex)));
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Parcel parcel)
        {
            try
            {
                var existingRecord = Context.Parcels.FirstOrDefault(p => p.Id == id);
                if (existingRecord != null)
                {
                    if (parcel.ParcelAreas != null && parcel.ParcelAreas.Count > 0)
                    {
                        existingRecord.ParcelAreas = new List<ParcelArea>();

                        foreach (var parcelArea in parcel.ParcelAreas)
                        {
                            parcelArea.MetricUnit = Context.MetricUnits.FirstOrDefault(mu => mu.Id == parcelArea.MetricUnit.Id);
                            existingRecord.ParcelAreas.Add(parcelArea);
                        }
                    }

                    existingRecord.GruntId = parcel.GruntId;
                    existingRecord.Name = parcel.Name;
                    existingRecord.OwnerName = parcel.OwnerName;
                    existingRecord.Points = parcel.Points;

                    Context.Parcels.AddOrUpdate(existingRecord);
                    Context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }

                throw new Exception("Parcel with id " + id + " not found.");
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
                Context.Parcels.Remove(Context.Parcels.FirstOrDefault(p => p.Id == id));
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