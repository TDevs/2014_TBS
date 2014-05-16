var EditModelYearInsuranceController = function ($scope, $http, $modalInstance, modelYearInsuranceItem) {
    $scope.modelYearInsuranceItem = modelYearInsuranceItem;
    $scope.IsSubmitted = false;
    $scope.SaveModelYearInsurance= function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save charge back type
            $http({
                method: 'POST',
                url: '/ModelYearInsurance/EditModelYearInsurance',
                data: $scope.modelYearInsuranceItem,
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