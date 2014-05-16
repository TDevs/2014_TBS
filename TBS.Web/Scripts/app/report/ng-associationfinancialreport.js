var AssociationMemberSummaryReportController = function ($scope, $http, $q, $location)
{
    $scope.report = {'MemberStatus':'All'};
    //config paging table
    $scope.aoColumns = [
            { "sTitle": "#" },
            { "sTitle": "Medallion #" },
            
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "Title", "aTargets": [0] },
                        { "mDataProp": "Name", "aTargets": [1] },
                         
    ];
    //end config

    $scope.GenerateReport = function ()
    {
        var listReport = [{ Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }];
        //gen report
        $http({
            method: 'POST',
            url: '/Report/ShowAssociationReport',
            data: { startDate: $scope.StartDate , endDate:$scope.EndDate},
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.AssociationFinancialReportList = listReport;
        });
    }
}

var AssociationMemberSummaryReportController = function ($scope, $http, $q, $location)
{
    $scope.report = { 'MemberStatus': 'All' };
    //config paging table
    $scope.aoColumns = [
            { "sTitle": "#" },
            { "sTitle": "Medallion #" },

    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "Title", "aTargets": [0] },
                        { "mDataProp": "Name", "aTargets": [1] },

    ];
    //end config

    $scope.GenerateReport = function () {
        var listReport = [{ Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }];
        //gen report
        $http({
            method: 'POST',
            url: '/Report/ShowAssociationMemberReport',
            data: { parameter: $scope.report },
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.DataSourceList = listReport;
        });
    }
}


var LateReportController = function ($scope, $http, $q, $location) {
    $scope.report = { 'MemberStatus': 'All' };
    //config paging table
    $scope.aoColumns = [
            { "sTitle": "#" },
            { "sTitle": "Medallion #" },

    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "Title", "aTargets": [0] },
                        { "mDataProp": "Name", "aTargets": [1] },

    ];
    //end config

    $scope.GenerateReport = function () {
        var listReport = [{ Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }];
        //gen report
        $http({
            method: 'POST',
            url: '/Report/ShowLateReport',
            data: { parameter: $scope.report },
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.DataSourceList = listReport;
        });
    }
}



var OutstandingBalanceController = function ($scope, $http, $q, $location) {
  
    //config paging table
    $scope.aoColumns = [
            { "sTitle": "#" },
            { "sTitle": "Medallion #" },

    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "Title", "aTargets": [0] },
                        { "mDataProp": "Name", "aTargets": [1] },

    ];
    //end config

    $scope.GenerateReport = function () {
        var listReport = [{ Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }];
        //gen report
        $http({
            method: 'POST',
            url: '/Report/ShowOutStandingBalanceReport',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.DataSourceList = listReport;
        });
    }
}


var MemberContactListReportController = function ($scope, $http, $q, $location) {
    $scope.report = { 'MemberStatus': 'All' };
    $scope.IsSubmitted = false;
    // Load Members
    GetMembers($http, $scope, $q).then(function (result) {
        $scope.MemberList = result;
    });

    //config paging table
    $scope.aoColumns = [
            { "sTitle": "#" },
            { "sTitle": "Medallion #" },

    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "Title", "aTargets": [0] },
                        { "mDataProp": "Name", "aTargets": [1] },

    ];
    //end config

    $scope.GenerateReport = function (invalid) {
        var listReport = [{ Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }, { Title: 'test', Name: 'Name' }];
        //gen report
        $scope.IsSubmitted = true;
        if (!invalid) {
            //gen report
            $http({
                method: 'POST',
                url: '/Report/ShowMemberContractListReport',
                data: { parameter: $scope.report },
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.DataSourceList = listReport;
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
}