
var TransactionHistoryController = function ($http, $scope, $location, $q, CashieringService) {

    CashieringService.TransactionHistoryList($http, $scope);

    //config paging table
    $scope.aoColumns = [
            { "sTitle": "#" },
            { "sTitle": "Medallion #" },
            { "sTitle": "Due Date" },
            { "sTitle": "Date Received" },
            { "sTitle": "Interval" },
            { "sTitle": "SubTotal Charges" },
            { "sTitle": "SubTotal Credit & Payments" },
             {
                 "sTitle": "Total",
                 "fnRender": function (obj) {
                     var history = obj.aData;
                     var action = history.Subtotal + history.Credit + history.CreditCardAmount + history.CreditCardFee;

                     return action;
                 }
             },
{
    "sTitle": "",
    "fnRender": function (obj) {
        var sReturn = obj.aData[obj.iDataColumn];
        var action = " <a href=\"\" ng-show=\"$first\" onclick=\"angular.element(this).scope().EditTransaction('" + obj.aData.Id + "')\">" +
                    "<i class=\"fa fa-edit\"></i>" +
                "</a>" +
                "<a href=\"\" onclick=\"angular.element(this).scope().DeleteTransaction('" + obj.aData.Id + "')\">" +
                "<i class=\"fa fa-ban\"></i></a>";

        return action;
    }
}
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "RecieptNumber", "aTargets": [0] },
                        { "mDataProp": "MedallionNumber", "aTargets": [1] },
                        { "mDataProp": "TransactionDate", "aTargets": [2] },
                        { "mDataProp": "DateReceived", "aTargets": [3] },
                        { "mDataProp": "Interval", "aTargets": [4] },
                        { "mDataProp": "TotalDueAmount", "aTargets": [5] },
                        { "mDataProp": "Subtotal", "aTargets": [6] },
                        { "mDataProp": "TotalPaid", "aTargets": [7] },
                        { "mDataProp": "Id", "aTargets": [8] }
    ];
    //end config

    $scope.BackToList = function () {
        window.location.href = '#/Cashiering/AccountSummary';
        //$location.path('/Cashiering/AccountSummary');
    }

    $scope.EditTransaction = function (id) {
        var u = $.grep($scope.TransactionHistoryList, function (e) { return e.Id == id; });
        var transactionHistory = u[0];
        CashieringService.setTransactionHistory(transactionHistory);
        window.location.href = '#/Cashiering/EditTransactionHistory';
        //$location.path('/Cashiering/EditTransactionHistory');
    }

    $scope.DeleteTransaction = function (id) {
        var u = $.grep($scope.TransactionHistoryList, function (e) { return e.Id == id; });
        var transactionHistory = u[0];
        if (transactionHistory != undefined) {
            if (confirm("Are you sure you want to delete this transaction?")) {
                $http({
                    method: 'POST',
                    url: '/Cashiering/DeleteTransaction',
                    data: { transactionId: transactionHistory.Id },
                    async: false,
                }).success(function (data, status, headers, config) {
                    if (data) {
                        alert("Deleted Transaction success!")
                        CashieringService.TransactionHistoryList($http, $scope);
                    }
                    else {
                        alert("Occured error during deleted transaction");
                    }
                })
            }
        }
    }
}

