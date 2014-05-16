var EditMedallionController = function ($scope, $http, $modalInstance, medallionItem, vehicleAssignedList) {
    $scope.medallionItem = medallionItem;
    $scope.vehicleAssignedList = vehicleAssignedList;
    $scope.IsSubmitted = false;
    $scope.EditMedallion = function (isValid) {
        $scope.IsSubmitted = true;
        if (!isValid) {
            //save vehicle model
            $scope.medallionItem.Id = $scope.medallionItem.MedallionId;
            //$scope.medallionItem.MemberId = MemberService.getMember().Id;
            $http({
                method: 'POST',
                url: '/Member/EditMedallion',
                data: $scope.medallionItem,
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