var AddStandardDueController = function ($scope, $http, $modalInstance, standardDueItem) {
    $scope.standardDueItem = standardDueItem;
    $scope.IsSubmitted = false;
    $scope.SaveStandardDue = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save standard due
            $http({
                method: 'POST',
                url: '/Member/AddStandardDue',
                data: $scope.standardDueItem,
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