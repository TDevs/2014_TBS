var AddModelYearInsuranceController = function ($scope, $http, $modalInstance, modelYearInsuranceItem) {
    $scope.modelYearInsuranceItem = modelYearInsuranceItem;
    $scope.IsSubmitted = false;
    $scope.SaveModelYearInsurance = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save charge back type
            $http({
                method: 'POST',
                url: '/ModelYearInsurance/AddModelYearInsurance',
                data: $scope.modelYearInsuranceItem,
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