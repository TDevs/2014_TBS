var EditWerkShopController = function ($scope, $http, $modalInstance, werkshop,state) {
    $scope.werkshop = werkshop;
    $scope.state = state;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {
            //save bank
            $http({
                method: 'POST',
                url: '/WerkShop/EditWerkShop',
                data: $scope.werkshop,
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