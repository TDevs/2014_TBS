var CCSystemAirtimeController = function ($http, $scope, $q, CashieringService) {
    
   

    $scope.CCSystemAirtimeList = CashieringService.CCSystemAirtimeList($http, $scope);
    $scope.CCSystemAirtime = CashieringService.CCSystemAirtime($http, $scope, $q);
    $scope.CompanySetting = CashieringService.CompanySetting($http, $scope, $q);
    $scope.MedallionList = CashieringService.MedallionList($http, $scope, $q);
    $scope.CCVendorList = CashieringService.CCVendorList($http, $scope);

    //$scope.Member = CashieringService.Member();
    $scope.IsSubmitted = false;

    $scope.MedallionChange = function (medallionId) {
        $scope.IsSubmitted = false;
        var ccsystemairtime = $.grep($scope.CCSystemAirtimeList, function (e) { return e.MedallionId == medallionId; });
        $scope.CCSystemAirtime = {'MedallionId':medallionId};
        if (ccsystemairtime != undefined && ccsystemairtime[0] != undefined) {
            $scope.CCSystemAirtime = angular.copy(ccsystemairtime[0]);
        }         
    }

    $scope.VendorChange = function (vendorId)
    {
        $scope.IsSubmitted = false;
        var vendor = $.grep($scope.CCVendorList, function (e) { return e.Id == vendorId; });
         
        if (vendor != undefined && vendor[0] != undefined) {
            $scope.CCSystemAirtime.Airtime = vendor[0].Airtime;
        }
    }

    $scope.Save = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {
            $scope.CCSystemAirtime.MemberId = $scope.Member.Id;
            $http({
                method: 'POST',
                url: '/Cashiering/SaveCCSystemAirtime',
                data: $scope.CCSystemAirtime,
                async: false,
            }).success(function (data, status, headers, config) {
                alert("Save CCSystem Airtime success!");
                //switch to 
                CashieringService.CCSystemAirtimeList($http, $scope);
            })
        }
    }

  
    

    
};