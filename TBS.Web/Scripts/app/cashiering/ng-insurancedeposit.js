var InsuranceDepositController = function ($http, $scope, $q, CashieringService) {
    var deferMedallion = $q.defer();
    var deferInsuranceDeposit = $q.defer();
    $scope.InsuranceDepositList = CashieringService.InsuranceDepositList($http, $scope);
    $scope.InsuranceDeposit = CashieringService.InsuranceDeposit($http, $scope, $q);
    $scope.CompanySetting = CashieringService.CompanySetting($http, $scope, $q);
    $scope.MedallionList = CashieringService.MedallionList($http, $scope, $q);
    
    $scope.IsSubmitted = false;
    $scope.$watch('InsuranceDeposit.DepositAmount', function (newval) {
        if (newval != undefined) {
            if ($scope.InsuranceDeposit.CurrentBalance < 0)
                $scope.InsuranceDeposit.CurrentBalance = newval;
        }
    })
    $scope.MedallionChange = function (medallionId) {
        $scope.IsSubmitted = false;
        var insurancedeposit = $.grep($scope.InsuranceDepositList, function (e) { return e.MedallionId == medallionId; });
        $scope.InsuranceDeposit = { 'CurrentBalance': -1, 'Status': 'Opened' };
        if (insurancedeposit != undefined && insurancedeposit[0] != undefined) {
            $scope.InsuranceDeposit = angular.copy(insurancedeposit[0]);
        }
        $scope.InsuranceDeposit.MedallionId = medallionId;

    }

    $scope.Save = function (invalid) {

        $scope.IsSubmitted = true;
        if (!invalid) {
            $scope.InsuranceDeposit.MemberId = $scope.Member.Id;
            $http({
                method: 'POST',
                url: '/Cashiering/SaveInsuranceDeposit',
                data: $scope.InsuranceDeposit,
                async: false,
            }).success(function (data, status, headers, config) {
                alert("Save Insurance Deposit success!");
                //switch to 
                CashieringService.InsuranceDepositList($http, $scope, $q);

            })
        }
    }
};