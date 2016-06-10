var validationHelper = validationHelper || {};

/*
* Currently, WebAPI returns only one planting per parcel.
* Planting is filtered by the selected year. All other plantings
* are removed before returning the parcel.
* Because of that, it is safe and sufficient to check only
* the first member of parcel.Plantings array.
* If ParcelsController.GetByYear WebAPI method gets changed, 
* this function should be updated as well.
*/
validationHelper.isParcelPlanted = function (parcel) {
    if (parcel.Plantings && parcel.Plantings.length > 0
        && parcel.Plantings[0].PlantingCrops && parcel.Plantings[0].PlantingCrops.length > 0) {
        return true;
    }
    return false;
};

validationHelper.isCropPlanted = function (parcel) {
    if (validationHelper.isParcelPlanted(parcel) && parcel.Plantings[0].PlantingCrops[0].Crop) {
        return true;
    }
    return false;
};

validationHelper.isCropMetricUnitSelected = function(parcel) {
    if (validationHelper.isParcelPlanted(parcel) && parcel.Plantings[0].PlantingCrops[0].MetricUnit) {
        return true;
    }
    return false;
};

validationHelper.hasColor = function (parcel) {
    return validationHelper.isCropPlanted(parcel) && parcel.Plantings[0].PlantingCrops[0].Crop.Color;
};

validationHelper.isParcelFertilized = function (parcel) {
    if (parcel.Plantings && parcel.Plantings.length > 0
        && parcel.Plantings[0].PlantingFertilizers && parcel.Plantings[0].PlantingFertilizers.length > 0) {
        return true;
    }
    return false;
};

validationHelper.areAllFertilzersSelected = function (parcel) {
    for (var i = 0; i < parcel.Plantings[0].PlantingFertilizers.length; i++) {
        if (!parcel.Plantings[0].PlantingFertilizers[i].Fertilizer) {
            return false;
        }
    }

    return true;
};

validationHelper.areAllFertilizersPresent = function (parcel) {
    return validationHelper.isParcelFertilized(parcel) && validationHelper.areAllFertilzersSelected(parcel);
};

validationHelper.doesParcelHaveYields = function (parcel) {
    if (parcel.Plantings && parcel.Plantings.length > 0
        && parcel.Plantings[0].Yields && parcel.Plantings[0].Yields.length > 0) {
        return true;
    }
    return false;
};

validationHelper.areAllYieldsSelected = function (parcel) {
    for (var i = 0; i < parcel.Plantings[0].Yields.length; i++) {
        if (!parcel.Plantings[0].Yields[i].Name || !parcel.Plantings[0].Yields[i].Quantity || !parcel.Plantings[0].Yields[i].MetricUnit) {
            return false;
        }
    }

    return true;
};

validationHelper.areAllYieldsPresent = function (parcel) {
    return validationHelper.doesParcelHaveYields(parcel) && validationHelper.areAllYieldsSelected(parcel);
};


validationHelper.findItemById = function (id, collection) {
    return $.grep(collection, function (item) {
        return item.Id === id;
    })[0];
};