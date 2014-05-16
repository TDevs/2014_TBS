var VehicleMakeListController = function ($scope, $http, $modal, VehicleMakeService) {
    //config paging table
    $scope.aoColumns = [
            { "sTitle": "Vehicle Make Name" },
            
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "  <a href=\"\"  onclick=\"angular.element(this).scope().EditVehicleMake('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                    "<a href=\"\"  onclick=\"angular.element(this).scope().DeleteVehicleMake('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-ban\"></i>"
                            + "</a>";

                    return action;
                }
            }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "Name", "aTargets": [0] },                        
                        { "mDataProp": "Id", "aTargets": [1] }
    ];
    //end config
    VehicleMakeService.VehicleMakeList($http, $scope);

    $scope.EditVehicleMake = function (id) {
        var u = $.grep($scope.VehicleMakeList, function (e) { return e.Id == id; });
        var vehicleMakeItem = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/VehicleMake/Edit',
            controller: 'EditVehicleMakeController',
            resolve: {
                vehicleMakeItem: function () {
                    return angular.copy(vehicleMakeItem);
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            VehicleMakeService.VehicleMakeList($http, $scope);

        });
    };

    $scope.AddVehicleMake = function () {
        var vehicleMakeItem = {
            'Name': ''
        };
        var modalInstance = $modal.open({
            templateUrl: '/VehicleMake/Create',
            controller: 'AddVehicleMakeController',
            resolve: {
                vehicleMakeItem: function () {
                    return vehicleMakeItem;
                }
            }
        });

        //modal opened
        modalInstance.opened.then(function () {
            //ComponentsFormTools.init();
        });
        //reload list
        modalInstance.result.then(function () {
            VehicleMakeService.VehicleMakeList($http, $scope);
        });
    };

    $scope.DeleteVehicleMake = function (id) {
        var u = $.grep($scope.VehicleMakeList, function (e) { return e.Id == id; });
        var vehicleMakeItem = u[0];
        if (confirm("Are you sure you want to delete this item?")) {
            $http({
                method: 'POST',
                url: '/VehicleMake/DeleteVehicleMake',
                data: vehicleMakeItem,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.VehicleMakeList = jQuery.grep($scope.VehicleMakeList, function (value) {
                        return value.Id != vehicleMakeItem.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};