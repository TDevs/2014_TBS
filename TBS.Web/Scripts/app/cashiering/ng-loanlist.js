var LoanListController = function ($http, $scope, $location, $q, CashieringService) {


    CashieringService.LoanList($scope).then(function (result) {
        $scope.LoanList = result;
    })

    //config paging table
    $scope.aoColumns = [
            { "sTitle": "Loan Name" },
            { "sTitle": "Medallion #" },
            { "sTitle": "Amount" },
            { "sTitle": "Start Date" },
            { "sTitle": "End Date" },
            { "sTitle": "Status" },
            { "sTitle": "Total Paid" },
            { "sTitle": "Total Interest Paid" },
            { "sTitle": "Total Principal" },
            {
                "sTitle": "",
                "fnRender": function (obj) {
                var sReturn = obj.aData[obj.iDataColumn];
                var action = "<a href=\"\" onclick=\"angular.element(this).scope().EditLoan('" + obj.aData.Id + "')\">" +
                    "<i class=\"fa fa-edit\"></i>" +
                    "</a>" +
                    "<a href=\"\" onclick=\"angular.element(this).scope().DeleteLoan('" + obj.aData.Id + "')\"><i class=\"fa fa-ban\"></i></a>";

                    return action;
                }
            }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "LoanName", "aTargets": [0] },
                        { "mDataProp": "Medallion.MedallionNumber", "aTargets": [1] },
                        { "mDataProp": "LoanAmount", "aTargets": [2] },
                        { "mDataProp": "StartDate", "aTargets": [3] },
                        { "mDataProp": "EndDate", "aTargets": [4] },
                        { "mDataProp": "Status", "aTargets": [5] },
                        { "mDataProp": "TotalPaid", "aTargets": [6] },
                        { "mDataProp": "TotalInterestPaid", "aTargets": [7] },
                        { "mDataProp": "TotalPrincipalPaid", "aTargets": [8] },
                        { "mDataProp": "Id", "aTargets": [9] }
    ];
    //end config
    $scope.AddLoan = function () {
        $location.path('/Cashiering/AddNewLoan');
    }

    $scope.EditLoan = function (id) {
        var u = $.grep($scope.LoanList, function (e) { return e.Id == id; });
        var loan = u[0];
        $http({
            method: 'POST',
            url: '/Cashiering/GetLoanById',
            data: { id: loan.Id },
            async: false,
        }).success(function (data, status, headers, config) {
            if (data != undefined) {
                CashieringService.setLoan(data);
                $location.path('/Cashiering/EditLoan');
            }
            else {
                alert('Can not edit this loan');
            }
        })
    }

    $scope.DeleteLoan = function  (id) {
        var u = $.grep($scope.LoanList, function (e) { return e.Id == id; });
        var loan = u[0];
        if (confirm("Are you sure you want to delete this loan?")) {
            $http({
                method: 'POST',
                url: '/Cashiering/DeleteLoan',
                data: { loan: loan },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    CashieringService.LoanList($scope).then(function (result) {
                        $scope.LoanList = result;
                    })
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    }
}

var NewLoanController = function ($http, $scope, $location, $q, CashieringService) {

    var deferMedallion = $q.defer();
    var deferCompany = $q.defer();
    $scope.Loan = {
        'CurrentBalance': -1,
        'Status': 'Opened'
    }
    $scope.CompanySetting = CashieringService.CompanySetting($http, $scope, $q);
    $scope.MedallionList = CashieringService.MedallionList($http, $scope, $q);
    $scope.BankList = CashieringService.BankList($http, $scope);

    $scope.IsSubmitted = false;
    $scope.$watch('Loan.LoanAmount', function (newval) {
        if (newval != undefined && $scope.Loan.CurrentBalance < 0) {
            $scope.Loan.CurrentBalance = newval;
        }
    });
    //event onchange Medallion combobox
    $scope.MedallionChange = function (medallionId) {
        $scope.IsSubmitted = false;
        $scope.Loan.MedallionId = medallionId;
        if ($scope.Loan.BankId != undefined)
            $scope.Loan.BankId = $scope.CompanySetting.BankId;
    }

    $scope.Save = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {

            $scope.Loan.MemberId = $scope.Member.Id;
            Calc();
            $http({
                method: 'POST',
                url: '/Cashiering/AddLoan',
                data: $scope.Loan,
                async: false,
            }).success(function (data, status, headers, config) {
                alert("Save Loan success!");
                //switch to 
                $location.path('/Cashiering/LoanList');

            })
        }
    }

    $scope.Cancel = function () {
        $location.path('/Cashiering/LoanList')
    };

    $scope.Recalculate = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {
            Calc();
        }
    }

    function Calc() {

        //amount 
        var amountInterest = ($scope.Loan.LoanAmount * ($scope.Loan.InterestRate / 12 / 100));
        var amount = parseFloat($scope.Loan.LoanAmount) + (amountInterest * $scope.Loan.LoanTerm);
        //set value to CalculatedMonthlyPayment
        $scope.Loan.CalculatedMonthlyPayment = Math.round(amount / $scope.Loan.LoanTerm * 100) / 100;
        //set value to Total Paid
        if ($scope.Loan.TotalPaid <= 0 || $scope.Loan.TotalPaid == undefined) {
            $scope.Loan.TotalPaid = 0;
        }
        //Total Principal Paid 
        $scope.Loan.TotalPrincipalPaid = $scope.Loan.TotalPrincipalPaid == 0 || $scope.Loan.TotalPrincipalPaid == undefined ? 0.0 : $scope.Loan.TotalPrincipalPaid;
        // Math.round($scope.Loan.LoanAmount / $scope.Loan.LoanTerm *100)/100;
        //Total Interest Paid 
        $scope.Loan.TotalInterestPaid = $scope.Loan.TotalInterestPaid != undefined ? $scope.Loan.TotalInterestPaid : 0.0;// Math.round(amountInterest*100)/100;
        //Current Balance 
        $scope.Loan.CurrentBalance = Math.round((amount - $scope.Loan.TotalPaid) * 100) / 100;
    }

    //watch StartDate  
    $scope.$watch('Loan.StartDate', function (newVal) {
        if (newVal != null && $scope.Loan.LoanTerm != undefined && $scope.Loan.LoanTerm > 0) {
            var startDate = new Date(newVal);
            startDate.setMonth(startDate.getMonth() + $scope.Loan.LoanTerm);
            $scope.Loan.EndDate = startDate.getMonth() + 1 + '/' + startDate.getDate() + '/' + startDate.getFullYear();
        }
    });

    $scope.$watch('Loan.LoanTerm', function (newVal) {
        if (newVal != null && $scope.Loan.StartDate != undefined) {
            var startDate = new Date($scope.Loan.StartDate);
            startDate.setMonth(startDate.getMonth() + newVal);
            $scope.Loan.EndDate = startDate.getMonth() + 1 + '/' + startDate.getDate() + '/' + startDate.getFullYear();
        }
    });

    //watch medallion 
    $scope.$watch('Loan', function (newMedallion) {
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
            $scope.Loan.InterestRate = arr[1].InterestRate;
            $scope.Loan.BankId = arr[1].BankId;
        }
    });
};

