var AddMedallionController = function ($scope, $http, $modalInstance, medallionItem, vehicleAssignedList) {
    $scope.medallionItem = medallionItem;
    $scope.vehicleAssignedList = vehicleAssignedList;    
    $scope.IsSubmitted = false;
    $scope.AddMedallion = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save vehicle model
            $http({
                method: 'POST',
                url: '/Member/AddMedallion',
                data: $scope.medallionItem,
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