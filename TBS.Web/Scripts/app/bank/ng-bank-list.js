
var BankListController = function ($scope, $http, $modal, BankService) {

    BankService.BankList($http, $scope);

    $scope.aoColumns = [
        { "sTitle": "Bank Name" },
        { "sTitle": "City" },
        { "sTitle": "State" },
        { "sTitle": "Phone 1" },
          { "sTitle": "Fax" },
        { "sTitle": "E-mail" },
        {
            "sTitle": "",
            "fnRender": function (obj) {
                var sReturn = obj.aData[obj.iDataColumn];
                var action = "<a href=\"#\" onclick=\"angular.element(this).scope().EditBank('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                            "<a href=\"#\" onclick=\"angular.element(this).scope().DeleteBank('" + obj.aData.Id + "')\"><i class=\"fa fa-ban\"></i></a>";

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

    $scope.EditBank = function (id) {
        var u = $.grep($scope.BankList, function (e) { return e.Id == id; });
        var bank = u[0];

        var modalInstance = $modal.open({
            templateUrl: '/Bank/Edit',
            controller: 'EditBankController',
            resolve: {
                bank: function () {
                    return angular.copy(bank);
                },
                state: function () { return BankService.States(); }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            BankService.BankList($http, $scope);

        });
    };

    $scope.AddBank = function () {
        var bank = {
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
            templateUrl: '/Bank/Create',
            controller: 'AddBankController',
            resolve: {
                bank: function () {
                    return bank;
                },
                state: function () { return BankService.States(); }
            }
        });
        //modal opened
        modalInstance.opened.then(function () {
            //ComponentsFormTools.init();
        });
        //reload list
        modalInstance.result.then(function () {
            BankService.BankList($http, $scope);
        });
    };

    $scope.DeleteBank = function (id) {
        var u = $.grep($scope.BankList, function (e) { return e.Id == id; });
        var bank = u[0];
        if (confirm("Are you sure you want to delete this bank?")) {
            $http({
                method: 'POST',
                url: '/Bank/DeleteBank',
                data: bank,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.BankList = jQuery.grep($scope.BankList, function (value) {
                        return value.Id != bank.Id;
                    });
                } else {
                    alert('The record is inused');
                }
            })
        }
    };
};