//window.app.controller('AddBankController', function ($scope, $http, $modalInstance, bank) {
var AddVenderController = function ($scope, $http, $modalInstance, vender,state) {
    $scope.vender = vender;
    $scope.state = state;
    $scope.IsSubmitted = false;
    
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {

            //save bank
            $http({
                method: 'POST',
                url: '/Vender/AddVender',
                data: $scope.vender,
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