var MedallionSummaryController = function ($http, $scope, $location, $q, CashieringService) {

    $scope.MedallionSummaryList = CashieringService.MedallionSummaryList($http, $scope);

    //config paging table
    $scope.aoColumns = [
            { "sTitle": "Med #" },
            { "sTitle": "Status" },
            { "sTitle": "Amount Owed" },
            { "sTitle": "Days late" },
            { "sTitle": "Paid Till Date" },
            { "sTitle": "Payment Due Date" },
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "<a href=\"\" class=\"btn default btn-xs purple\" onclick=\"angular.element(this).scope().Change('" + obj.aData.MedallionId + "')\">"
                                     + "<i class=\"fa fa-edit\">Change</i>    </a>"
                                    + "<a href=\"#/Cashiering/NewCashierMember\" class=\"btn default btn-xs purple\" onclick=\"angular.element(this).scope().CashierMember('" + obj.aData.MedallionId + "')\">"
                                     + "<i class=\"fa fa-video-camera\">Cashier Member</i>      </a>"
                                    + "<a href=\"\" class=\"btn default btn-xs purple\" onclick=\"angular.element(this).scope().CashierAgent('" + obj.aData.MedallionId + "')\">"
                                     + "<i class=\"fa fa-road\">Cashier Agent</i></a>"
                                    + "<a href=\"#/Cashiering/PaymentHistory\" class=\"btn default btn-xs purple\" onclick=\"angular.element(this).scope().ViewPaymentHistory('" + obj.aData.MedallionId + "')\">"
                                     + "<i class=\"fa fa-power-off\">Payment History</i>"
                                    + "</a>";

                    return action;
                }
            }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "MedallionNumber", "aTargets": [0] },
                        { "mDataProp": "Status", "aTargets": [1] },
                        { "mDataProp": "CurrentBalance", "aTargets": [2] },
                        { "mDataProp": "DateLates", "aTargets": [3] },
                        { "mDataProp": "PayTillDate", "aTargets": [4] },
                        { "mDataProp": "PaymentDueDate", "aTargets": [5] },
                        { "mDataProp": "MedallionId", "aTargets": [6] }
    ];
    //end config

    $scope.SummaryReceipt = false;
    $scope.TwoPartReceipt = false;

    $scope.$watch('MemberService.Member', function (newval) {
        if (newval != undefined) {
            $scope.Member = newval;
        }
    });

    //view payment History 
    $scope.ViewPaymentHistory = function (id) {
        var u = $.grep($scope.MedallionSummaryList, function (e) { return e.MedallionId == id; });
        var medallion = u[0];
        CashieringService.setMedallion(medallion);
        window.location.href = '#/Cashiering/PaymentHistory';
        //$location.path('/Cashiering/PaymentHistory');
    };
    //view payment History 
    $scope.Change = function (id) {
        var u = $.grep($scope.MedallionSummaryList, function (e) { return e.MedallionId == id; });
        var medallion = u[0];
        confirm("Are you sure to postponse the due date?");
    };
    //view payment History 
    $scope.CashierMember = function (id) {
        var u = $.grep($scope.MedallionSummaryList, function (e) { return e.MedallionId == id; });
        var medallion = u[0];
        CashieringService.setMedallion(medallion);
        window.location.href = '#/Cashiering/NewCashierMember';
        //$location.path('/Cashiering/NewCashierMember');
    };

    //view payment History 
    $scope.CashierAgent = function (id) {
        var u = $.grep($scope.MedallionSummaryList, function (e) { return e.MedallionId == id; });
        var medallion = u[0];

    };
};

