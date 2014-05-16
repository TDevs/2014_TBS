var UserListController = function ($scope, $http, $modal, UserService, GeneralService, RoleService) {
    $scope.Users = [];
    UserService.GetUserList($http, $scope).then(function (result) {
        $scope.Users = result;
    });

    $scope.aoColumns = [
        { "sTitle": "Login" },
        { "sTitle": "Full Name" },
        { "sTitle": "Address" },
        { "sTitle": "City" },
        { "sTitle": "State" },
        { "sTitle": "Phone" },
        { "sTitle": "e-mail" },
        {
            "sTitle": "Active",
            "fnRender": function (obj) {
                var sReturn = obj.aData[obj.iDataColumn];
                if(sReturn)
                    return " <input type='checkbox' readonly checked='true'/>";
                else
                    return " <input type='checkbox' readonly checked = 'false'/>";
                return action;
            }
        },
        {
            "sTitle": "",
            "fnRender": function (obj) {
                var sReturn = obj.aData[obj.iDataColumn];
                 
                var action = "<a href='#' title='Edit' ng-show='role.UserCanEdit' onclick=\"angular.element(this).scope().EditUser('"+obj.aData.Id+"')\">" +
                                    "<i class='fa fa-edit'></i>" +
                                "</a>" +
                                "<a href='#' title='Delete' ng-show='role.UserCanDelete'  onclick=\"angular.element(this).scope().DeleteUser('" + obj.aData.Id + "')\">" +
                                    "<i class='fa fa-ban'></i>" +
                                "</a>" +
                                "<a href='#' title='Roles' ng-show='role.UserCanEdit' onclick=\"angular.element(this).scope().EditRoles('" + obj.aData.Id + "')\">" +
                                    "<i class='fa fa-users'></i>" +
                                "</a>"
                return action;
            }
        }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "UserName", "aTargets": [0] },
                        { "mDataProp": "FullName", "aTargets": [1] },
                        { "mDataProp": "Address", "aTargets": [2] },
                        { "mDataProp": "City", "aTargets": [3] },
                        { "mDataProp": "State", "aTargets": [4] },
                        { "mDataProp": "Phone1", "aTargets": [5] },
                        { "mDataProp": "Email", "aTargets": [6] },
                        { "mDataProp": "Active", "aTargets": [7] },
                        { "mDataProp": "Id", "aTargets": [8] }
    ];


    GeneralService.States().then(function (result) { $scope.States = result; });
    RoleService.GetCurrentRole($http, $scope);

    $scope.AddUser = function () {
        var user = {
            'UserName': '',
            'Password': '',
            'FirstName': '',
            'LastName': '',
            'SecretQuestion': '',
            'SecretAnswer': '',
            'Email': '',
            'Phone1': '',
            'Phone2': '',
            'Address1': '',
            'Address2': '',
            'City': '',
            'State': '',
            'Zip': '',
            'Fax': '',
            'Active': true

        };
        var modalInstance = $modal.open({
            templateUrl: '/User/Create',
            controller: 'UserCreateController',
            resolve: {
                user: function () {
                    return user;
                },
                states: function () { return $scope.States }
            }
        });

        //reload list
        modalInstance.result.then(function () {
            UserService.GetUserList($http, $scope);

        });
    };

    $scope.EditUser = function (id) {
        var u = $.grep($scope.Users, function (e) { return e.Id == id; });
        var user = u[0];
        user.Password = '';
        if (user.SecretQuestion == undefined) { user.SecretQuestion = ''; }
        if (user.SecretAnswer == undefined) { user.SecretAnswer = ''; }
        if (user.Phone2 == undefined) { user.Phone2 = ''; }
        if (user.Fax == undefined) { user.Fax = ''; }
        var modalInstance = $modal.open({
            templateUrl: '/User/Edit',
            controller: 'UserEditController',
            resolve: {
                user: function () {
                    return angular.copy(user);
                }, States: function () { return $scope.States }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            UserService.GetUserList($http, $scope);

        });
    };

    $scope.EditRoles = function (id) {
        var u = $.grep($scope.Users, function (e) { return e.Id == id; });
        var user = u[0];
        var modalInstance = $modal.open({
            templateUrl: '/User/Roles',
            controller: 'UserRoleController',
            resolve: {
                user: function () {
                    return user;
                }
            }
        });
        //reload list
        modalInstance.result.then(function () {
            UserService.GetUserList($http, $scope);

        });
    };

    $scope.DeleteUser = function (id) {
        var u = $.grep($scope.Users, function (e) { return e.Id == id; });
        var user = u[0];
        if (confirm("Are you sure you want to delete this user?")) {
            $http({
                method: 'POST',
                url: '/User/Delete',
                data: user,
                async: false,
            }).success(function (data, status, headers, config) {
                if (data) {
                    UserService.GetUserList($http, $scope);
                }
                else {
                    alert('You cannot delete yourself');
                }
            })
        }
    };
};


var UserCreateController = function ($scope, $http, $modalInstance, user, GeneralService) {
    $scope.user = user;
    $scope.IsSubmitted = false;
    $scope.States = GeneralService.States().then(function (result) { $scope.States = result; });
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (isVaild) {
            //save user
            $http({
                method: 'POST',
                url: '/User/Create',
                data: $scope.user,
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

var UserEditController = function ($scope, $http, $modalInstance, user, GeneralService) {
    $scope.user = user;
    $scope.IsSubmitted = false;
    $scope.States = GeneralService.States().then(function (result) { $scope.States = result; });
    $scope.Save = function (valid) {
        $scope.IsSubmitted = true;
        alert(valid);
        if (valid) {
            //save user
            $http({
                method: 'POST',
                url: '/User/Edit',
                data: $scope.user,
                async: true
            }).success(function (data, status, headers, config) {
                $modalInstance.close();
            });
        }


    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};


var UserRoleController = function ($scope, $http, $modalInstance, user, RoleService) {
    $scope.Roles = RoleService.GetUserInRoles($http, $scope, user.Id);

    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = false;
        if (!isVaild) {
            $scope.IsSubmitted = true;
            //save user
            $http({
                method: 'POST',
                url: '/User/EditRoles',
                data: $scope.Roles,
                async: false
            }).success(function (data, status, headers, config) {

                $modalInstance.close();
            });
        }
    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    $scope.EditUserRole = function (roleId) {

        var index = 0;
        var log = [];
        angular.forEach($scope.Roles, function (value, key) {
            if (value.RoleId == roleId) {
                value.IsInRole = !value.IsInRole;
                $scope.$apply();
                return;
            }
            else {
                value.IsInRole = false;// A specific User cannot have multiple Roles
            }
            index++;
        }, log);
    };
};

