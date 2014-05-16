var UniversalAgentController = function ($http, $scope, $q, MemberService) {
    $scope.UniversalAgentList = [];
    GetUniversalAgentList();
    function GetUniversalAgentList() {
        $http({
            method: 'POST',
            url: '/Member/GetUniversalAgentList',            
            async: false,
        }).success(function (data, status, headers, config) {
            if (data != undefined && data != "") {
                $scope.UniversalAgentList = data;
            }

        })
    }

    $scope.Refresh = function () {
        var url = "https://data.cityofchicago.org/api/views/97wa-y6ff/rows.json?accessType=DOWNLOAD";
        $http({
            method: 'GET',
            url: url,
            async: true
        }).success(function (data, status, headers, config) {
            if (data != undefined && data != "") {
                var result = data.data;
                var univerlist = []
                for (i = 0; i < 100; i++)
                {
                    var univer = {
                        'Id': result[i][1],
                        'LicenseNumber': result[i][8],
                        'IsRenewed': result[i][9],
                        'Status': result[i][10],
                        'StatusDate': result[i][11],
                        'DriverType': result[i][12],
                        'LicenseType': result[i][13],
                        'OriginalIssueDate': result[i][14],
                        'Name': result[i][15],
                        'Sex': result[i][16],
                        'ChaufferCity': result[i][17],
                        'ChaufferState': result[i][18],
                    };
                    univerlist.push(univer);
                }
                //$scope.UniversalAgentList = univerlist;
                //send to server to save 
                $http({
                    method: 'POST',
                    url: '/Member/Refresh',
                    data: { list: univerlist },
                    async: false,
                }).success(function (data, status, headers, config) {
                    if (data =="true") {
                        GetUniversalAgentList();
                    }

                })

            }

        });

    }
};