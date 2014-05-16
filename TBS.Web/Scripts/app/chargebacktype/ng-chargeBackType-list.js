//myApp.controller('ChargeBackTypeListController', function ($scope, $http, $modal, ChargeBackTypeService) {
var ChargeBackTypeListController = function ($scope, $http, $modal, ChargeBackTypeService) {

    ChargeBackTypeService.ChargeBackTypeList($http, $scope);
    //config paging table
    $scope.aoColumns = [
            { "sTitle": "Type" },
            { "sTitle": "Description" },
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "  <a href=\"\"  onclick=\"angular.element(this).scope().EditChargeBackType('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>"+
                            "</a>"
                    "<a href=\"\"  onclick=\"angular.element(this).scope()DeleteChargeBackType('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-ban\"></i>"
                            +"</a>";                   

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
    $scope.EditChargeBackType = function (id) {
        var u = $.grep($scope.ChargeBackTypeList, function (e) { return e.Id == id; });
        var chargeBackTypeItem = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/ChargeBackType/Edit',
            controller: 'EditChargeBackTypeController',
            resolve: {
                chargeBackTypeItem: function () {
                    return angular.copy(chargeBackTypeItem);
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            ChargeBackTypeService.ChargeBackTypeList($http, $scope);

        });
    };

    $scope.AddChargeBackType = function () {
        var chargeBackTypeItem = {
            'Name': '',
            'Description': ''
        };
        var modalInstance = $modal.open({
            templateUrl: '/ChargeBackType/Create',
            controller: 'AddChargeBackTypeController',
            resolve: {
                chargeBackTypeItem: function () {
                    return chargeBackTypeItem;
                }
            }
        });

        //modal opened
        modalInstance.opened.then(function () {
            //ComponentsFormTools.init();
        });
        //reload list
        modalInstance.result.then(function () {
            ChargeBackTypeService.ChargeBackTypeList($http, $scope);
        });
    };

    $scope.DeleteChargeBackType = function (id) {
        var u = $.grep($scope.ChargeBackTypeList, function (e) { return e.Id == id; });
        var chargeBackTypeItem = u[0];
        if (confirm("Are you sure you want to delete this item?")) {
            $http({
                method: 'POST',
                url: '/ChargeBackType/DeleteChargeBackType',
                data: chargeBackTypeItem,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.ChargeBackTypeList = jQuery.grep($scope.ChargeBackTypeList, function (value) {
                        return value.Id != chargeBackTypeItem.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};