var EditStandardDueController = function ($scope, $http, $modalInstance, standardDueItem) {
    $scope.standardDueItem = standardDueItem;
    $scope.IsSubmitted = false;
    $scope.SaveStandardDue = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save charge back type
            $http({
                method: 'POST',
                url: '/Member/EditStandardDue',
                data: $scope.standardDueItem,
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