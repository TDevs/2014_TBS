var EditCorporateClientController = function ($scope, $http, $modalInstance, corporateclient, state) {
    $scope.corporateclient = corporateclient;
    $scope.state = state;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {
            //save CorporateClient
            $http({
                method: 'POST',
                url: '/CorporateClient/EditCorporateClient',
                data: $scope.corporateclient,
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