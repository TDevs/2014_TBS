window.app = angular.module('TDevLog', []);

//loading 
window.app.config(function ($httpProvider) {
    $httpProvider.responseInterceptors.push('myHttpInterceptor');
    var spinnerFunction = function (data, headersGetter) {
        // todo start the spinner here
        var loading = "#site_statistics_loading";

        $(loading).show();
        return data;
    };
    $httpProvider.defaults.transformRequest.push(spinnerFunction);
})
// register the interceptor as a service, intercepts ALL angular ajax http calls
    .factory('myHttpInterceptor', function ($q, $window) {

        return function (promise) {
            return promise.then(function (response) {
                // do something on success
                // todo hide the spinner
                $("#site_statistics_loading").hide();
                return response;

            }, function (response) {
                // do something on error
                // todo hide the spinner
                $("#site_statistics_loading").hide();
                return $q.reject(response);
            });
        };
    })

 
var LockscreenController = function ($scope, $http, $location) {
    $scope.User = {};
    $http({
        method: 'GET',
        url: ' /Account/GetUserInfo',      
        async: true,
    }).success(function (data, status, headers, config) {
        $scope.User = data;
    });
    $scope.IsSubmitted = false;


    $scope.Relogin = function (isvalid) {
        $scope.IsSubmitted = true;
        $scope.login = true;
        if (isvalid) {
            //relogin /User/Login
            var userinfo = {
                'UserName': $scope.User.UserName,
                'Password': $scope.User.Password,
                'RememberMe': false,
                'LoginTime': 0
            }
            $http({
                method: 'POST',
                url: ' /User/ReLogin',
                data: userinfo,
                async: true,
            }).success(function (data, status, headers, config) {
                if (data == "true") {                    
                      window.location.href = ("/Home/Index");                     
                }
                else {
                    $scope.login =  false;
                }
            });
        }
    }
}


