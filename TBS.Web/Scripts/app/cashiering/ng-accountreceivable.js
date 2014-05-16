var AccountReceivableController = function ($http, $scope, $q, CashieringService) {    
    $scope.AccountReceivableList = CashieringService.AccountReceivableList($http, $scope);
    $scope.AccountReceivable = CashieringService.AccountReceivable($http, $scope, $q);
    $scope.CompanySetting = CashieringService.CompanySetting($http, $scope, $q);
    $scope.MedallionList = CashieringService.MedallionList($http, $scope, $q);
    
    $scope.IsSubmitted = false;

    //event onchange Medallion combobox
    $scope.MedallionChange = function (medallionId) {
        $scope.IsSubmitted = false;
        var accountReceivable = $.grep($scope.AccountReceivableList, function (e) { return e.MedallionId == medallionId; });
        $scope.AccountReceivable = { 'CurrentBalance': -1, 'Status': 'Opened' };
        if (accountReceivable != undefined && accountReceivable[0] != undefined) {
            $scope.AccountReceivable = angular.copy(accountReceivable[0]);
        }
             
                $scope.AccountReceivable.MedallionId =medallionId;
             
    }


    $scope.Save = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {

            $scope.AccountReceivable.MemberId = $scope.Member.Id;
            Calc();
            $http({
                method: 'POST',
                url: '/Cashiering/SaveAccountReceivable',
                data: $scope.AccountReceivable,
                async: false,
            }).success(function (data, status, headers, config) {
                alert("Save Account Receivable success!");
                //switch to 
                CashieringService.AccountReceivableList($http, $scope, $q);

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
        var amount = $scope.AccountReceivable.AccountReceivableAmount;
        //set value to Monthly payments 
        $scope.AccountReceivable.MonthlyPayment = amount / $scope.AccountReceivable.PaymentTermMonth;
        //set value to Total Paid
        if ($scope.AccountReceivable.TotalPaid == undefined || $scope.AccountReceivable.TotalPaid == "") {
            $scope.AccountReceivable.TotalPaid = 0;
        }
        //Current Balance 
        $scope.AccountReceivable.CurrentBalance = amount - $scope.AccountReceivable.TotalPaid;
    }

    //watch StartDate  
    $scope.$watch('AccountReceivable.StartDate', function (newVal) {
        if (newVal != null && $scope.AccountReceivable.PaymentTermMonth != undefined && $scope.AccountReceivable.PaymentTermMonth>0) {
            var startDate = new Date(newVal);
            startDate.setMonth(startDate.getMonth() + $scope.AccountReceivable.PaymentTermMonth);
            $scope.AccountReceivable.EndDate = startDate.getMonth() + 1 + '/' + startDate.getDate() + '/' + startDate.getFullYear();
        }
    });

    $scope.$watch('AccountReceivable.PaymentTermMonth', function (newval) {
        if (newval != null && $scope.AccountReceivable.StartDate != undefined) {
            var startDate = new Date($scope.AccountReceivable.StartDate);
            startDate.setMonth(startDate.getMonth() + newval);
            $scope.AccountReceivable.EndDate = startDate.getMonth() + 1 + '/' + startDate.getDate() + '/' + startDate.getFullYear();
        }
    });
};