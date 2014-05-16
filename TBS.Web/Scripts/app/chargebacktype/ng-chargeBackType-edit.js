//myApp.controller('EditChargeBackTypeController', function ($scope, $http, $modalInstance, chargeBackTypeItem) {
var EditChargeBackTypeController = function ($scope, $http, $modalInstance, chargeBackTypeItem) {
    $scope.chargeBackTypeItem = chargeBackTypeItem;
    $scope.IsSubmitted = false;
    $scope.SaveChargeBackType = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save charge back type
            $http({
                method: 'POST',
                url: '/ChargeBackType/EditChargeBackType',
                data: $scope.chargeBackTypeItem,
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