var MeterManufacturerListController = function ($scope, $http, $modal, MeterManufacturerService) {
    //config paging table
    $scope.aoColumns = [
            { "sTitle": "Meter Manufacture" },           
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "  <a href=\"\"  onclick=\"angular.element(this).scope().EditMeterManufacturer('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                    "<a href=\"\"  onclick=\"angular.element(this).scope().DeleteMeterManufacturer('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-ban\"></i>"
                            + "</a>";

                    return action;
                }
            }
    ];

    $scope.aoColumnDefs = [
                            { "mDataProp": "Description", "aTargets": [0] },
                         
                            { "mDataProp": "Id", "aTargets": [1] }
    ];
    //end config
    MeterManufacturerService.MeterManufacturerList($http, $scope);

    $scope.EditMeterManufacturer = function (id) {
        var u = $.grep($scope.MeterManufacturerList, function (e) { return e.Id == id; });
        var meterManufacturerItem = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/MeterManufacturer/Edit',
            controller: 'EditMeterManufacturerController',
            resolve: {
                meterManufacturerItem: function () {
                    return angular.copy(meterManufacturerItem);
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            MeterManufacturerService.MeterManufacturerList($http, $scope);

        });
    };

    $scope.AddMeterManufacturer = function () {
        var meterManufacturerItem = {
            'Description': ''
        };
        var modalInstance = $modal.open({
            templateUrl: '/MeterManufacturer/Create',
            controller: 'AddMeterManufacturerController',
            resolve: {
                meterManufacturerItem: function () {
                    return meterManufacturerItem;
                }
            }
        });

        //modal opened
        modalInstance.opened.then(function () {
            //ComponentsFormTools.init();
        });
        //reload list
        modalInstance.result.then(function () {
            MeterManufacturerService.MeterManufacturerList($http, $scope);
        });
    };

    $scope.DeleteMeterManufacturer = function (id) {
        var u = $.grep($scope.MeterManufacturerList, function (e) { return e.Id == id; });
        var meterManufacturerItem = u[0];
        if (confirm("Are you sure you want to delete this item?")) {
            $http({
                method: 'POST',
                url: '/MeterManufacturer/DeleteMeterManufacturer',
                data: meterManufacturerItem,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.MeterManufacturerList = jQuery.grep($scope.MeterManufacturerList, function (value) {
                        return value.Id != meterManufacturerItem.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};