var MedallionListController = function ($scope, $http, $q, $modal, MemberService) {

    //start config paging
    $scope.aoColumns = [
        { "sTitle": "Medallion #" },
        { "sTitle": "Vehicle Assigned" },
        { "sTitle": "Date Joined" },
        {
            "sTitle": "Underserved",
            "fnRender": function (obj) {
                var sReturn = obj.aData[obj.iDataColumn];
                var action = " <input type=\"checkbox\" class=\"group-checkable\" data-set=\"#tblMedallion.checkboxes\" checked="+obj.aData.UnderServed+" disabled=\"disabled\" />"

                return action;
            }
        },
        { "sTitle": "Insurance Surcharge (Weekly)" },
        { "sTitle": "Collision" },
         { "sTitle": "Balance" },
          
        {
            "sTitle": "",
            "fnRender": function (obj) {
                var sReturn = obj.aData[obj.iDataColumn];
                var action = "<a href=\"\" onclick=\"angular.element(this).scope().EditMedallion('" + obj.aData.MedallionId + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                            "<a href=\"\" onclick=\"angular.element(this).scope().DeleteMedallion('" + obj.aData.MedallionId + "')\"><i class=\"fa fa-ban\"></i></a>";

                return action;
            }
        }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "MedallionNumber", "aTargets": [0] },
                        { "mDataProp": "VehicleAssigned", "aTargets": [1] },
                        { "mDataProp": "DateJoined", "aTargets": [2] },
                         { "mDataProp": "UnderServed", "aTargets": [3] },
                        { "mDataProp": "InsuranceSurcharge", "aTargets": [4] },
                        { "mDataProp": "Collision", "aTargets": [5] },
                         { "mDataProp": "Balance", "aTargets": [6] },
                        { "mDataProp": "MedallionId", "aTargets": [7] }
    ];

    //end config paging 

    $scope.Member = MemberService.getMember();
    MemberService.MedallionList($http, $scope);
    MemberService.VehicleAssignedList($http, $scope);

    //get company setting 
    MemberService.CompanySetting($http, $scope, $q).then(function(result){
        $scope.CompanySetting =result;
    });

    $scope.EditMedallion = function (id) {
        var u = $.grep($scope.MedallionList, function (e) { return e.MedallionId == id; });
        var medallionItem = u[0];
        medallionItem.MemberId = MemberService.getMember().Id;
        var modalInstance = $modal.open({
            templateUrl: '/Member/EditMedallion',
            controller: 'EditMedallionController',
            resolve: {
                medallionItem: function () {
                    return angular.copy(medallionItem);
                },
                vehicleAssignedList: function () {
                    return $scope.VehicleAssignedList
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            MemberService.MedallionList($http, $scope);

        });
    };

    $scope.AddMedallion = function () {
        var medallionItem = {
            'MedallionNumber': '',
            'VehicleId': '',
            'DateJoined': '',
            'BillingStartDate': '',
            'BillingEndDate': '',
            'UnderServed': '',
            //'InsuranceSurcharge': '',
            //'Collision': '',
            'Balance': ''
        };
        medallionItem.MemberId = $scope.Member.Id;
        switch ($scope.Member.PreferPayment) {
            case 'Weekly':
                {
                    medallionItem.InsuranceSurcharge = $scope.CompanySetting.ActualRateSet;
                    medallionItem.Collision = $scope.CompanySetting.ActualCollisionRateSet;
                }
                break;
            case 'Monthly':
                {
                    medallionItem.InsuranceSurcharge = ($scope.CompanySetting.ActualRateSet * 52) / 12;
                    medallionItem.Collision = ($scope.CompanySetting.ActualCollisionRateSet * 52) / 12;
                }
                break;
        }
        MemberService.VehicleAssignedList($http, $scope);
        var modalInstance = $modal.open({
            templateUrl: '/Member/CreateMedallion',
            controller: 'AddMedallionController',
            resolve: {
                medallionItem: function () {
                    return medallionItem;
                },
                vehicleAssignedList: function () {
                    return $scope.VehicleAssignedList;
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            MemberService.MedallionList($http, $scope);
        });
    };

    $scope.DeleteMedallion = function (id) {
        var u = $.grep($scope.MedallionList, function (e) { return e.MedallionId  == id; });
        var medallionItem = u[0];
        if (confirm("Are you sure you want to delete this item?")) {
            $http({
                method: 'POST',
                url: '/Member/DeleteMedallion',
                data: { medallionId: medallionItem.MedallionId },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    $scope.MedallionList = jQuery.grep($scope.MedallionList, function (value) {
                        return value.MedallionId != medallionItem.MedallionId;
                    });
                }
                else {
                    alert('The record is inused');
                }
            })
        }
    };
};

