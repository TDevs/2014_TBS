var AddMeterManufacturerController = function ($scope, $http, $modalInstance, meterManufacturerItem) {
    $scope.meterManufacturerItem = meterManufacturerItem;
    $scope.IsSubmitted = false;
    $scope.SaveMeterManufacturer = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save meter manufacturer
            $http({
                method: 'POST',
                url: '/MeterManufacturer/AddMeterManufacturer',
                data: $scope.meterManufacturerItem,
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