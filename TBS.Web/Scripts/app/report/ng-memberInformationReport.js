
var MemberInformationReportController = function ($scope, $http, $q, $location) {
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
                url: '/Report/ShowMemberInformationReport',
                data: { parameter: $scope.report },
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.DataSourceList = listReport;
            });
        }
    }

    // Load Medallion per member
    $scope.LoadMemberMedallion = function (memberId) {
        GetMedallionOfMember($http, $scope, memberId, $q).then(function (result) {
            $scope.Medallions = result;
        });
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

}
 