var EditMemberController = function ($scope, $http, $modal, MemberService, $location) {
    $scope.Status = MemberService.Status();
    $scope.States = MemberService.States().then(function (result) { $scope.States = result; });;
    $scope.PreferPayments = MemberService.PreferPayments();
    MemberService.StockholderList($http, $scope);

    $scope.IsSubmitted = false;
    $scope.IsStockholderSubmitted = false; 
    //save member 
    $scope.Save = function (inValid) {
        $scope.IsSubmitted = true;
        if (!inValid) {
            //save bank
            $http({
                method: 'POST',
                url: '/Member/EditMemberInfo',
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

    $scope.$watch('MemberService.Member', function (newval,oldval) {
        if (newval != oldval)
        {            
            $scope.Member = newval;
        }
    },true);

    //call modal to save new Stockholder
    $scope.NewStockholder = function () {
        var stockholder = { 'Id': '', 'MemberId': $scope.Member.Id, 'StockholderName': '', };
        var modalInstance = $modal.open({
            templateUrl: '/Member/NewStockholder',
            controller: 'AddStockholderController',
            resolve: {
                stockholder: function () {
                    return stockholder;
                }
            }
        });

        //reload list
        modalInstance.result.then(function () {
            MemberService.StockholderList($http, $scope);
        });
    };

    $scope.EditStockholder = function (stockholder) {
        var modalInstance = $modal.open({
            templateUrl: '/Member/EditStockholder',
            controller: 'EditStockholderController',
            resolve: {
                stockholder: function () {
                    return stockholder;
                }
            }
        });

        //reload list
        modalInstance.result.then(function () {
            MemberService.StockholderList($http, $scope);
        });
    };

    $scope.Cancel = function () {

        $("#search").attr("class", "active");
        $("#info").attr("class", "");
        $("#cash").attr("class", "");
        $location.path("/Member/Search");
    }

    $scope.DelStockholder = function (stockholder) {
        //delete stockholder
        if (confirm("Are you sure you want to delete this stockholder?")) {
            $('#site_statistics_loading').show();
            $http({
                method: 'POST',
                url: '/Member/DelStockholder',
                data: stockholder,
                async: true
            }).success(function (data, status, headers, config) {
                if (data) {
                    MemberService.StockholderList($http, $scope);
                }

                $('#site_statistics_loading').hide();
            });
        }
    }
}

//controller New Stockholder 
var AddStockholderController = function ($scope, $http, $modalInstance, stockholder) {
    $scope.stockholder = stockholder;
    $scope.IsSubmitted = false;
    //save Stockholder
    $scope.Save = function (inValid) {
        $('#site_statistics_loading').show();
        $scope.IsSubmitted = true;
        if (!inValid) {
            $http({
                method: 'POST',
                url: '/Member/InsertStockholder',
                data: $scope.stockholder,
                async: false
            }).success(function (data, status, headers, config) {
                $modalInstance.close();
                $('#site_statistics_loading').hide();
            });
        }
    }

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
}

//controller Edit Stockholder 
var EditStockholderController = function ($scope, $http, $modalInstance, stockholder) {
    $scope.IsSubmitted = false;
    $scope.stockholder = stockholder;
    $scope.Save = function (inValid) {
        $('#site_statistics_loading').show();
        $scope.IsSubmitted = true;
        if (!inValid) {
            $http({
                method: 'POST',
                url: '/Member/UpdateStockholder',
                data: $scope.stockholder,
                async: false
            }).success(function (data, status, headers, config) {
                $modalInstance.close();
                $('#site_statistics_loading').hide();
            });
        }
    }

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
}