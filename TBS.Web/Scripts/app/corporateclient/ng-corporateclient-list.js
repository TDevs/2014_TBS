
var CorporateClientListController = function ($scope, $http, $modal, CorporateClientService) {

    CorporateClientService.CorporateClientList($http, $scope);
    //config paging table
    $scope.aoColumns = [
            { "sTitle": "Name" },
            { "sTitle": "City" },
            { "sTitle": "State" },
            { "sTitle": "Phone" },
            { "sTitle": "Fax" },
            { "sTitle": "Email" },            
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "  <a href=\"\"  onclick=\"angular.element(this).scope().EditCorporateClient('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>"+
                    "<a href=\"\"  onclick=\"angular.element(this).scope().DeleteCorporateClient('" + obj.aData.Id + "')\">" +
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

    $scope.EditCorporateClient = function (id) {
        var u = $.grep($scope.CorporateClientList, function (e) { return e.Id == id; });
        var corporateclient = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/CorporateClient/Edit',
            controller: 'EditCorporateClientController',
            resolve: {
                corporateclient: function () {
                    return angular.copy(corporateclient);
                },
                state: function () { return CorporateClientService.States(); }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            CorporateClientService.CorporateClientList($http, $scope);

        });
    };

    $scope.AddCorporateClient = function () {
        var corporateclient = {
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
            templateUrl: '/CorporateClient/Create',
            controller: 'AddCorporateClientController',
            resolve: {
                corporateclient: function () {
                    return corporateclient;
                },
                state: function () { return CorporateClientService.States(); }
            }
        });
        //modal opened
        modalInstance.opened.then(function () {
            //ComponentsFormTools.init();
        });
        //reload list
        modalInstance.result.then(function () {
            CorporateClientService.CorporateClientList($http, $scope);
        });
    };

    $scope.DeleteCorporateClient = function (id) {
        var u = $.grep($scope.CorporateClientList, function (e) { return e.Id == id; });
        var corporateclient = u[0];
        if (confirm("Are you sure you want to delete this corprorate client?")) {
            $http({
                method: 'POST',
                url: '/CorporateClient/DeleteCorporateClient',
                data: corporateclient,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.CorporateClientList = jQuery.grep($scope.CorporateClientList, function (value) {
                        return value.Id != corporateclient.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};