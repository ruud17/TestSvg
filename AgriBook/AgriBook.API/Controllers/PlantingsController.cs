using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgriBook.API.Helpers;
using AgriBook.DB.Models;

namespace AgriBook.API.Controllers
{
    public class PlantingsController : BaseController
    {
        // GET api/<controller>
        public IEnumerable<Planting> Get()
        {
            return Context.Plantings.ToList();
        }

        // GET api/<controller>/5
        public Planting Get(int id)
        {
            return Context.Plantings.FirstOrDefault(p => p.Id == id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Planting planting)
        {
            try
            {
                var parcelArea = planting.Parcel.ParcelAreas.FirstOrDefault();
                if (parcelArea != null && parcelArea.MetricUnit == null)
                {
                    parcelArea.MetricUnit = Context.MetricUnits.FirstOrDefault(mu =>
                        mu.Id == Context.Parcels.FirstOrDefault(p =>
                            p.Id == planting.Parcel.Id).ParcelAreas.FirstOrDefault().MetricUnit.Id);

                    planting.Parcel.ParcelAreas = new List<ParcelArea>()
                    {
                        parcelArea
                    };
                }

                if (planting.PlantingCrops != null && planting.PlantingCrops.Count > 0)
                {
                    foreach (var plantingCrop in planting.PlantingCrops)
                    {
                        plantingCrop.Crop = Context.Crops.FirstOrDefault(c => c.Id == plantingCrop.Crop.Id);
                        plantingCrop.MetricUnit = Context.MetricUnits.FirstOrDefault(mu => mu.Id == plantingCrop.MetricUnit.Id);
                    }
                }

                if (planting.PlantingFertilizers != null && planting.PlantingFertilizers.Count > 0)
                {
                    foreach (var plantingFertilizer in planting.PlantingFertilizers)
                    {
                        plantingFertilizer.Fertilizer = Context.Fertilizers.FirstOrDefault(f => f.Id == plantingFertilizer.Fertilizer.Id);
                        plantingFertilizer.MetricUnit = Context.MetricUnits.FirstOrDefault(mu => mu.Id == plantingFertilizer.MetricUnit.Id);
                    }
                }

                if (planting.Yields != null && planting.Yields.Count > 0)
                {
                    foreach (var yield in planting.Yields)
                    {
                        yield.MetricUnit = Context.MetricUnits.FirstOrDefault(mu => mu.Id == yield.MetricUnit.Id);
                    }
                }

                var tempParcel = Context.Parcels.FirstOrDefault(p => p.Id == planting.Parcel.Id);

                if (tempParcel != null)
                {
                    tempParcel.GruntId = planting.Parcel.GruntId;
                    tempParcel.Name = planting.Parcel.Name;
                    tempParcel.OwnerName = planting.Parcel.OwnerName;

                    var tempParcelAreas = tempParcel.ParcelAreas.ToList();
                    var tempPlantingParcelAreas = planting.Parcel.ParcelAreas.ToList();
                    for (int i = 0; i < tempParcelAreas.Count; i++)
                    {
                        var muId = tempPlantingParcelAreas[i].MetricUnit.Id;
                        tempParcelAreas[i].MetricUnit = Context.MetricUnits.FirstOrDefault(mu => mu.Id == muId);
                        tempParcelAreas[i].Quantity = tempPlantingParcelAreas[i].Quantity;
                    }
                    tempParcel.ParcelAreas = tempParcelAreas;

                    planting.Parcel = tempParcel;
                }

                Context.Plantings.AddOrUpdate(planting);
                Context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, true);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ExceptionMessageHelper.GetErrorMessage(ex)));
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]Planting planting)
        {
            try
            {
                var existingRecord = Context.Plantings.FirstOrDefault(p => p.Id == id);

                Context.Entry(existingRecord).CurrentValues.SetValues(planting);

                if (existingRecord != null)
                {
                    existingRecord.Season = planting.Season;

                    #region Update PlantingCrops

                    var plantingCropsToRemove = existingRecord.PlantingCrops.Where(existingPlantingCrop => planting.PlantingCrops.All(pc => !ObjectComparisonHelper.AreEqual(pc, existingPlantingCrop))).ToList();

                    if (plantingCropsToRemove.Count > 0)
                    {
                        foreach (var plantingCropToRemove in plantingCropsToRemove)
                        {
                            existingRecord.PlantingCrops.Remove(plantingCropToRemove);
                        }
                    }

                    foreach (var plantingCrop in planting.PlantingCrops)
                    {
                        if (existingRecord.PlantingCrops.All(pc => !ObjectComparisonHelper.AreEqual(pc, plantingCrop)))
                        {
                            plantingCrop.Crop = Context.Crops.FirstOrDefault(c => c.Id == plantingCrop.Crop.Id);
                            plantingCrop.MetricUnit = Context.MetricUnits.FirstOrDefault(mu => mu.Id == plantingCrop.MetricUnit.Id);
                            existingRecord.PlantingCrops.Add(plantingCrop);
                        }
                    }
                    #endregion

                    #region Update PlantingFertilizers

                    var plantingFertilizersToRemove = existingRecord.PlantingFertilizers.Where(existingPlantingFertilizer => planting.PlantingFertilizers.All(pf => !ObjectComparisonHelper.AreEqual(pf, existingPlantingFertilizer))).ToList();

                    if (plantingFertilizersToRemove.Count > 0)
                    {
                        foreach (var plantingFertilizerToRemove in plantingFertilizersToRemove)
                        {
                            existingRecord.PlantingFertilizers.Remove(plantingFertilizerToRemove);
                        }
                    }

                    foreach (var plantingFertilizer in planting.PlantingFertilizers)
                    {
                        if (existingRecord.PlantingFertilizers.All(pf => !ObjectComparisonHelper.AreEqual(pf, plantingFertilizer)))
                        {
                            plantingFertilizer.Fertilizer = Context.Fertilizers.FirstOrDefault(c => c.Id == plantingFertilizer.Fertilizer.Id);
                            plantingFertilizer.MetricUnit = Context.MetricUnits.FirstOrDefault(mu => mu.Id == plantingFertilizer.MetricUnit.Id);
                            existingRecord.PlantingFertilizers.Add(plantingFertilizer);
                        }
                    }
                    #endregion

                    #region Update Yields

                    var yieldsToRemove = existingRecord.Yields.Where(existingYield => planting.Yields.All(y => !ObjectComparisonHelper.AreEqual(y, existingYield))).ToList();

                    if (yieldsToRemove.Count > 0)
                    {
                        foreach (var yieldToRemove in yieldsToRemove)
                        {
                            existingRecord.Yields.Remove(yieldToRemove);
                        }
                    }

                    foreach (var yield in planting.Yields)
                    {
                        if (existingRecord.Yields.All(y => !ObjectComparisonHelper.AreEqual(y, yield)))
                        {
                            yield.MetricUnit = Context.MetricUnits.FirstOrDefault(mu => mu.Id == yield.MetricUnit.Id);
                            existingRecord.Yields.Add(yield);
                        }
                    }
                    #endregion

                    var tempParcel = Context.Parcels.FirstOrDefault(p => p.Id == planting.Parcel.Id);

                    if (tempParcel != null)
                    {
                        tempParcel.GruntId = planting.Parcel.GruntId;
                        tempParcel.Name = planting.Parcel.Name;
                        tempParcel.OwnerName = planting.Parcel.OwnerName;

                        var tempParcelAreas = tempParcel.ParcelAreas.ToList();
                        var tempPlantingParcelAreas = planting.Parcel.ParcelAreas.ToList();
                        for (int i = 0; i < tempParcelAreas.Count; i++)
                        {
                            var muId = tempPlantingParcelAreas[i].MetricUnit.Id;
                            tempParcelAreas[i].MetricUnit = Context.MetricUnits.FirstOrDefault(mu => mu.Id == muId);
                            tempParcelAreas[i].Quantity = tempPlantingParcelAreas[i].Quantity;
                        }
                        tempParcel.ParcelAreas = tempParcelAreas;

                        existingRecord.Parcel = tempParcel;
                    }

                    Context.Plantings.AddOrUpdate(existingRecord);
                    Context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, true);
                }

                throw new Exception("Planting with id " + id + " not found.");
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
                Context.Plantings.Remove(Context.Plantings.FirstOrDefault(p => p.Id == id));
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