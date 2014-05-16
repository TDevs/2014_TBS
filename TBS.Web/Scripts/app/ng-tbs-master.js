
var UserLoginControler = function ($scope, $http, $modalInstance, userLogin) {
    $scope.userLogin = userLogin;
    $scope.IsSubmitted = false;
    $scope.Login = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {

            //save user
            $http({
                method: 'POST',
                url: '/User/Login',
                data: $scope.User,
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



var MasterUserLoginControler = function ($scope, $http) {
    $('.captcha-form').hide();
    var loginTime = 0;
    $scope.Login = function (isVaild) {
        $('#site_statistics_loading').show();
        var model = {
            'UserName': $scope.UserName,
            'Password': $scope.Password,
            'RememberMe': $scope.RememberMe,
            'LoginTime': loginTime
        };

        $scope.IsSubmitted = true;

        if ($('.login-form').validate().form() && $('.captcha-form').validate().form()) {
            $('.alert-warning', $('.login-form')).hide();
            $('.alert-danger', $('.login-form')).hide();
            $.ajax({
                url: '/User/Login',
                type: 'POST',
                data: model,
                success: function (data) {

                    if (data) {
                        $('.alert-success', $('.login-form')).show();
                        window.location.href = '/';
                    } else {

                        if (++loginTime > 3) {
                            $('.captcha-form').show();
                            loginTime = 0;
                        }
                        else {
                            $('.alert-warning', $('.login-form')).show();
                        }
                    }
                },
                complete: function (data) {
                    $('#site_statistics_loading').hide();
                }
            });
        }

    }
}

var MasterController = function ($scope, $http, $location, RoleService, $idle, LockService,UserService) {
    RoleService.GetCurrentRole($http, $scope);
    UserService.getNotifcations($http, $scope);
    $scope.ShowSupportMenu = function () {
        location.href = "/Support/FAQs";
    }

    $scope.ShowHome = function () {
        location.href = "/";
    }

    $scope.ShowReportMenu = function () {
        location.href = "/Report/PaymemtHistoryReport";
    }

    $scope.ShowAgentMenu = function () {
        location.href = "/Agent/Search";
    }

    $scope.ShowMemberMenu = function () {
        location.href = "/Member/Search";
    }

    $scope.ShowDashboardMenu = function () {

    }

    $scope.ShowHomePagedMenu = function () {

    }

    $scope.ShowSettingMenu = function () {

        location.href = "/User";

    }


    $scope.$on('$idleStart', function () {
        //start Idle 

    });

    $scope.$on('$idleEnd', function () {
        //end Idle

    });

    $scope.$on('$idleWarn', function (e, countdown) {
        //show warning 
    });

    $scope.$on('$idleTimeout', function () {
        //function timeout to show lock screen
        //set link back when 
        var linkback = location.hash.substr(1);
        LockService.setLinkBack(linkback);
        //log out 
        LockService.LogOut().then(function (result) {
            LockService.setCurrentUser(result);
            var id = location.hash.substr(1);
            if (id == "") id = '/';
            window.location.href = id;
        });
        // window.location.href = "/Home/LockScreen";
    });

    $scope.$on('$keepalive', function () {
        //keep alive
    })

}