var NewCashierMemberController = function ($http, $scope, $location, $q, CashieringService) {
    $scope.IsReInitted = false;
    $scope.Interval = 0;
    $scope.TransactionType = 'M';


    $scope.TransactionHistory = CashieringService.getTransactionHistory();
    //get MetricList
    CashieringService.MetricList($http, $scope);
    //get new bill 
    CashieringService.NewBill($http, $scope);

    $scope.addWatch = function () {
        $scope.$watchCollection('NewBill.Bill', function (newval, oldval) {
            if (newval != oldval && newval != undefined) {
                $scope.NewBill.Bill.Subtotal = Math.round(($scope.NewBill.Bill.Cash
                    + $scope.NewBill.Bill.Check
                    + $scope.NewBill.Bill.Credit
                    + $scope.NewBill.Bill.CreditCardAmount
                    + $scope.NewBill.Bill.CreditCardFee) * 100) / 100;

                $scope.NewBill.Bill.TotalPaidAmount = Math.round(($scope.NewBill.Bill.Cash + $scope.NewBill.Bill.Check) * 100) / 100;

                $scope.NewBill.Bill.TotalDueAmount =
                   Math.round(($scope.NewBill.Bill.AssociationDueAmount
                   + $scope.NewBill.Bill.CollsionInsuranceAmount
                   + $scope.NewBill.Bill.WorkerCompensationAmount
                   + $scope.NewBill.Bill.InsuranceSurchargeAmount
                   + $scope.NewBill.Bill.Loan
                   + $scope.NewBill.Bill.AccountReceivableAmount
                   + $scope.NewBill.Bill.InsuranceDepositAmount
                   + $scope.NewBill.Bill.WerkReceivableAmount
                   + $scope.NewBill.Bill.CCSystemAirtimeAmount
                   + $scope.NewBill.Bill.MiscCharge + $scope.NewBill.Bill.LateFees) * 100) / 100;

                $scope.NewBill.Bill.TotalPaid = Math.round(($scope.NewBill.Bill.TotalPaidAmount + $scope.NewBill.Bill.TotalDueAmount) * 100) / 100;

                if ($scope.IsZeroOut) {
                    $scope.NewBill.Bill.SavingDepositAmount = Math.round(($scope.NewBill.Bill.TotalPaid) * 100) / 100;
                }
                else {
                    $scope.NewBill.Bill.TotalPaid = Math.round(($scope.NewBill.Bill.TotalPaid + $scope.NewBill.Bill.SavingDepositAmount) * 100) / 100;
                }
            }
        });
    }

    $scope.Submitted = false;
    $scope.CancelPayment = function () {
        window.location.href = '#/Cashiering/AccountSummary';
        // $location.path('/Cashiering/AccountSummary');
    }
    $scope.Save = function (inValid) {
        $scope.Submitted = true;
        if (!inValid) {
            if ($scope.IsZeroOut) {
                if ($scope.NewBill.Bill.TotalPaid > $scope.SavingDepositZero) {
                    alert("You don't have enough money to use Zero-Out feature.")
                    return;
                }
            }

            $scope.NewBill.Bill.Interval = $scope.Interval;
            $scope.NewBill.Bill.TransactionType = $scope.TransactionType;
            $http({
                method: 'POST',
                url: '/Cashiering/SaveNewBill',
                data: { bill: $scope.NewBill.Bill, isZeroOut: $scope.IsZeroOut },
                async: false,
            }).success(function (data, status, headers, config) {
                alert("Save Bill success!");
                //switch to 
                $location.path('/Cashiering/AccountSummary');
            })

        }
    }
    $scope.MakeCurrent = function () {
        $http({
            method: 'POST',
            url: '/Cashiering/MakeCurrent',
            data: {
                bill: $scope.NewBill.Bill
            },
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.NewBill.Bill = data;
        })
    }
    $scope.ReInitialize = function (invalid) {
        $scope.IsReInitted = true;
        if (!invalid) {
            $http({
                method: 'POST',
                url: '/Cashiering/ReInitialize',
                data: {
                    interval: $scope.Interval, transactionType: $scope.TransactionType, memberId: $scope
                    .NewBill.Member.Id, medallionId: $scope.NewBill.Medallion.Id
                },
                async: false,
            }).success(function (data, status, headers, config) {
                //switch to 
                CashieringService.MetricList($http, $scope);
                CashieringService.NewBill($http, $scope);
            })
        }
    }

    $scope.Recalculate = function () { }
    $scope.IsZeroOut = false;
    $scope.SavingDepositZero = 0.0;
    $scope.ZeroOut = function () {

        $http({
            method: 'POST',
            url: '/Cashiering/ZeroOut',
            data: {
                bill: $scope.NewBill.Bill
            },
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.IsZeroOut = true;
            //switch to 
            $scope.SavingDepositZero = data;
            //$scope.NewBill.Bill.Credit = 0;
            //$scope.NewBill.Bill.Check = 0;
            //$scope.NewBill.Bill.Cash = 0;
            //$scope.NewBill.Bill.CreditCardAmount = 0;
            //$scope.NewBill.Bill.CreditCardFee = 0;
            //$scope.NewBill.Bill.AssociationDueAmount = 0;
            //$scope.NewBill.Bill.CollsionInsuranceAmount = 0;
            //$scope.NewBill.Bill.WorkerCompensationAmount = 0
            //$scope.NewBill.Bill.InsuranceSurchargeAmount = 0;
            //$scope.NewBill.Bill.MedallionLoanAmount = 0;
            //$scope.NewBill.Bill.AutoLoanAmount = 0;
            //$scope.NewBill.Bill.AccountReceivableAmount = 0;
            //$scope.NewBill.Bill.InsuranceDepositAmount = 0;
            //$scope.NewBill.Bill.WerkReceivableAmount = 0;
            //$scope.NewBill.Bill.CCSystemAirtimeAmount = 0;
            //$scope.NewBill.Bill.SavingDepositAmount = 0;
            //$scope.NewBill.Bill.MiscCharge = 0;
            //$scope.NewBill.Bill.LateFees = 0;
        })
    }
    $scope.TurnOffZeroOut = function () {
        $scope.IsZeroOut = false;
    }
}

