var config = config || {};

var basicRoute = 'http://' + window.location.hostname + ':52896/';

var loginUrl = basicRoute + 'token';
var parcelsUrl = basicRoute + 'api/Parcels/';
var cropsUrl = basicRoute + 'api/Crops/';
var fertilizersUrl = basicRoute + 'api/Fertilizers/';
var plantingsUrl = basicRoute + 'api/Plantings/';
var metricUnitsUrl = basicRoute + 'api/MetricUnits/';

var plantingDatepickerOptions = {
    datepickerMode: "'year'",
    minMode: "year",
    showWeeks: "false"
};

var season = {
    years: [
        2016,
        2015,
        2014,
        2013,
        2012
    ]
};

var cloudinaryCloudName = 'laktal-stipanjici';
var cloudinaryUploadPreset = 'vmx6qdm9';
var cloudinaryUrl = 'https://api.cloudinary.com/v1_1/' + cloudinaryCloudName + '/image/upload';

var defaultCropColor = "#e39f4e";

var messages = {
    BOS: {
        SUCCESS_SAVING_CROP: 'Novi tip sjemena uspjesno spasen.',
        ERROR_LOADING_CROP: 'Greska pri ucitavanju sjemena.',
        ERROR_SAVING_CROP: 'Greska pri spasavanju sjemena.',
        ERROR_UPDATING_CROP: 'Greska pri spasavanju izmjena sjemena.',
        ERROR_UPLOADING_IMAGE: 'Greska pri spasavanju slike.',
        ERROR_LOADING_FERTILIZER: 'Greska pri ucitavanju gnjojiva.',
        ERROR_SAVING_FERTILIZER: 'Greska pri spasavanju gnjojiva.',
        ERROR_SAVING_PLANTING: 'Greska pri spasavanju sjetve.',
        ERROR_LOADING_PARCELS: 'Greska pri ucitavanju parcela.',
        ERROR_LOADING_PARCEL: 'Greska pri ucitavanju parcele.',
        ERROR_LOADING_METRIC_UNITS: 'Greska pri ucitavanju mjernih jedinica.',
        CROP: 'Sjeme ',
        FERTILIZER:'Gnojivo',
        METRIC_UNIT: 'Mjerna jedinica ',
        EDIT_SUCCESS_I: ' uspjesno uredjeno.',
        DELETE_SUCCESS_I: ' uspjesno obrisano.',
        EDIT_SUCCESS_F: ' uspjesno uredjena.',
        DELETE_SUCCESS_F: ' uspjesno obrisana.',
        SUCCESS_SAVING_FERTILIZER: 'Novi tip gnjojiva uspjesno spasen.',
        SUCCESS_SAVING_METRIC_UNIT: 'Novi tip mjerne jedinice uspjesno spasen.',
        SUCCESS_SAVING_PLANTING: 'Sjetva je uspjesno spasena.',
        SUCCESS_UPDATING_PLANTING: 'Sjetva je uspjesno uredjena.'
    }
};