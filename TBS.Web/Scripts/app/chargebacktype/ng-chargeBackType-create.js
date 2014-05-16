//myApp.controller('AddChargeBackTypeController', function ($scope, $http, $modalInstance, chargeBackTypeItem) {
var AddChargeBackTypeController = function ($scope, $http, $modalInstance, chargeBackTypeItem) {
    $scope.chargeBackTypeItem = chargeBackTypeItem;
    $scope.IsSubmitted = false;
    $scope.SaveChargeBackType = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save charge back type
            $http({
                method: 'POST',
                url: '/ChargeBackType/AddChargeBackType',
                data: $scope.chargeBackTypeItem,
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