var EditTransactionHistoryController = function ($http, $scope, $location, $q, CashieringService) {

    $scope.TransactionHistory = CashieringService.getTransactionHistory();
    //get MetricList
    CashieringService.MetricList($http, $scope);

    //get Edit Bill 
    CashieringService.EditBill($http, $scope);

    //watch for onchange all textbox amount on form
    $scope.addWatch = function () {
        $scope.$watchCollection('NewBill.Bill', function (newval, oldval) {
            if (newval != oldval && newval != undefined) {
                //get total of cash and check
                $scope.NewBill.Bill.Subtotal = Math.round($scope.NewBill.Bill.Cash
                    + $scope.NewBill.Bill.Check
                    + $scope.NewBill.Bill.Credit
                    + $scope.NewBill.Bill.CreditCardAmount
                    + $scope.NewBill.Bill.CreditCardFee);

                $scope.NewBill.Bill.TotalPaidAmount = Math.round($scope.NewBill.Bill.Cash + $scope.NewBill.Bill.Check);

                $scope.NewBill.Bill.TotalDueAmount =
                   Math.round($scope.NewBill.Bill.AssociationDueAmount
                   + $scope.NewBill.Bill.CollsionInsuranceAmount
                   + $scope.NewBill.Bill.WorkerCompensationAmount
                   + $scope.NewBill.Bill.InsuranceSurchargeAmount
                   + $scope.NewBill.Bill.Loan
                   + $scope.NewBill.Bill.AccountReceivableAmount
                   + $scope.NewBill.Bill.InsuranceDepositAmount
                   + $scope.NewBill.Bill.WerkReceivableAmount
                   + $scope.NewBill.Bill.CCSystemAirtimeAmount
                   + $scope.NewBill.Bill.MiscCharge + $scope.NewBill.Bill.LateFees);

                $scope.NewBill.Bill.TotalPaid = Math.round($scope.NewBill.Bill.TotalPaidAmount +
                    $scope.NewBill.Bill.TotalDueAmount);

                if ($scope.IsZeroOut) {
                    $scope.NewBill.Bill.SavingDepositAmount = Math.round($scope.NewBill.Bill.TotalPaid);
                }
                else {
                    $scope.NewBill.Bill.TotalPaid = Math.round($scope.NewBill.Bill.TotalPaid + $scope.NewBill.Bill.SavingDepositAmount);
                }
            }
        });
    }

    $scope.Submitted = false;
    $scope.CancelPayment = function () {
        window.location.href = "#/Cashiering/PaymentHistory";
        //$location.path('/Cashiering/PaymentHistory');
    }
    $scope.Save = function (inValid) {
        $scope.Submitted = true;
        if (!inValid) {
            $scope.NewBill.Bill.IsZeroOut = $scope.IsZeroOut;
            $http({
                method: 'POST',
                url: '/Cashiering/SaveEditBill',
                data: { bill: $scope.NewBill.Bill, transactionId: $scope.TransactionHistory.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    alert("Update Bill success!");
                    //switch to 

                    $location.path('/Cashiering/PaymentHistory');
                }
                else {
                    alert("Update Bill Fail!");
                }
            })
        }
    }

    $scope.MakeCurrent = function () {
        $http({
            method: 'POST',
            url: '/Cashiering/MakeCurrent',
            data: {
                bill: $scope.NewBill.Bill
            },
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.NewBill.Bill = data;
        })
    }
    $scope.ReInitialize = function (invalid) {

        $scope.IsReInitted = true;
        if (!invalid) {
            $http({
                method: 'POST',
                url: '/Cashiering/ReInitialize',
                data: {
                    interval: $scope.NewBill.Bill.Interval, transactionType: $scope.NewBill.Bill.TransactionType, memberId: $scope
                    .NewBill.Member.Id, medallionId: $scope.NewBill.Medallion.Id
                },
                async: false,
            }).success(function (data, status, headers, config) {
                //switch to 
                CashieringService.MetricList($http, $scope);
                CashieringService.NewBill($http, $scope);
            })
        }
    }

    $scope.Recalculate = function () { }
    $scope.IsZeroOut = false;
    $scope.SavingDepositZero = 0.0;
    $scope.ZeroOut = function () {

        $http({
            method: 'POST',
            url: '/Cashiering/ZeroOut',
            data: {
                bill: $scope.NewBill.Bill
            },
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.IsZeroOut = true;
            //switch to 
            $scope.SavingDepositZero = data;

        })
    }
    $scope.TurnOffZeroOut = function () {
        $scope.IsZeroOut = false;
    }
}
