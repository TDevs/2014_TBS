var VehicleFeatureListController = function ($scope, $http, $modal, VehicleFeatureService) {
    //config paging table
    $scope.aoColumns = [
            { "sTitle": "Feature" },
            { "sTitle": "Description" },
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "  <a href=\"\"  onclick=\"angular.element(this).scope().EditVehicleFeature('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                    "<a href=\"\"  onclick=\"angular.element(this).scope().DeleteVehicleFeature('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-ban\"></i>"
                            + "</a>";

                    return action;
                }
            }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "Name", "aTargets": [0] },
                        { "mDataProp": "Description", "aTargets": [1] },
                        { "mDataProp": "Id", "aTargets": [2] }
    ];
    //end config
    VehicleFeatureService.VehicleFeatureList($http, $scope);

    $scope.EditVehicleFeature = function (id) {
        var u = $.grep($scope.VehicleFeatureList, function (e) { return e.Id == id; });
        var vehicleFeatureItem = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/VehicleFeature/Edit',
            controller: 'EditVehicleFeatureController',
            resolve: {
                vehicleFeatureItem: function () {
                    return angular.copy(vehicleFeatureItem);
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            VehicleFeatureService.VehicleFeatureList($http, $scope);

        });
    };

    $scope.AddVehicleFeature = function () {
        var vehicleFeatureItem = {
            'Name': '',
            'Description': ''
        };
        var modalInstance = $modal.open({
            templateUrl: '/VehicleFeature/Create',
            controller: 'AddVehicleFeatureController',
            resolve: {
                vehicleFeatureItem: function () {
                    return angular.copy(vehicleFeatureItem);
                }
            }
        });

        //modal opened
        modalInstance.opened.then(function () {
            //ComponentsFormTools.init();
        });
        //reload list
        modalInstance.result.then(function () {
            VehicleFeatureService.VehicleFeatureList($http, $scope);
        });
    };

    $scope.DeleteVehicleFeature = function (id) {
        var u = $.grep($scope.VehicleFeatureList, function (e) { return e.Id == id; });
        var vehicleFeatureItem = u[0];
        if (confirm("Are you sure you want to delete this item?")) {
            $http({
                method: 'POST',
                url: '/VehicleFeature/DeleteVehicleFeature',
                data: vehicleFeatureItem,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.VehicleFeatureList = jQuery.grep($scope.VehicleFeatureList, function (value) {
                        return value.Id != vehicleFeatureItem.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};