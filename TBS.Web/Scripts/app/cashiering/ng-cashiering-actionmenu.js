var CashieringController = function ($http, $scope,$location)
{
    $scope.LoansClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Cashiering/LoanList');
    }
    $scope.InsuranceDepositClick = function ()
    {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Cashiering/InsuranceDeposit');
    }

    $scope.CCSystemAirtimeClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Cashiering/CCSystemAirtime');
    }

    $scope.MedallionLoanSetupClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Cashiering/MedallionLoanSetup');
    }

    $scope.AutoLoanSetupClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Cashiering/AutoLoanSetup');
    }

    $scope.AccountReceivableClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Cashiering/AccountReceivable');
    }

    $scope.SavingDepositClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Cashiering/SavingDeposit');
    }

    $scope.AccountSummaryClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Cashiering/AccountSummary');
    }
}