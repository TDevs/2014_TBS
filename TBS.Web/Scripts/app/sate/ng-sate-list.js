var SateListController = function ($scope, $http, $modal, SateService) {
    //config paging table
    $scope.aoColumns = [
            { "sTitle": "State Name" },
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "  <a href=\"\"  onclick=\"angular.element(this).scope().EditSate('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                    "<a href=\"\"  onclick=\"angular.element(this).scope().DeleteSate('" + obj.aData.Id + "')\">" +
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
    SateService.SateList($http, $scope);

    $scope.EditSate = function (id) {
        var u = $.grep($scope.SateList, function (e) { return e.Id == id; });
        var sateItem = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/Sate/Edit',
            controller: 'EditSateController',
            resolve: {
                sateItem: function () {
                    return angular.copy(sateItem);
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            SateService.SateList($http, $scope);

        });
    };

    $scope.AddSate = function () {
        var sateItem = {
            'Name': ''
        };
        var modalInstance = $modal.open({
            templateUrl: '/Sate/Create',
            controller: 'AddSateController',
            resolve: {
                sateItem: function () {
                    return sateItem;
                }
            }
        });

        //modal opened
        modalInstance.opened.then(function () {
            //ComponentsFormTools.init();
        });
        //reload list
        modalInstance.result.then(function () {
            SateService.SateList($http, $scope);
        });
    };

    $scope.DeleteSate = function (id) {
        var u = $.grep($scope.SateList, function (e) { return e.Id == id; });
        var sateItem = u[0];
        if (confirm("Are you sure you want to delete this item?")) {
            $http({
                method: 'POST',
                url: '/Sate/DeleteSate',
                data: sateItem,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.SateList = jQuery.grep($scope.SateList, function (value) {
                        return value.Id != sateItem.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};