using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgriBook.DB.Models;

namespace AgriBook.API.Helpers
{
    public class ObjectComparisonHelper
    {
        public static bool AreEqual(PlantingCrop plantingCropA, PlantingCrop plantingCropB)
        {
            return plantingCropA.AmountId == plantingCropB.AmountId 
                && plantingCropA.Quantity == plantingCropB.Quantity
                && AreEqual(plantingCropA.MetricUnit, plantingCropB.MetricUnit) 
                && AreEqual(plantingCropA.Crop, plantingCropB.Crop);
        }

        public static bool AreEqual(PlantingFertilizer plantingFertilizerA, PlantingFertilizer plantingFertilizerB)
        {
            return plantingFertilizerA.AmountId == plantingFertilizerB.AmountId
                && plantingFertilizerA.Quantity == plantingFertilizerB.Quantity
                && AreEqual(plantingFertilizerA.MetricUnit, plantingFertilizerB.MetricUnit)
                && AreEqual(plantingFertilizerA.Fertilizer, plantingFertilizerB.Fertilizer);
        }

        public static bool AreEqual(Yield yieldA, Yield yieldB)
        {
            return yieldA.AmountId == yieldB.AmountId && yieldA.Name == yieldB.Name &&
                   yieldA.Quantity == yieldB.Quantity && AreEqual(yieldA.MetricUnit, yieldB.MetricUnit);
        }

        public static bool AreEqual(Crop cropA, Crop cropB)
        {
            return cropA.Id == cropB.Id && cropA.Color == cropB.Color && cropA.Name == cropB.Name &&
                   cropA.Description == cropB.Description;
        }

        public static bool AreEqual(Fertilizer fertilizerA, Fertilizer fertilizerB)
        {
            return fertilizerA.Id == fertilizerB.Id && fertilizerA.Name == fertilizerB.Name &&
                   fertilizerA.Description == fertilizerB.Description;
        }

        public static bool AreEqual(MetricUnit metricUnitA, MetricUnit metricUnitB)
        {
            return metricUnitA.Id == metricUnitB.Id && metricUnitA.Name == metricUnitB.Name &&
                   metricUnitA.Type == metricUnitB.Type;
        }
    }
}