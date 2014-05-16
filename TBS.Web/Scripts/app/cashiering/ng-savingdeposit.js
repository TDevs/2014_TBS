var SavingDepositController = function ($http, $scope, $q, CashieringService) {
    $scope.SavingDepositList = CashieringService.SavingDepositList($http, $scope);
    $scope.SavingDeposit = CashieringService.SavingDeposit($http, $scope, $q);
    $scope.CompanySetting = CashieringService.CompanySetting($http, $scope, $q);
    $scope.MedallionList = CashieringService.MedallionList($http, $scope, $q);
     
    $scope.IsSubmitted = false;

    //event onchange Medallion combobox
    $scope.MedallionChange = function (medallionId) {
        $scope.IsSubmitted = false;
        var savingDeposit = $.grep($scope.SavingDepositList, function (e) { return e.MedallionId == medallionId; });
        $scope.SavingDeposit = { 'CurrentBalance': -1, 'Status': 'Opened' };
        if (savingDeposit != undefined && savingDeposit[0] != undefined) {
            $scope.SavingDeposit = angular.copy(savingDeposit[0]);
        }

        $scope.SavingDeposit.MedallionId = medallionId;
        //$scope.SavingDeposit.CurrentBalance = angular.copy(medallion[0]).Balance;

    }



    $scope.Save = function (invalid) {

        $scope.IsSubmitted = true;
        if (!invalid) {
            $scope.SavingDeposit.MemberId = $scope.Member.Id;
            $http({
                method: 'POST',
                url: '/Cashiering/SaveSavingDeposit',
                data: $scope.SavingDeposit,
                async: false,
            }).success(function (data, status, headers, config) {
                alert("Save Saving Deposit success!");
                //switch to 
                CashieringService.SavingDepositList($http, $scope, $q);

            })
        }
    }
};