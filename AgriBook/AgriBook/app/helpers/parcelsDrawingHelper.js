var polygonToRaphaelPath = function (points) {
    if (points.charAt(0) == 'M') {
        return points;
    }
    return "M" + points.substr(0, points.length - 5) + "Z";
};