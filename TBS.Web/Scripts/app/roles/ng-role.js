var RoleListController = function ($scope, $http, $modal, RoleService) {

    //config paging table
    $scope.aoColumns = [
            { "sTitle": "Name" },
             { "sTitle": "Desciption" },
              { "sTitle": "Predefined" },
            {
                "sTitle": "",
                "fnRender": function (obj) {
                    var sReturn = obj.aData[obj.iDataColumn];
                    var action = "  <a href=\"\"  onclick=\"angular.element(this).scope().EditRole('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                    "<a href=\"\"  onclick=\"angular.element(this).scope().DeleteRole('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-ban\"></i>"
                            + "</a>";

                    return action;
                }
            }
    ];

    $scope.aoColumnDefs = [
                            { "mDataProp": "Name", "aTargets": [0] },
                             { "mDataProp": "Description", "aTargets": [1] },
                              { "mDataProp": "Predefined", "aTargets": [2] },
                            { "mDataProp": "Id", "aTargets": [3] }
    ];
    //end config

    RoleService.GetRoles($http, $scope);
    $scope.AddRole = function () {
        var role = {
            'Name': '',
            'Description': '',
            'Predefined': '',
            'AccountCanEdit': false,
            'AccountCanView': false,
            'AgentCanCreate': false,
            'MedallionCanDelete': false,
            'ViewMemberData': false,
            'EditMemberData': false,
            'DeleteMemberData': false,
            'ViewMemberCashiering': false,
            'BillOutMember': false,
            'ViewDeletedMembers': false,
            'ReportCanMakeReport': false,
            'ReportCanMakeEndOfDayTrans': false,
            'RoleCanViewList': false,
            'RoleCanEdit': false,
            'RoleCanCreate': false,
            'RoleCanDelete': false,
            'UserCanViewList': false,
            'UserCanEdit': false,
            'UserCanCreate': false,
            'UserCanDelete': false,

            'ReferenceCanEdit': false,
            'AccountCanEdit': false,
            'AccountCanEdit': false,

            'AllowDashboard': false
        };
        var modalInstance = $modal.open({
            templateUrl: '/Role/Create',
            controller: 'RoleCreateController',
            resolve: {
                role: function () {
                    return role;
                }
            }
        });

        //reload list
        modalInstance.result.then(function () {
            RoleService.GetRoles($http, $scope);

        });
    };
    RoleService.GetCurrentRole($http, $scope);
    $scope.EditRole = function (id) {
        var u = $.grep($scope.Roles, function (e) { return e.Id == id; });
        var role = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/Role/Edit',
            controller: 'RoleEditController',
            resolve: {
                role: function () {
                    return angular.copy(role);
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            RoleService.GetRoles($http, $scope);

        });
    };

    $scope.DeleteRole = function (id) {
        var u = $.grep($scope.Roles, function (e) { return e.Id == id; });
        var role = u[0];
        if (confirm("Are you sure you want to delete this role?")) {
            $http({
                method: 'POST',
                url: '/Role/Delete',
                data: role,
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.Roles = jQuery.grep($scope.Roles, function (value) {
                    return value.Id != bank.Id;
                });
            })
        }
    };


};


var RoleCreateController = function ($scope, $http, $modalInstance, role) {
    $scope.role = role;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {

            //save user
            $http({
                method: 'POST',
                url: '/Role/Create',
                data: $scope.role,
                async: false
            }).success(function (data, status, headers, config) {
                $modalInstance.close();
            });

        }
    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

var RoleEditController = function ($scope, $http, $modalInstance, role) {
    $scope.role = role;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {
            //save EditRole
            $http({
                method: 'POST',
                url: '/Role/Edit',
                data: $scope.role,
                async: true
            });
            $modalInstance.close();
        }


    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};

