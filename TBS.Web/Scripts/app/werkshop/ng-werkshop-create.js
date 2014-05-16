//window.app.controller('AddBankController', function ($scope, $http, $modalInstance, bank) {
var AddWerkShopController = function ($scope, $http, $modalInstance, werkshop, state) {
    $scope.werkshop = werkshop;
    $scope.state = state;
    $scope.IsSubmitted = false;
    
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {

            //save bank
            $http({
                method: 'POST',
                url: '/WerkShop/AddWerkShop',
                data: $scope.werkshop,
                async: false
            }).success(function (data, status, headers, config) {
                $modalInstance.close();
            });
           
        }
    };

    $scope.Cancel = function () {
         $modalInstance.dismiss('cancel');
    };
};
