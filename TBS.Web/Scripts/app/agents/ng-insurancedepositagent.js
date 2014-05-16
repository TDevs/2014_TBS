var CashieringAgentController = function ($http, $scope, $location, AgentService)
{
    $scope.InsuranceDepositClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Agent/InsuranceDepositAgent');
    }
         
    $scope.AutoLoanSetupClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Agent/AutoLoanSetupAgent');
    }

    $scope.AccountReceivableClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Agent/AccountReceivableAgent');
    }

    $scope.SavingDepositClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Agent/SavingDepositAgent');
    }
 
}

var InsuranceDepositAgentController = function ($http, $scope, $location, AgentService)
{
    $scope.InsuranceDepositList = [];
    $scope.CompanySetting = [];
    $scope.AgentVehicleList = [];
    $scope.InsuranceDepositList = AgentService.InsuranceDepositList().then(function (result) {
        $scope.InsuranceDepositList = result;
    });
   
    $scope.CompanySetting = AgentService.CompanySetting().then(function (result) {
        $scope.CompanySetting = result;
    });

    AgentService.AgentVehicleList($http, $scope).then(function (result) {
        $scope.AgentVehicleList = result;
    });

    $scope.IsSubmitted = false;
    $scope.$watch('InsuranceDeposit.DepositAmount', function (newval) {
        if (newval != undefined) {
            if ($scope.InsuranceDeposit.CurrentBalance < 0)
                $scope.InsuranceDeposit.CurrentBalance = newval;
        }
    })
    $scope.AgentVehicleChange = function (agentvehicleId) {
        $scope.IsSubmitted = false;
        var insurancedeposit = $.grep($scope.InsuranceDepositList, function (e) { return e.AgentVehicleId == agentvehicleId; });
        $scope.InsuranceDeposit = { 'CurrentBalance': -1, 'Status': 'Opened' };
        if (insurancedeposit != undefined && insurancedeposit[0] != undefined) {
            $scope.InsuranceDeposit = angular.copy(insurancedeposit[0]);
        }
        $scope.InsuranceDeposit.AgentVehicleId = medallionId;

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
}