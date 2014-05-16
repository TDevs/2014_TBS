//window.app.controller('AddBankController', function ($scope, $http, $modalInstance, bank) {
var AddCorporateClientController = function ($scope, $http, $modalInstance, corporateclient, state) {
    $scope.corporateclient = corporateclient;
    $scope.state = state;
    $scope.IsSubmitted = false;
    
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        if (!isVaild) {

            //save bank
            $http({
                method: 'POST',
                url: '/CorporateClient/AddCorporateClient',
                data: $scope.corporateclient,
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
