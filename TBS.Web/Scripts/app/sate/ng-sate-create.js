var AddSateController = function ($scope, $http, $modalInstance, sateItem) {
    $scope.sateItem = sateItem;
    $scope.IsSubmitted = false;
    $scope.SaveSate = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save sate
            $http({
                method: 'POST',
                url: '/Sate/AddSate',
                data: $scope.sateItem,
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