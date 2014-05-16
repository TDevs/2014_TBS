var EditVehicleMakeController = function ($scope, $http, $modalInstance, vehicleMakeItem) {
    $scope.vehicleMakeItem = vehicleMakeItem;
    $scope.IsSubmitted = false;
    $scope.SaveVehicleMake = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save vehicle make
            $http({
                method: 'POST',
                url: '/VehicleMake/EditVehicleMake',
                data: $scope.vehicleMakeItem,
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