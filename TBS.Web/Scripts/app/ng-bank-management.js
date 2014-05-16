//var myApp = angular.module('TDev', ['ui.bootstrap']);--> move to global core (ng-tbs-core.js)

////create service BankService
//window.app.service('BankService', function () {
//    //get banks
//    this.BankList = function ($http, $scope) {
//        $http({
//            method: 'GET',
//            url: '/Bank/GetBanks',            
//            async: false,
//        }).success(function (data, status, headers, config) {
//            $scope.BankList = data;
//        })
//    }

//});

window.app.controller('EditBankController', function ($scope, $http, $modalInstance, bank) {
    $scope.bank = bank;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {
            //save bank
            $http({
                method: 'POST',
                url: '/Bank/EditBank',
                data: $scope.bank,
                async: true
            });
            $modalInstance.close();
        }
        
        
    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});

window.app.controller('AddBankController', function ($scope, $http, $modalInstance, bank) {
    $scope.bank = bank;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {

            //save bank
            $http({
                method: 'POST',
                url: '/Bank/AddBank',
                data: $scope.bank,
                async: true
            });
            $modalInstance.close();
        }
    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});


window.app.controller('BankListController', function ($scope, $http, $modal, BankService) {

    BankService.BankList($http, $scope);

    $scope.EditBank = function (bank) {
        var modalInstance = $modal.open({
            templateUrl: '/Bank/Edit',
            controller: 'EditBankController',
            resolve: {
                bank: function () {
                    return bank;
                }
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
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            BankService.BankList($http, $scope);            
        });
    };

    $scope.DeleteBank = function (bank) {
        if (confirm("Are you sure you want to delete this bank?")) {
            $http({
                method: 'POST',
                url: '/Bank/DeleteBank',
                data: bank,
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.BankList = jQuery.grep($scope.BankList, function (value) {
                    return value.Id != bank.Id;
                });
            })
        }
    };
});

