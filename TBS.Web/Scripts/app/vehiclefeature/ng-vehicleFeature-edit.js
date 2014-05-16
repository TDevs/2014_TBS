var EditVehicleFeatureController = function ($scope, $http, $modalInstance, vehicleFeatureItem) {
    $scope.vehicleFeatureItem = vehicleFeatureItem;
    $scope.IsSubmitted = false;
    $scope.SaveVehicleFeature = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save vehicle feature
            $http({
                method: 'POST',
                url: '/VehicleFeature/EditVehicleFeature',
                data: $scope.vehicleFeatureItem,
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