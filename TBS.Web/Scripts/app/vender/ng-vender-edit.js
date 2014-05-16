var EditVenderController = function ($scope, $http, $modalInstance, vender, state) {
    $scope.state = state;
    $scope.vender = vender;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {
            //save bank
            $http({
                method: 'POST',
                url: '/Vender/EditVender',
                data: $scope.vender,
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