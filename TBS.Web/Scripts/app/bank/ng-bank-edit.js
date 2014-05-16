var EditBankController = function ($scope, $http, $modalInstance, bank,state) {
    $scope.bank = bank;
    $scope.state = state;
    $scope.IsSubmitted = false;
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {
            //save bank
            $http({
                method: 'POST',
                url: '/Bank/EditBank',
                data: $scope.bank,
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