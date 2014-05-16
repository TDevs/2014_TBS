var EditMeterManufacturerController = function ($scope, $http, $modalInstance, meterManufacturerItem) {
    $scope.meterManufacturerItem = meterManufacturerItem;
    $scope.IsSubmitted = false;
    $scope.SaveMeterManufacturer = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save vehicle make
            $http({
                method: 'POST',
                url: '/MeterManufacturer/EditMeterManufacturer',
                data: $scope.meterManufacturerItem,
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