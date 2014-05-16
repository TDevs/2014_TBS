var VehicleReportController = function ($scope, $http, $location, MemberService) {
    GetVehiclModelYears($http, $scope);
    GetVehicleMakes($http, $scope);

    $scope.Cancel = function () {

        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path("/Member/Vehicle");
    }
};

function GetVehiclModelYears($http, $scope) {

    $http({
        method: 'GET',
        url: '/Member/GetModelYears',
        async: true,
    }).success(function (data, status, headers, config) {
        if (data) {
            $scope.ModelYears = data;
        }
        else {
            alert('Errors');
        }
    })
}

function GetVehicleMakes($http, $scope) {

    $http({
        method: 'GET',
        url: '/VehicleMake/GetVehicleMakes',
        async: true,
    }).success(function (data, status, headers, config) {
        if (data) {
            $scope.VehicleMakes = data;
        }
        else {
            alert('Errors');
        }
    })
}