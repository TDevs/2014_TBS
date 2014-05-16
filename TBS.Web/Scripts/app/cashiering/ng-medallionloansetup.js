var MedallionLoanSetupController = function ($http, $scope, $q, CashieringService) {
    var deferMedallion = $q.defer();
    var deferCompany = $q.defer();
    $scope.MedallionLoanSetupList = CashieringService.MedallionLoanSetupList($http, $scope);
    $scope.MedallionLoanSetup = CashieringService.MedallionLoanSetup($http, $scope, $q);
    $scope.CompanySetting = CashieringService.CompanySetting($http, $scope, $q);
    $scope.MedallionList = CashieringService.MedallionList($http, $scope, $q);
    $scope.BankList = CashieringService.BankList($http, $scope);
    //$scope.Member = CashieringService.Member();
    $scope.IsSubmitted = false;
    $scope.$watch('MedallionLoanSetup.LoanAmount', function (newval) {
        if (newval != undefined && $scope.MedallionLoanSetup.CurrentBalance < 0) {
            $scope.MedallionLoanSetup.CurrentBalance = newval;
        }
    });
    //event onchange Medallion combobox
    $scope.MedallionChange = function (medallionId) {
        $scope.IsSubmitted = false;
        var medallionLoanSetup = $.grep($scope.MedallionLoanSetupList, function (e) { return e.MedallionId == medallionId; });
        $scope.MedallionLoanSetup = { 'CurrentBalance': -1, 'Status': 'Opened' };
        if (medallionLoanSetup != undefined && medallionLoanSetup[0] != undefined) {
            $scope.MedallionLoanSetup = angular.copy(medallionLoanSetup[0]);
        }
        

        $scope.MedallionLoanSetup.MedallionId = medallionId;
        
        if ($scope.MedallionLoanSetup.BankId != undefined)
            $scope.MedallionLoanSetup.BankId = $scope.CompanySetting.BankId;
 
    }

    $scope.Save = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {

            $scope.MedallionLoanSetup.MemberId = $scope.Member.Id;
            Calc();
            $http({
                method: 'POST',
                url: '/Cashiering/SaveMedallionLoanSetup',
                data: $scope.MedallionLoanSetup,
                async: false,
            }).success(function (data, status, headers, config) {
                alert("Save Medallion Loan Setup  success!");
                //switch to 
                CashieringService.MedallionLoanSetupList($http, $scope);

            })
        }
    }

    $scope.Recalculate = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {
            Calc();
        }
    }

    function Calc() {
        
        //amount 
        var amountInterest = ($scope.MedallionLoanSetup.LoanAmount * ($scope.MedallionLoanSetup.InterestRate/ 12/100));
        var amount = parseFloat($scope.MedallionLoanSetup.LoanAmount) + (amountInterest * $scope.MedallionLoanSetup.LoanTerm);
        //set value to CalculatedMonthlyPayment
        $scope.MedallionLoanSetup.CalculatedMonthlyPayment = amount / $scope.MedallionLoanSetup.LoanTerm;
        //set value to Total Paid
        if ($scope.MedallionLoanSetup.TotalPaid <= 0 || $scope.MedallionLoanSetup.TotalPaid == undefined) {
            $scope.MedallionLoanSetup.TotalPaid = 0;
        }
        //Total Principal Paid 
        $scope.MedallionLoanSetup.TotalPrincipalPaid = Math.round($scope.MedallionLoanSetup.LoanAmount / $scope.MedallionLoanSetup.LoanTerm);
        //Total Interest Paid 
        $scope.MedallionLoanSetup.TotalInterestPaid = amountInterest;
        //Current Balance 
        $scope.MedallionLoanSetup.CurrentBalance = amount - $scope.MedallionLoanSetup.TotalPaid;
    }

    //watch StartDate  
    $scope.$watch('MedallionLoanSetup.StartDate', function (newVal) {
        if (newVal != null && $scope.MedallionLoanSetup.LoanTerm != undefined && $scope.MedallionLoanSetup.LoanTerm > 0) {
            var startDate = new Date(newVal);
            startDate.setMonth(startDate.getMonth() + $scope.MedallionLoanSetup.LoanTerm);
            $scope.MedallionLoanSetup.EndDate = startDate.getMonth() + 1 + '/' + startDate.getDate() + '/' + startDate.getFullYear();
        }
    });

    $scope.$watch('MedallionLoanSetup.LoanTerm', function (newVal) {
        if (newVal != null && $scope.MedallionLoanSetup.StartDate != undefined) {
            var startDate = new Date($scope.MedallionLoanSetup.StartDate);
            startDate.setMonth(startDate.getMonth() + newVal);
            $scope.MedallionLoanSetup.EndDate = startDate.getMonth() + 1 + '/' + startDate.getDate() + '/' + startDate.getFullYear();
        }
    });

    //watch medallion 
    $scope.$watch('MedallionLoanSetup', function (newMedallion) {
        if (newMedallion != null) {
            deferMedallion.resolve(newMedallion);
        }
    });
    //watch company 
    $scope.$watch('CompanySetting', function (newCompany) {
        if (newCompany != null) {
            deferCompany.resolve(newCompany);
        }
    });
    $q.all([deferMedallion.promise, deferCompany.promise]).then(function (arr) {
        if (arr[0].InterestRate <= 0 || arr[0].InterestRate == undefined) {
            $scope.MedallionLoanSetup.InterestRate = arr[1].InterestRate;
            $scope.MedallionLoanSetup.BankId = arr[1].BankId;
        }
    });
};