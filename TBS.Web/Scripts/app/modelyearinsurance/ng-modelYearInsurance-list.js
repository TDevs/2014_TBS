var ModelYearInsuranceListController = function ($scope, $http, $modal, ModelYearInsuranceService) {

    //config paging table
    $scope.aoColumns = [
            { "sTitle": "Model Year" },
            { "sTitle": "Amount" },            
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "  <a href=\"\"  onclick=\"angular.element(this).scope().EditModelYearInsurance('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                    "<a href=\"\"  onclick=\"angular.element(this).scope().DeleteModelYearInsurance('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-ban\"></i>"
                            + "</a>";

                    return action;
                }
            }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "ModelYear", "aTargets": [0] },
                        { "mDataProp": "Amount", "aTargets": [1] },
                        { "mDataProp": "Id", "aTargets": [2] }
    ];
    //end config

    ModelYearInsuranceService.ModelYearInsuranceList($http, $scope);

    $scope.EditModelYearInsurance = function (id) {
        var u = $.grep($scope.ModelYearInsuranceList, function (e) { return e.Id == id; });
        var modelYearInsuranceItem = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/ModelYearInsurance/Edit',
            controller: 'EditModelYearInsuranceController',
            resolve: {
                modelYearInsuranceItem: function () {
                    return angular.copy(modelYearInsuranceItem);
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            ModelYearInsuranceService.ModelYearInsuranceList($http, $scope);

        });
    };

    $scope.AddModelYearInsurance = function () {
        var modelYearInsuranceItem = {
            'ModelYear': '',
            'Amount': ''
        };
        var modalInstance = $modal.open({
            templateUrl: '/ModelYearInsurance/Create',
            controller: 'AddModelYearInsuranceController',
            resolve: {
                modelYearInsuranceItem: function () {
                    return modelYearInsuranceItem;
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            ModelYearInsuranceService.ModelYearInsuranceList($http, $scope);
        });
    };

    $scope.DeleteModelYearInsurance = function (id) {
        var u = $.grep($scope.ModelYearInsuranceList, function (e) { return e.Id == id; });
        var modelYearInsuranceItem = u[0];
        if (confirm("Are you sure you want to delete this item?")) {
            $http({
                method: 'POST',
                url: '/ModelYearInsurance/DeleteModelYearInsurance',
                data: modelYearInsuranceItem,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.ModelYearInsuranceList = jQuery.grep($scope.ModelYearInsuranceList, function (value) {
                        return value.Id != modelYearInsuranceItem.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};