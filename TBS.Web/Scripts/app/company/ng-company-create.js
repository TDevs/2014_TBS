 

var CompanyController = function ($scope, $http, CompanyService) {

    $scope.Company = {
        'UseCompanyAddress': false,
        'Address1': '',
        'Address2': ''
    }

    CompanyService.States().then(function (result) { $scope.States = result;});
    $scope.PaymentType = CompanyService.PaymentType();
    $scope.IsSubmitted = false;
    CompanyService.Company($http, $scope);
    $scope.SearchBy = 0;
   
    $scope.$watch('Company.UseCompanyAddress', function (newval, oldval) {
        if (newval) {
            $scope.Company.AgentAddress1 = $scope.Company.Address1;
            $scope.Company.AgentAddress2 = $scope.Company.Address2;
            $scope.Company.AgentZip = $scope.Company.Zip;
            $scope.Company.AgentState = $scope.Company.State;
            $scope.Company.AgentCity = $scope.Company.City;
        }
    })

    $scope.$watch('Company.Address1', function (newval,oldval) {
        if ($scope.Company.UseCompanyAddress && newval != oldval)
        {
            $scope.Company.AgentAddress1 = $scope.Company.Address1;
             
        }
    })

    $scope.$watch('Company.Address2', function (newval,oldval) {
        if ($scope.Company.UseCompanyAddress && newval != oldval) {
            $scope.Company.AgentAddress2 = newval;
           
        }
    })

    $scope.$watch('Company.State', function (newval, oldval) {
        if ($scope.Company.UseCompanyAddress && newval != oldval) {
            $scope.Company.AgentState = newval;

        }
    })

    $scope.$watch('Company.City', function (newval, oldval) {
        if ($scope.Company.UseCompanyAddress && newval != oldval) {
            $scope.Company.AgentCity = newval;

        }
    })

    $scope.$watch('Company.Zip', function (newval, oldval) {
        if ($scope.Company.UseCompanyAddress && newval != oldval) {
            $scope.Company.AgentZip = newval;
        }
    })

    $scope.Save = function (isValid) {
        $scope.IsSubmitted = true;
        if (isValid) {
            $scope.ChooseSearch($scope.SearchBy);
            //save company
            $http({
                method: 'POST',
                url: '/Company/SaveCompany',
                data: $scope.Company,
                async: false
            }).success(function (data, status, headers, config) {
                if (data) {
                    alert("Saving success");
                }
                else {
                    alert("Occured error during save data");
                }
            });
        }
    };

    $scope.ChooseSearch = function (number) {
        //SeachByAccountNumber
        if (number == 1) {
            $scope.Company.SeachByAccountNumber = true;
            $scope.Company.SearchByMedallionNumber = false;
            $scope.Company.SearchByAgentChaufferNumber = false;
            $scope.Company.SearchBySSN = false;
            $scope.Company.SearchByMemberName = false;
            $scope.Company.SearchByMemberContactName = false;
            $scope.Company.SearchByAgentLastName = false;
        }
        //SearchByMedallionNumber
        if (number == 2) {
            $scope.Company.SeachByAccountNumber = false;
            $scope.Company.SearchByMedallionNumber = true;
            $scope.Company.SearchByAgentChaufferNumber = false;
            $scope.Company.SearchBySSN = false;
            $scope.Company.SearchByMemberName = false;
            $scope.Company.SearchByMemberContactName = false;
            $scope.Company.SearchByAgentLastName = false;
        }
        //SearchByAgentChaufferNumber
        if (number == 3) {
            $scope.Company.SeachByAccountNumber = false;
            $scope.Company.SearchByMedallionNumber = false;
            $scope.Company.SearchByAgentChaufferNumber = true;
            $scope.Company.SearchBySSN = false;
            $scope.Company.SearchByMemberName = false;
            $scope.Company.SearchByMemberContactName = false;
            $scope.Company.SearchByAgentLastName = false;
        }
        //SearchBySSN
        if (number == 4) {
            $scope.Company.SeachByAccountNumber = false;
            $scope.Company.SearchByMedallionNumber = false;
            $scope.Company.SearchByAgentChaufferNumber = false;
            $scope.Company.SearchBySSN = true;
            $scope.Company.SearchByMemberName = false;
            $scope.Company.SearchByMemberContactName = false;
            $scope.Company.SearchByAgentLastName = false;
        }
        //SearchByMemberName
        if (number == 5) {
            $scope.Company.SeachByAccountNumber = false;
            $scope.Company.SearchByMedallionNumber = false;
            $scope.Company.SearchByAgentChaufferNumber = false;
            $scope.Company.SearchBySSN = false;
            $scope.Company.SearchByMemberName = true;
            $scope.Company.SearchByMemberContactName = false;
            $scope.Company.SearchByAgentLastName = false;
        }
        //SearchByMemberContactName
        if (number == 6) {
            $scope.Company.SeachByAccountNumber = false;
            $scope.Company.SearchByMedallionNumber = false;
            $scope.Company.SearchByAgentChaufferNumber = false;
            $scope.Company.SearchBySSN = false;
            $scope.Company.SearchByMemberName = false;
            $scope.Company.SearchByMemberContactName = true;
            $scope.Company.SearchByAgentLastName = false;
        }
        //SearchByAgentLastName
        if (number == 7) {
            $scope.Company.SeachByAccountNumber = false;
            $scope.Company.SearchByMedallionNumber = false;
            $scope.Company.SearchByAgentChaufferNumber = false;
            $scope.Company.SearchBySSN = false;
            $scope.Company.SearchByMemberName = false;
            $scope.Company.SearchByMemberContactName = false;
            $scope.Company.SearchByAgentLastName = true;
        }
    };

     $scope.BackChoose= function() {
        if ($scope.Company.SeachByAccountNumber) { $scope.SearchBy = 1};
        if ($scope.Company.SearchByMedallionNumber) { $scope.SearchBy = 2};
        if ($scope.Company.SearchByAgentChaufferNumber ) { $scope.SearchBy = 3};
        if ($scope.Company.SearchBySSN ) { $scope.SearchBy = 4};
        if ( $scope.Company.SearchByMemberName) { $scope.SearchBy = 5};
        if ($scope.Company.SearchByMemberContactName ) { $scope.SearchBy = 6};
        if ($scope.Company.SearchByAgentLastName) { $scope.SearchBy = 7 };
    };
    
};