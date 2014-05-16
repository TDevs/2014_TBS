var VehicleModelListController = function ($scope, $http, $modal, VehicleModelService) {
    //config paging table
    $scope.aoColumns = [
            { "sTitle": "Vehicle Model Name" },
            { "sTitle": "Make" },
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "  <a href=\"\"  onclick=\"angular.element(this).scope().EditVehicleModel('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                    "<a href=\"\"  onclick=\"angular.element(this).scope().DeleteVehicleModel('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-ban\"></i>"
                            + "</a>";

                    return action;
                }
            }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "Name", "aTargets": [0] },
                         { "mDataProp": "VehicleMakeName", "aTargets": [1] },
                        { "mDataProp": "Id", "aTargets": [2] }
    ];
    //end config

    VehicleModelService.VehicleModelList($http, $scope);
    VehicleModelService.VehicleMakeList($http, $scope);
    $scope.EditVehicleModel = function (id) {
        var u = $.grep($scope.VehicleModelList, function (e) { return e.Id == id; });
        var vehicleModelItem = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/VehicleModel/Edit',
            controller: 'EditVehicleModelController',
            resolve: {
                vehicleModelItem: function () {
                    return angular.copy(vehicleModelItem);
                },
                vehicleMakeList: function () {
                    return $scope.VehicleMakeList
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            VehicleModelService.VehicleModelList($http, $scope);

        });
    };

    $scope.AddVehicleModel = function () {
        var vehicleModelItem = {
            'Name': '',
            'VehicleMakeId': ''
        };

        VehicleModelService.VehicleMakeList($http, $scope);
        var modalInstance = $modal.open({
            templateUrl: '/VehicleModel/Create',
            controller: 'AddVehicleModelController',
            resolve: {
                vehicleModelItem: function () {
                    return vehicleModelItem;
                },
                vehicleMakeList: function () {
                    return $scope.VehicleMakeList;
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            VehicleModelService.VehicleModelList($http, $scope);
        });
    };

    $scope.DeleteVehicleModel = function (id) {
        var u = $.grep($scope.VehicleModelList, function (e) { return e.Id == id; });
        var vehicleModelItem = u[0];

        if (confirm("Are you sure you want to delete this item?")) {
            $http({
                method: 'POST',
                url: '/VehicleModel/DeleteVehicleModel',
                data: vehicleModelItem,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.VehicleModelList = jQuery.grep($scope.VehicleModelList, function (value) {
                        return value.Id != vehicleModelItem.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};

var listVehicleMakeController = function ($scope, $http) {
    $http({
        method: 'GET',
        url: '/VehicleModel/GetVehicleMakes',
        async: false,
    }).success(function (data, status, headers, config) {
        $scope.VehicleMakeList = data;
    });
}

