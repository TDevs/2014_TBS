var AddVehicleFeatureController = function ($scope, $http, $modalInstance, vehicleFeatureItem) {
    $scope.vehicleFeatureItem = vehicleFeatureItem;
    $scope.IsSubmitted = false;
    $scope.SaveVehicleFeature = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save vehicle feature
            $http({
                method: 'POST',
                url: '/VehicleFeature/AddVehicleFeature',
                data: $scope.vehicleFeatureItem,
                async: false,
            }).success(function (data, status, headers, config) {
                $modalInstance.close();
            });
        }
    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
};