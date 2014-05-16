var AutoLoanSetupController = function ($http, $scope, $q, CashieringService) {
    var deferAuto = $q.defer();
    var deferCompany = $q.defer();
    $scope.AutoLoanSetupList = CashieringService.AutoLoanSetupList($http, $scope);
    $scope.AutoLoanSetup = CashieringService.AutoLoanSetup($http, $scope, $q);
    $scope.CompanySetting = CashieringService.CompanySetting($http, $scope, $q);
    $scope.MedallionList = CashieringService.MedallionList($http, $scope, $q);
    $scope.BankList = CashieringService.BankList($http, $scope);
    //$scope.Member = CashieringService.Member();
    $scope.IsSubmitted = false;
    $scope.$watch('AutoLoanSetup.LoanAmount', function (newval) {
        if (newval != undefined && $scope.AutoLoanSetup.CurrentBalance < 0) {
            $scope.AutoLoanSetup.CurrentBalance = newval;
        }
    });
    //event onchange Medallion combobox
    $scope.MedallionChange = function (medallionId) {
        $scope.IsSubmitted = false;
        var autoLoanSetup = $.grep($scope.AutoLoanSetupList, function (e) { return e.MedallionId == medallionId; });
        $scope.AutoLoanSetup = { 'CurrentBalance': -1, 'Status': 'Opened' };
        if (autoLoanSetup != undefined && autoLoanSetup[0] != undefined) {
            $scope.AutoLoanSetup = angular.copy(autoLoanSetup[0]);
        }
        // if ($scope.AutoLoanSetup.CurrentBalance < 0) {            
        $scope.AutoLoanSetup.MedallionId = medallionId;
        //$scope.AutoLoanSetup.CurrentBalance = $scope.AutoLoanSetup.LoanAmount; 
        if ($scope.AutoLoanSetup.BankId != undefined) {
            $scope.AutoLoanSetup.BankId = $scope.CompanySetting.BankId;
        }
        //}
    }


    $scope.Save = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {

            $scope.AutoLoanSetup.MemberId = $scope.Member.Id;
            Calc();
            $http({
                method: 'POST',
                url: '/Cashiering/SaveAutoLoanSetup',
                data: $scope.AutoLoanSetup,
                async: false,
            }).success(function (data, status, headers, config) {
                alert("Save Auto Loan Setup  success!");
                //switch to 
                CashieringService.AutoLoanSetupList($http, $scope, $q);

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
        var amountInterest = ($scope.AutoLoanSetup.LoanAmount * ($scope.AutoLoanSetup.InterestRate / 12/100));
        var amount = parseFloat($scope.AutoLoanSetup.LoanAmount) + (amountInterest *$scope.AutoLoanSetup.LoanTerm);
        //set value to CalculatedMonthlyPayment
        $scope.AutoLoanSetup.CalculatedMonthlyPayment = amount / $scope.AutoLoanSetup.LoanTerm;
        //set value to Total Paid
        if ($scope.AutoLoanSetup.TotalPaid <= 0 || $scope.AutoLoanSetup.TotalPaid == undefined) {
         $scope.AutoLoanSetup.TotalPaid = 0;
        }
        //Total Principal Paid 
        $scope.AutoLoanSetup.TotalPrincipalPaid = Math.round($scope.AutoLoanSetup.LoanAmount / $scope.AutoLoanSetup.LoanTerm);
        //Total Interest Paid 
        $scope.AutoLoanSetup.TotalInterestPaid = amountInterest;
        //Current Balance 
        $scope.AutoLoanSetup.CurrentBalance = amount - $scope.AutoLoanSetup.TotalPaid;
    }

    //watch StartDate  
    $scope.$watch('AutoLoanSetup.StartDate', function (newVal) {
        if (newVal != null && $scope.AutoLoanSetup.LoanTerm != undefined && $scope.AutoLoanSetup.LoanTerm > 0) {
            var startDate = new Date(newVal);
            startDate.setMonth(startDate.getMonth() + $scope.AutoLoanSetup.LoanTerm);
            $scope.AutoLoanSetup.EndDate = startDate.getMonth() + 1 + '/' + startDate.getDate() + '/' + startDate.getFullYear();
        }
    });

    $scope.$watch('AutoLoanSetup.LoanTerm', function (newVal) {
        if (newVal != null && $scope.AutoLoanSetup.StartDate != undefined) {
            var startDate = new Date($scope.AutoLoanSetup.StartDate);
            startDate.setMonth(startDate.getMonth() + newVal);
            $scope.AutoLoanSetup.EndDate = startDate.getMonth() + 1 + '/' + startDate.getDate() + '/' + startDate.getFullYear();
        }
    });

    //watch medallion 
    $scope.$watch('AutoLoanSetup', function (newauto) {
        if (newauto != null) {
            deferAuto.resolve(newauto);
        }
    });
    //watch company 
    $scope.$watch('CompanySetting', function (newCompany) {
        if (newCompany != null) {
            deferCompany.resolve(newCompany);
        }
    });
    $q.all([deferAuto.promise, deferCompany.promise]).then(function (arr) {
        if (arr[0].InterestRate <= 0 || arr[0].InterestRate == undefined) {
            $scope.AutoLoanSetup.InterestRate = arr[1].InterestRate;
            $scope.AutoLoanSetup.BankId = arr[1].BankId;
        }
    });
};