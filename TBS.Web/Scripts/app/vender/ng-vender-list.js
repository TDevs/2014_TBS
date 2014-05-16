
var VenderListController = function ($scope, $http, $modal, VenderService) {

    //config paging table
    $scope.aoColumns = [
            { "sTitle": "CC Vendor Name" },
            { "sTitle": "City" },
            { "sTitle": "State" },
            { "sTitle": "Phone 1" },
            { "sTitle": "Fax" },
            { "sTitle": "Email" },
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "  <a href=\"\"  onclick=\"angular.element(this).scope().EditVender('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                    "<a href=\"\"  onclick=\"angular.element(this).scope().DeleteVender('" + obj.aData.Id + "')\">" +
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

    VenderService.VenderList($http, $scope);

    $scope.EditVender = function (id) {
        var u = $.grep($scope.VenderList, function (e) { return e.Id == id; });
        var vender = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/Vender/Edit',
            controller: 'EditVenderController',
            resolve: {
                vender: function () {
                    return angular.copy(vender);
                },
                state: function () { return VenderService.States(); }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            VenderService.VenderList($http, $scope);

        });
    };

    $scope.AddVender = function () {
        var vender = {
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
            templateUrl: '/Vender/Create',
            controller: 'AddVenderController',
            resolve: {
                vender: function () {
                    return vender;
                },
                state: function () { return VenderService.States(); }
            }
        });
        //modal opened
        modalInstance.opened.then(function () {
            //ComponentsFormTools.init();
        });
        //reload list
        modalInstance.result.then(function () {
            VenderService.VenderList($http, $scope);
        });
    };

    $scope.DeleteVender = function (id) {
        var u = $.grep($scope.VenderList, function (e) { return e.Id == id; });
        var vender = u[0];
        if (confirm("Are you sure you want to delete this bank?")) {
            $http({
                method: 'POST',
                url: '/Vender/DeleteVender',
                data: vender,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.VenderList = jQuery.grep($scope.VenderList, function (value) {
                        return value.Id != vender.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};