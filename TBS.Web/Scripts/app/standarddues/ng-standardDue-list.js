var StandardDueListController = function ($scope, $http, $modal, MemberService) {
    $scope.Member = MemberService.getMember();
    MemberService.StandardDueList($http, $scope);
    //start config paging
    $scope.aoColumns = [
        { "sTitle": "Start Date" },
        { "sTitle": "Dues" },       
        {
            "sTitle": "",
            "fnRender": function (obj) {
                var sReturn = obj.aData[obj.iDataColumn];
                var action = "<a href=\"\" onclick=\"angular.element(this).scope().EditStandardDue('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                            "<a href=\"\" onclick=\"angular.element(this).scope().DeleteStandardDue('" + obj.aData.Id + "')\"><i class=\"fa fa-ban\"></i></a>";

                return action;
            }
        }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "StartDate", "aTargets": [0] },
                        { "mDataProp": "Dues", "aTargets": [1] },
                       
                        { "mDataProp": "Id", "aTargets": [2] }
    ];

    //end config paging 
    $scope.EditStandardDue = function (id) {
        var u = $.grep($scope.StandardDueList, function (e) { return e.Id == id; });
        var standardDueItem = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/Member/EditStandardDue',
            controller: 'EditStandardDueController',
            resolve: {
                standardDueItem: function () {
                    return angular.copy(standardDueItem);
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            MemberService.StandardDueList($http, $scope);

        });
    };

    $scope.AddStandardDue = function () {
        var standardDueItem = {
            'StartDate': '',
            'Dues': ''
        };
        standardDueItem.MemberId = $scope.Member.Id;
        var modalInstance = $modal.open({
            templateUrl: '/Member/CreateStandardDue',
            controller: 'AddStandardDueController',
            resolve: {
                standardDueItem: function () {
                    return standardDueItem;
                }
            }
        });

        //modal opened
        modalInstance.opened.then(function () {
            //ComponentsFormTools.init();
        });
        //reload list
        modalInstance.result.then(function () {
            MemberService.StandardDueList($http, $scope);
        });
    };

    $scope.DeleteStandardDue = function (id) {
        var u = $.grep($scope.StandardDueList, function (e) { return e.Id == id; });
        var standardDueItem = u[0];
        if (confirm("Are you sure you want to delete this item?")) {
            $http({
                method: 'POST',
                url: '/Member/DeleteStandardDue',
                data: standardDueItem,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.StandardDueList = jQuery.grep($scope.StandardDueList, function (value) {
                        return value.Id != standardDueItem.Id;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};