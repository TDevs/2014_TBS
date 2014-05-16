
var WerkShopListController = function ($scope, $http, $modal, WerkShopService) {

    //config paging table
    $scope.aoColumns = [
            { "sTitle": "Werk Shop Name" },
            { "sTitle": "City" },
            { "sTitle": "State" },
            { "sTitle": "Phone 1" },
            { "sTitle": "Fax" },
            { "sTitle": "Email" },
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "  <a href=\"\"  onclick=\"angular.element(this).scope().EditWerkShop('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                    "<a href=\"\"  onclick=\"angular.element(this).scope().DeleteWerkShop('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-ban\"></i>"
                            + "</a>";

                    return action;
                }
            }
    ];

    $scope.aoColumnDefs = [
                            { "mDataProp": "Name", "aTargets": [0] },
                            { "mDataProp": "City", "aTargets": [1] },
                            { "mDataProp": "State", "aTargets": [2] },
                            { "mDataProp": "Phone1", "aTargets": [3] },
                            { "mDataProp": "Fax", "aTargets": [4] },
                            { "mDataProp": "Email", "aTargets": [5] },
                            { "mDataProp": "Id", "aTargets": [6] }
    ];
    //end config

    WerkShopService.WerkShopList($http, $scope);

    $scope.EditWerkShop = function (id) {
        var u = $.grep($scope.WerkShopList, function (e) { return e.Id == id; });
        var werkshop = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/WerkShop/Edit',
            controller: 'EditWerkShopController',
            resolve: {
                werkshop: function () {
                    return angular.copy(werkshop);
                },
                state: function () { return WerkShopService.States(); }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            WerkShopService.WerkShopList($http, $scope);

        });
    };

    $scope.AddWerkShop = function () {
        var werkshop = {
            'Name': '',
            'Address': '',
            'Address1': '',
            'State': '',
            'Zip': '',
            'Phone1': '',
            'Phone2': '',
            'Email': '',
            'Fax': '',
            'City': '',
            'Comment': ''
        };
        var modalInstance = $modal.open({
            templateUrl: '/WerkShop/Create',
            controller: 'AddWerkShopController',
            resolve: {
                werkshop: function () {
                    return werkshop;
                },
                state: function () { return WerkShopService.States(); }
            }
        });
        //modal opened
        modalInstance.opened.then(function () {
            //ComponentsFormTools.init();
        });
        //reload list
        modalInstance.result.then(function () {
            WerkShopService.WerkShopList($http, $scope);
        });
    };

    $scope.DeleteWerkShop = function (id) {
        var u = $.grep($scope.WerkShopList, function (e) { return e.Id == id; });
        var werkshop = u[0];
        if (confirm("Are you sure you want to delete this bank?")) {
            $http({
                method: 'POST',
                url: '/WerkShop/DeleteWerkShop',
                data: werkshop,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.WerkShopList = jQuery.grep($scope.WerkShopList, function (value) {
                        return value.Id != werkshop.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};