var EditLoanController = function ($http, $scope, $location, $q, CashieringService) {
    var deferMedallion = $q.defer();
    var deferCompany = $q.defer();

    //retrieve loan to edit
    $scope.Loan = CashieringService.getLoan();

    $scope.CompanySetting = CashieringService.CompanySetting($http, $scope, $q);
    $scope.MedallionList = CashieringService.MedallionList($http, $scope, $q);
    $scope.BankList = CashieringService.BankList($http, $scope);

    $scope.IsSubmitted = false;
    $scope.$watch('Loan.LoanAmount', function (newval) {
        if (newval != undefined && $scope.Loan.CurrentBalance < 0) {
            $scope.Loan.CurrentBalance = newval;
        }
    });
    //event onchange Medallion combobox
    $scope.MedallionChange = function (medallionId) {
        $scope.IsSubmitted = false;
        $scope.Loan.MedallionId = medallionId;
        if ($scope.Loan.BankId != undefined)
            $scope.Loan.BankId = $scope.CompanySetting.BankId;
    }

    $scope.Save = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {

            $scope.Loan.MemberId = $scope.Member.Id;
            Calc();
            $http({
                method: 'POST',
                url: '/Cashiering/EditLoan',
                data: $scope.Loan,
                async: false,
            }).success(function (data, status, headers, config) {
                alert("Save Loan success!");
                //switch to 
                $location.path('/Cashiering/LoanList');

            })
        }
    }

    $scope.Cancel = function () {
        $location.path('/Cashiering/LoanList')
    };

    $scope.Recalculate = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {
            Calc();
        }
    }

    function Calc() {

        //amount 
        var amountInterest = ($scope.Loan.LoanAmount * ($scope.Loan.InterestRate / 12 / 100));
        var amount = parseFloat($scope.Loan.LoanAmount) + (amountInterest * $scope.Loan.LoanTerm);
        //set value to CalculatedMonthlyPayment
        $scope.Loan.CalculatedMonthlyPayment = Math.round(amount / $scope.Loan.LoanTerm * 100) / 100;
        //set value to Total Paid
        if ($scope.Loan.TotalPaid <= 0 || $scope.Loan.TotalPaid == undefined) {
            $scope.Loan.TotalPaid = 0;
        }
        //Total Principal Paid 
        $scope.Loan.TotalPrincipalPaid = $scope.Loan.TotalPrincipalPaid == 0 || $scope.Loan.TotalPrincipalPaid == undefined ? 0.0 : $scope.Loan.TotalPrincipalPaid;// Math.round($scope.Loan.LoanAmount / $scope.Loan.LoanTerm *100)/100;
        //Total Interest Paid 
        $scope.Loan.TotalInterestPaid = $scope.Loan.TotalInterestPaid != undefined ? $scope.Loan.TotalInterestPaid : 0.0;// Math.round(amountInterest*100)/100;
        //Current Balance 
        $scope.Loan.CurrentBalance = Math.round((amount - $scope.Loan.TotalPaid) * 100) / 100;
    }

    //watch StartDate  
    $scope.$watch('Loan.StartDate', function (newVal) {
        if (newVal != null && $scope.Loan.LoanTerm != undefined && $scope.Loan.LoanTerm > 0) {
            var startDate = new Date(newVal);
            startDate.setMonth(startDate.getMonth() + $scope.Loan.LoanTerm);
            $scope.Loan.EndDate = startDate.getMonth() + 1 + '/' + startDate.getDate() + '/' + startDate.getFullYear();
        }
    });

    $scope.$watch('Loan.LoanTerm', function (newVal) {
        if (newVal != null && $scope.Loan.StartDate != undefined) {
            var startDate = new Date($scope.Loan.StartDate);
            startDate.setMonth(startDate.getMonth() + newVal);
            $scope.Loan.EndDate = startDate.getMonth() + 1 + '/' + startDate.getDate() + '/' + startDate.getFullYear();
        }
    });

    //watch medallion 
    $scope.$watch('Loan', function (newMedallion) {
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
            $scope.Loan.InterestRate = arr[1].InterestRate;
            $scope.Loan.BankId = arr[1].BankId;
        }
    });
};
