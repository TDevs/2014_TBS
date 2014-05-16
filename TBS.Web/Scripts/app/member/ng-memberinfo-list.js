
var MemberController = function ($http, $scope, $location, MemberService) {


    $scope.TabSearch = function (event) {        
        $("#search").attr("class", "active");
        $("#info").removeAttr("class");
        $("#cash").removeAttr("class");
        window.location.href = ('#/Member/SearchMember');
    }
    $scope.TabMemberInfo = function () {
        if (MemberService.getMember() == undefined || MemberService.getMember() == "") {
            alert('Member not found')
        }
        $("#search").removeAttr("class");
        $("#info").attr("class", "active");
        $("#cash").removeAttr("class");
        $location.path('/Member/EditInfo');

    }
    $scope.TabCashiering = function () {
        if (MemberService.getMember() == undefined || MemberService.getMember() == "") {
            alert('Member not found');
        }

        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Cashiering/AccountSummary');
    }

    $scope.ContactClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path('/Member/ListContact');
    }
    $scope.VehicleClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path('/Member/Vehicle');
    }
    $scope.MedallionClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path('/Member/MedallionListing');
    }
    $scope.AgentClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path('/Member/ListAgent');
    }
    $scope.AssociationDueClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path('/Member/StandardDueListing');
    }
    $scope.InfoClick = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path('/Member/EditInfo');
    }

    $scope.IsMainSubmited = false;
    $scope.MemberService = MemberService;


    $scope.$watch('MemberService.getAccountNumber()', function (newVal) {
        $scope.AccountNumber = newVal;
        $scope.Member = MemberService.getMember();
    });


    $scope.$watch(function () { return MemberService.MemberName }, function (memberName) {
        $scope.MemberName = memberName;
        $scope.Member = MemberService.getMember();
    }, true);

    $scope.Go = function (invalid) {
        $scope.IsMainSubmited = true;
        if (!invalid) {
            $http({
                method: 'POST',
                url: '/Member/GetMemberByAccountName',
                data: { accountNumber: $scope.AccountNumber },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data != undefined && data != "") {
                    //set current member
                    MemberService.setMember(data.Member);
                    MemberService.setAccountNumber(data.AccountNumber);
                    MemberService.setMemberName(data.Name);
                    MemberService.setReturnToMemberInfo(true);
                    //reload tab member
                    $("#search").attr("class", "");
                    $("#info").attr("class", "active");
                    $("#cash").attr("class", "");
                    $location.path('/Member/EditInfo');
                }
                else {
                    alert("Not Found Any Members!");
                }
            })
        }
    }

    $scope.selected = undefined;
    MemberService.AccountNumberMemberList($http, $scope).then(function (result) {
        $scope.MemberList = result;
    });

    $scope.getLocation = function (val) {
        return $http.get('http://maps.googleapis.com/maps/api/geocode/json', {
            params: {
                address: val,
                sensor: false
            }
        }).then(function (res) {
            var addresses = [];
            angular.forEach(res.data.results, function (item) {
                addresses.push(item.formatted_address);
            });
            return addresses;
        });
    };

}
var MemberListController = function ($http, $scope, $location, MemberService) {


    //start config paging
    $scope.aoColumns = [
        { "sTitle": "Account #" },
        { "sTitle": "Member Name" },
        { "sTitle": "Phone" },
        {
            "sTitle": "Status",
            "fnRender": function (obj) {
                var sReturn = obj.aData;
                if (sReturn != null && sReturn.Member != null && !sReturn.Member.IsDeteled) {
                    return sReturn.Status;
                }
                else (sReturn != null && sReturn.Member != null && sReturn.Member.IsDeteled)
                {
                    return "Deleted";
                }
                return "";
            }
        },
        { "sTitle": "Payment Status" },
        { "sTitle": "Payment Due Date" },
        {
            "sTitle": "",
            "fnRender": function (obj) {
                var sReturn = obj.aData[obj.iDataColumn];
                var action = "<a href=\"#/Member/EditInfo\"  ng-show=\"!searchmember.Member.IsDeleted\" onclick=\"angular.element(this).scope().EditMember('" + obj.aData.Member.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                            "<a href=\"\" ng-show=\"!searchmember.Member.IsDeleted\" onclick=\"angular.element(this).scope().DeleteMember('" + obj.aData.Member.Id + "')\"><i class=\"fa fa-ban\"></i></a>";

                return action;
            }
        }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "AccountNumber", "aTargets": [0] },
                        { "mDataProp": "MemberName", "aTargets": [1] },
                        { "mDataProp": "Phone", "aTargets": [2] },
                         { "mDataProp": "Status", "aTargets": [3] },
                        { "mDataProp": "PreferPayment", "aTargets": [4] },
                               { "mDataProp": "PaymentDueDate", "aTargets": [5] },
                        { "mDataProp": "MemberId", "aTargets": [6] }
    ];

    //end config paging 


    if (MemberService.getReturnToMemberInfo()) {
        //reset flag ReturnToMemberInfo 
        MemberService.setReturnToMemberInfo(false);
        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path('/Member/EditInfo');
    }

    //get company setting
    $scope.CompanySetting = [];
    MemberService.CompanySetting($http, $scope).then(function (result) {
        $scope.CompanySetting = result;
    });

    $scope.ListSearchBy = MemberService.ListSearchBy();
    $scope.CriterialSearch = MemberService.getCriterialSearch();

    if ($scope.CriterialSearch == undefined) {
        $scope.CriterialSearch = {
            'SearchBy': 1,
            'ViewDeleted': true,
            'Keyword': ''
        };
        SettingDefaultValue($scope);
    }
    else {
        $scope.CriterialSearch.SearchBy = parseInt($scope.CriterialSearch.SearchBy);
        FindRetain($http, $scope);
    }

    $scope.SearchAgent = function () {
        window.location.href = '/Agent/Search#Agent/Search';
        //$location.path('/Agent/SearchAgent');
    };

    $scope.SearchMemberList = [];



    $scope.EditMember = function (id) {
        var u = $.grep($scope.SearchMemberList, function (e) { return e.Member.Id == id; });
        var member = u[0].Member;
        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");

        //set member to service
        MemberService.setMember(member);
        MemberService.setAccountNumber(member.AccountNumber);
        MemberService.setMemberName(member.Name);
        //set this flag to research when turn around
        MemberService.setReturnToMemberInfo(true);
        //redirect to Edit member
        window.location.href = ('#/Member/EditInfo');
    };

    $scope.AddMember = function () {
        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");

        //set this flag to research when turn around
        MemberService.setReturnToMemberInfo(true);

        $location.path('/Member/NewInfo');
    };

    $scope.DeleteMember = function (id) {
        var u = $.grep($scope.SearchMemberList, function (e) { return e.Member.Id == id; });
        var member = u[0].Member;
        if (confirm("Are you sure you want to delete this member?")) {
            $http({
                method: 'POST',
                url: '/Member/DeleteMemberInfo',
                data: member,
                async: false,
            }).success(function (data, status, headers, config) {

                if (data == "true") {
                    FindRetain($http, $scope);
                }
                else {
                    alert("This member is inused");
                }
            })
        }
    };

    $scope.Find = function () {
        MemberService.setCriterialSearch($scope.CriterialSearch);
        if ($scope.CriterialSearch.Keyword == '' || $scope.CriterialSearch.Keyword == '*') {
            $scope.ShowAll();
        } else {
            $http({
                method: 'POST',
                url: '/Member/SearchByCriterial',
                data: $scope.CriterialSearch,
                async: true,
            }).success(function (data, status, headers, config) {
                $scope.SearchMemberList = data;
               // MemberService.setMember(data);
            })
        }
    };

    $scope.ShowAll = function () {
        MemberService.setCriterialSearch($scope.CriterialSearch);
        $http({
            method: 'POST',
            url: '/Member/SearchByAll',
            async: true,
        }).success(function (data, status, headers, config) {
            $scope.SearchMemberList = data;
            //MemberService.setMember(data);
            $scope.CriterialSearch = MemberService.getCriterialSearch();
            $scope.CriterialSearch.SearchBy = 8;
            $scope.CriterialSearch.Keyword = '';
        })
    };

    function ShowAllRetain($http, $scope) {
        $http({
            method: 'POST',
            url: '/Member/SearchByAll',
            async: true,
        }).success(function (data, status, headers, config) {
            $scope.SearchMemberList = data;
            //MemberService.setMember(data);
        })
    }

    function FindRetain($http, $scope) {
        if ($scope.CriterialSearch.Keyword == '' || $scope.CriterialSearch.Keyword == '*') {
            ShowAllRetain($http, $scope);
        } else {
            $http({
                method: 'POST',
                url: '/Member/SearchByCriterial',
                data: $scope.CriterialSearch,
                async: true,
            }).success(function (data, status, headers, config) {
                $scope.SearchMemberList = data;
                //MemberService.setMember(data);
            })
        }
    }

    function SettingDefaultValue($scope) {
        if ($scope.CompanySetting.SearchByAccountNumber) {
            $scope.CriterialSearch.SearchBy = 1;
        }
        else if ($scope.CompanySetting.SearchByMedallionNumber) {
            $scope.CriterialSearch.SearchBy = 2;
        }
        else if ($scope.CompanySetting.SearchByAgentChaufferNumber) {
            $scope.CriterialSearch.SearchBy = 3;
        }
        else if ($scope.CompanySetting.SearchBySSN) {
            $scope.CriterialSearch.SearchBy = 4;
        }
        else if ($scope.CompanySetting.SearchByMemberName) {
            $scope.CriterialSearch.SearchBy = 5;
        }
        else if ($scope.CompanySetting.SearchByMemberContactName) {
            $scope.CriterialSearch.SearchBy = 6;
        }
        else if ($scope.CompanySetting.SearchByAgentLastName) {
            $scope.CriterialSearch.SearchBy = 7;
        }
    };

    $scope.$watch('MemberService.getSearchFilter()', function (newVal) {
        $scope.SearchMemberList = newVal;
    });
}