var PaymemtHistoryReportController = function ($scope, $q, $http, $location, MemberService) {

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
    ];
    //end config

    $scope.report = {'MemberStatus':'All'};
    $scope.IsSubmitted = false;
    // Load Members
    GetMembers($http, $scope, $q).then(function (result) {
        $scope.MemberList = result;
    });

    //Load Fee Types
    GetFeeTypes($http, $scope, $q).then(function (result) {
        $scope.report.FeeTypes = result;
    });

    //  Load Payment Types
    GetPaymentTypes($http, $scope, $q).then(function (result) {
        $scope.report.PaymentTypes = result;

    });

    // Load Medallion per member
    $scope.LoadMemberMedallion = function (memberId) {
        GetMedallionOfMember($http, $scope, memberId, $q).then(function (result) {
            $scope.Medallions = result;
        });
    }

    // All/None Fee Type
    $scope.CheckFeeType = function (value) {
        $('input', $('#feeDiv')).each(function () {
            this.checked = value;
        });
    }

    // All/None Payment Type
    $scope.CheckPaymentType = function (value) {
        $('input', $('#paymentDiv')).each(function () {
            this.checked = value;
        });
    }

    $scope.GenerateReport = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {
            //gen report
            $http({
                method: 'POST',
                url: '/Report/ShowPaymentHistory',
                data: { parameter: $scope.report },
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.TransactionHistoryList = data;
            });
        }
    }



    function GetMembers($http, $scope, $q) {
        var qdefer = $q.defer();
        $http({
            method: 'POST',
            url: '/Member/GetMemberList',
            async: true,
        }).success(function (data, status, headers, config) {
            qdefer.resolve(data);
        });
        return qdefer.promise;
    };

    function GetMedallionOfMember($http, $scope, memberId, $q) {
        var qdefer = $q.defer();
        $http({
            method: 'POST',
            url: '/Member/GetMedallions',
            data: { memberId: memberId },
            async: true,
        }).success(function (data, status, headers, config) {
            qdefer.resolve(data);
        })
        return qdefer.promise;
    };


    function GetFeeTypes($http, $scope, $q) {
        var qdefer = $q.defer();
        $http({
            method: 'GET',
            url: '/Member/GetFeeTypes',
            async: true,
        }).success(function (data, status, headers, config) {
            qdefer.resolve(data);
        })
        return qdefer.promise;
    };


    function GetPaymentTypes($http, $scope, $q) {
        var qdefer = $q.defer();
        $http({
            method: 'GET',
            url: '/Member/GetPaymentTypes',
            async: true,
        }).success(function (data, status, headers, config) {
            qdefer.resolve(data);
        })
        return qdefer.promise;
    };


};

