var AddMemberInfoController = function ($scope, $http,$location, MemberService) {
    $scope.Status = MemberService.Status();
    MemberService.States().then(function (result) { $scope.States = result; });;
    $scope.PreferPayments = MemberService.PreferPayments();
    $scope.IsSubmitted = false;
    $scope.Member = {
        'AccountNumber': '',
        'Name': '',
        'Address': '',
        'Address1': '',
        'City': '',
        'State': '',
        'Zip': '',
        'Phone1': '',
        'Phone2': '',
        'Fax': '',
        'Email': '',
        'SSN': '',
        'IRIS': '',
        'Article': '',
        'Fein': '',         
        'Status': 'Active',
        'UseThisAddressForBilling': '',
        'PreferPayment': 'Weekly',
        'LateBreaks': 5,
        'DefaultWorkerComp': 35,
        'RegisteredAgentName': '',
        'AgentAddress': '',
        'AgentAddress1': '',
        'AgentCity': '',
        'AgentState': '',
        'AgentZip': '',
        'ReciepMessage': ''
    };


    $scope.Cancel = function () {

        $("#search").attr("class", "active");
        $("#info").attr("class", "");
        $("#cash").attr("class", "");
        $location.path("/Member/Search");
    }

    //save member 
    $scope.Save = function (inValid) {
        $scope.IsSubmitted = true;
        if (!inValid) {
            //save bank
            $http({
                method: 'POST',
                url: '/Member/CreateMemberInfo',
                data: $scope.Member,
                async: false
            }).success(function (data, status, headers, config) {
                alert('Save Member Info success!');
                $("#search").attr("class", "active");
                $("#info").attr("class", "");
                $("#cash").attr("class", "");
                $location.path('/Member/Search');
            });
        }
    };

};
