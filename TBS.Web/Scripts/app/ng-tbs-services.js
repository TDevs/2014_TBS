var regexIso8601 = /\/Date\((.*?)\)\//gi;
function convertDateStringsToDates(input) {
    // Ignore things that aren't objects.
    if (typeof input !== "object") return input;

    for (var key in input) {
        if (!input.hasOwnProperty(key)) continue;

        var value = input[key];
        var match;
        // Check for string properties which look like dates.
        // TODO: Improve this regex to better match ISO 8601 date strings.
        if (typeof value === "string" && (match = value.match(regexIso8601))) {
            // Assume that Date.parse can parse ISO 8601 strings, or has been shimmed in older browsers to do so.
            var parsedDate = new Date(parseInt(value.substr(6)));

            var jsDate = new Date(parsedDate);
            input[key] = pad((jsDate.getMonth() + 1)) + '/' + pad(jsDate.getDate()) + '/' + pad(jsDate.getFullYear());

        } else if (typeof value === "object") {
            // Recurse into object
            convertDateStringsToDates(value);
        }
    }
}
function pad(d) {
    return (d < 10) ? '0' + d.toString() : d.toString();
}


//control timeout 
//window.app.run(function ($rootScope, $timeout) {
//    console.log('starting run');
//    $timeout(function () {
//        otherService.updateTestService('Mellow Yellow')
//        console.log('update with timeout fired')
//    }, 3000);
//});

//loading 
window.app.config(function ($httpProvider) {
    $httpProvider.responseInterceptors.push('myHttpInterceptor');

    var spinnerFunction = function (data, headersGetter) {
        // todo start the spinner here
        var loading = "#site_statistics_loading";

        $(loading).show();
        return data;
    };
    //show loading 
    $httpProvider.defaults.transformRequest.push(spinnerFunction);
    //convert datetime json to normal datetime to display on form
    $httpProvider.defaults.transformResponse.push(function (responseData) {
        convertDateStringsToDates(responseData);
        return responseData;
    });
    //check session timeout
    $httpProvider.interceptors.push(['$injector',
    function ($injector) {
        return $injector.get('AuthInterceptor');
    }]);

})
    .factory('AuthInterceptor', function ($rootScope, $q) {
    return {
        responseError: function (response) {
            if (response.status === 401) {
                //The user is not logged in
                $rootScope.$broadcast(AUTH_EVENTS.notAuthenticated,
                                      response);
            }
            if (response.status === 403) {
                //The user is logged in but isn’t allowed access
                $rootScope.$broadcast(AUTH_EVENTS.notAuthorized,
                                      response);
            }
            if (response.status === 419 || response.status === 440) {
                //Session has expired
                $rootScope.$broadcast(AUTH_EVENTS.sessionTimeout,
                                      response);
            }
            return $q.reject(response);
        }
    };
})
// register the interceptor as a service, intercepts ALL angular ajax http calls
    .factory('myHttpInterceptor', function ($q, $window) {

        return function (promise) {
            return promise.then(function (response) {
                // do something on success
                // todo hide the spinner
                $("#site_statistics_loading").hide();
                return response;

            }, function (response) {
                // do something on error
                // todo hide the spinner
                $("#site_statistics_loading").hide();
                return $q.reject(response);
            });
        };
    })

window.app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
          .when('/Member/SearchMember', {
              //controller: 'MemberListController',
              templateUrl: '/Member/SearchMember',
              label: 'Member'
          })
        .when('/Member/Search', {
            //controller: 'MemberListController',
            templateUrl: '/Member/SearchMember',
            label: 'Member'
        })
        .when('/Member/NewInfo', {
            //controller: 'AddMemberInfoController',
            templateUrl: '/Member/NewInfo',
            label: 'Member Information'
        })
        .when('/Member/EditInfo', {
            //controller: 'EditMemberController',
            templateUrl: '/Member/EditInfo'
        })
    //Vehicle route
    .when('/Member/Vehicle', {
        //controller: 'VehicleListController',
        templateUrl: '/Member/Vehicle',
        label: 'Vehicle'
    })
    .when('/Member/NewVehicle', {
        //controller: 'VehicleNewController',
        templateUrl: '/Member/NewVehicle',
        label: 'Add New Vehicle'
    })
    //End
    //CONTACT route
    .when('/Member/ListContact', {
        //controller: 'ContactListController',
        templateUrl: '/Member/ListContact',
        label: 'Contacts'
    })
    .when('/Member/NewContact', {
        //controller: 'CreateContactController',
        templateUrl: '/Member/NewContact',
        label: 'Add New Contact'
    })
    .when('/Member/EditContact', {
        //controller: 'EditContactController',
        templateUrl: '/Member/EditContact',
        label: 'Edit Contact'
    })
    //Agent Route
     .when('/Member/ListAgent', {
         //controller: 'AgentListController',
         templateUrl: '/Member/ListAgent',
         label: 'Agents'
     })
    .when('/Member/NewAgent', {
        //controller: 'CreateAgentController',
        templateUrl: '/Member/NewAgent',
        label: 'Add New Agent'
    })
    .when('/Member/EditAgent', {
        //controller: 'EditAgentController',
        templateUrl: '/Member/EditAgent',
        label: 'Edit Agent'
    })
    //Vehicle 
     .when('/Member/Vehicle', {
         //controller: 'VehicleListController',
         templateUrl: '/Member/Vehicle',
         label: 'Vehicle'
     })
    .when('/Member/NewVehicle', {
        //controller: 'VehicleCreateController',
        templateUrl: '/Member/NewVehicle',
        label: 'Add New Vehicle'
    })
    .when('/Member/EditVehicle', {
        //controller: 'VehicleEditController',
        templateUrl: '/Member/EditVehicle',
        label: 'Edit Vehicle'
    })
    //Medallion
     .when('/Member/MedallionListing', {
         //controller: 'MedallionListController',
         templateUrl: '/Member/MedallionListing',
         label: 'Medallions'
     })
    //Standard Dues
     .when('/Member/StandardDueListing', {
         //controller: 'StandardDueListController',
         templateUrl: '/Member/StandardDueListing',
         label: 'Standard Dues'
     })
    //Cashiering
         .when('/Member/Cashiering', {
             //controller: 'InsuranceDepositController',
             templateUrl: '/Cashiering/InsuranceDeposit',
             label: 'Insurance Deposit'
         })
     .when('/Cashiering/InsuranceDeposit', {
         //controller: 'InsuranceDepositController',
         templateUrl: '/Cashiering/InsuranceDeposit',
         label: 'Insurance Deposit'
     })
     .when('/Cashiering/CCSystemAirtime', {
         //controller: 'CCSystemAirtimeController',
         templateUrl: '/Cashiering/CCSystemAirtime',
         label: 'CC System Airtime'
     })
        .when('/Cashiering/LoanList', {
            //controller: 'LoanListController',
            templateUrl: '/Cashiering/LoanList',
            label: 'Loans'
        })
        .when('/Cashiering/AddNewLoan', {
            //controller: 'NewLoanController',
            templateUrl: '/Cashiering/AddNewLoan',
            label: 'New Loan'
        })
        .when('/Cashiering/EditLoan', {
            //controller: 'EditLoanController',
            templateUrl: '/Cashiering/EditLoan',
            label: 'Edit Loan'
        })
     .when('/Cashiering/MedallionLoanSetup', {
         //controller: 'MedallionLoanSetupController',
         templateUrl: '/Cashiering/MedallionLoanSetup',
         label: 'Medallion Loan Setup'
     })
     .when('/Cashiering/AutoLoanSetup', {
         //controller: 'AutoLoanSetupController',
         templateUrl: '/Cashiering/AutoLoanSetup',
         label: 'Auto Loan Setup'
     })
     .when('/Cashiering/AccountReceivable', {
         //controller: 'AccountReceivableController',
         templateUrl: '/Cashiering/AccountReceivable',
         label: 'Account Receivable'
     })
     .when('/Cashiering/SavingDeposit', {
         //controller: 'SavingDepositController',
         templateUrl: '/Cashiering/SavingDeposit',
         label: 'Saving Deposit'
     })
     .when('/Cashiering/AccountSummary', {
         //controller: 'MedallionSummaryController',
         templateUrl: '/Cashiering/MedallionSummary',
         label: 'Medallion Summary'
     })
     .when('/Cashiering/PaymentHistory', {
         //controller: 'TransactionHistoryController',
         templateUrl: '/Cashiering/PaymentHistory',
         label: 'Payment History'
     })
     .when('/Cashiering/NewCashierMember', {
         //controller: 'NewCashierMemberController',
         templateUrl: '/Cashiering/NewCashierMember',
         label: 'New Cashier Member'
     })
    .when('/Cashiering/EditTransactionHistory', {
        //controller: 'EditTransactionHistoryController',
        templateUrl: '/Cashiering/EditTransactionHistory',
        label: 'Edit Transaction History'
    })
//search Agent 
 .when('/Agent/SearchAgent', {
     //controller: 'SearchAgentController',
     templateUrl: '/Agent/SearchAgent',
     label: 'Search Agent'
 })
     .when('/Agent/Search', {
         //controller: 'SearchAgentController',
         templateUrl: '/Agent/SearchAgent',
         label: 'Search Agent'
     })
    .when('/Agent/NewAgent', {
        //controller: 'NewAgentController',
        templateUrl: '/Agent/NewAgent',
        label: 'Search Agent'
    })
    .when('/Agent/EditAgent', {
        //controller: 'EditAgentController',
        templateUrl: '/Agent/EditAgent',
        label: 'Search Agent'
    })
    .when('/Agent/InsuranceDepositAgent', {
        //controller: 'InsuranceDepositAgentController',
        templateUrl: '/Agent/InsuranceDepositAgent',
        label: 'Insurance Deposit'
    })
    .when('/Agent/AccountReceivableAgent', {
        //controller: 'AccountReceivableAgentController',
        templateUrl: '/Agent/AccountReceivableAgent',
        label: 'Account Receivable'
    })
    .when('/Agent/SavingDepositAgent', {
        //controller: 'SavingDepositAgentController',
        templateUrl: '/Agent/SavingDepositAgent',
        label: 'Saving Deposit'
    })
    .when('/Agent/AutoLoanSetupAgent', {
        //controller: 'LoanAgentController',
        templateUrl: '/Agent/AutoLoanSetupAgent',
        label: 'Loan'
    })

    // FAQs
    .when('/Support/ViewTicket', {
        //controller: 'TicketController',
        templateUrl: '/Support/ViewTicket',
        label: 'View Ticket'
    })
        .when('/Support/ViewTicketList', {
            //controller: 'TicketController',
            templateUrl: '/Support/ViewTicketList',
            label: 'View Ticket List'
        })
    .when('/Support/Ticket', {
        //controller: 'TicketController',
        templateUrl: '/Support/ViewTicketList',
        label: 'View Ticket List'
    })
     .when('/Home/LockScreen', {
         //controller: 'LockScreenController',
         templateUrl: '/Home/LockScreen',
         label: 'Lock'
     })
    ;
}]);

//validate Percent
window.app.directive('percentInput', function () {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, el, attrs, ctrl) {

            //TODO: We need to check that the value is different to the original
            //For DOM -> model validation
            ngModel.$parsers.unshift(function (value) {
                var valid = value > 100;
                ngModel.$setValidity('percentInput', valid);
                return valid ? value : undefined;
            });

            //For model -> DOM validation
            ngModel.$formatters.unshift(function (value) {
                ngModel.$setValidity('percentInput', value > 100);
                return value;
            });
            //using push() here to run it as the last parser, after we are sure that other validators were run
            //ctrl.$parsers.push(function (viewValue) {

            //    if (viewValue) {

            //        if (viewValue<100) {
            //            ctrl.$setValidity('$scope.frmStockholder.Percentage.$error.percentInput', true);
            //            } else {
            //            ctrl.$setValidity('$scope.frmStockholder.Percentage.$error.percentInput', false);
            //            }

            //        return viewValue;
            //    }
            //});
        }
    };
})

window.app.directive('inputMask', function () {
    return {
        restrict: 'A',
        link: function (scope, el, attrs, ctrl) {
            if (attrs.ngType == 'phone') {
                $(el).inputmask(scope.$eval(attrs.inputMask));
            }
            else if (attrs.ngType == 'currency' || attrs.ngType == 'decimal') {
                $(el).inputmask('decimal', {
                    rightAlignNumerics: false
                    //numericInput: true,
                    // rightAlignNumerics: true,
                    // greedy: false
                    //999,999,999.99
                });
            }
            $(el).on('change', function (e) {
                if (attrs.ngType == 'phone') {
                    scope.$eval(attrs.ngModel + "='" + (el.val().replace(/[^\d]/g, '')) + "'");
                }
                else if (attrs.ngType == 'decimal') {
                    scope.$eval(attrs.ngModel + "=" + parseFloat(el.val().replace(/[^\d\.]/g, '')) + "");
                    scope.$apply();
                }
                else if (attrs.ngType == 'currency' || attrs.ngType == 'decimal') {
                    var value = el.val().replace(/[^\d\.\-]/g, '');
                    if (value == NaN) { value = 0; }
                    //$(el).val(value);
                    scope.$eval(attrs.ngModel + "=" + value + "");
                    scope.$apply();
                }
            });
        }
    };
});

//registry for datetime picker
window.app.directive('datetimePicker', function () {
    return {
        restrict: 'A',
        link: function (scope, el, attrs, ctrl) {
            $(el).parent().datepicker({
                rtl: App.isRTL(),
                format: attrs.datetimePicker, //'MM/DD/YYYY',
                autoclose: true
            });
            $(el).on('change', function (e) {
                scope.$eval(attrs.ngModel + "='" + el.val() + "'");
                scope.$apply();
            });
            $('body').removeClass("modal-open");
        }
    };
})

//LockService
window.app.service('LockService', function ($http, $q) {
    //this.LinkBack='';
    this.setIsStop = function (value) { this.IsStop = value; }
    this.getIsStop = function () { return this.IsStop;}
    this.setCurrentUser = function (user) { this.CurrentUser = user; }
    this.getCurrentUser = function () { return this.CurrentUser; }
    this.setLinkBack = function (linkback) { this.LinkBack = linkback; }
    this.getLinkBack = function () { return this.LinkBack; }
    this.LogOut = function () {
        var qdefer = $q.defer();
        $http({
            method: 'GET',
            url: '/Account/LogOut',
            async: true,
        }).success(function (data, status, headers, config) {
            qdefer.resolve(data);
        });
        return qdefer.promise;
    }
})

window.app.service('GeneralService', function ($http, $q) {
    var states = [];//["MN", "LS", "TX", "KS", "OH", "NY", "LA"];
    getState().then(function (result) {
        states = result;
    });

    var paymentTypes = ['Prepaid', 'Cash', 'Card', 'PayPal'];
    var preferPayment = ['Weekly', 'Monthly'];
    var status = ['Active', 'Inactive'];
    var supportStatus = [{ "name": "Open", "value": "Open" }, { "name": "Close", "value": "Close" }, { name: "In Review", "value": "InReview" }];
    var listSearchBy = [{ "name": "Search By Account Number", "value": 1 },
                        { "name": "Search By Medallion Number", "value": 2 },
                        { "name": "Search By Agents Chauffeur Number", "value": 3 },
                        { "name": "Search By SSN", "value": 4 },
                        { "name": "Search By Member Name", "value": 5 },
                        { "name": "Search By Member Contact Name", "value": 6 },
                        { "name": "Search By Agent Name", "value": 7 }];

    function getState() {

        var stateDefer = $q.defer()
        if (states != undefined && states.length > 0) {
            stateDefer.resolve(states);
        }
        else {
            $http({
                method: 'POST',
                url: '/Home/GetStates',
                async: false,
            }).success(function (data, status, headers, config) {
                // states = data;
                stateDefer.resolve(data);
            });
        }
        return stateDefer.promise;

    };

    this.States = function () { return getState() }


    this.Status = function () { return status; }
    this.PreferPayments = function () { return preferPayment; }
    this.PaymentType = function () { return paymentTypes; }
    this.SupportStatus = function () { return supportStatus; }
    this.ListSearchBy = function () { return listSearchBy; }

    this.CompanySetting = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/Company/GetCompany',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.Company = data;
        });
    }


    this.GenerateUUID = function () {
        var d = new Date().getTime();
        var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (d + Math.random() * 16) % 16 | 0;
            d = Math.floor(d / 16);
            return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
        });
        return uuid;
    };

});

//Agent service
window.app.service('AgentService', function ($http, $q, GeneralService) {
    var paymentTypes = ['Prepaid', 'Cash', 'Card', 'PayPal'];
    var preferPayment = ['Weekly', 'Monthly'];
    var status = ['Active', 'Inactive'];
    var supportStatus = [{ "name": "Open", "value": "Open" }, { "name": "Close", "value": "Close" }, { name: "In Review", "value": "InReview" }];

    this.setAgent = function (agent) { this.Agent = agent; }
    this.getAgent = function (agent) { return this.Agent; }

    this.States = function () { return GeneralService.States(); }

    this.Status = function () { return status; }
    this.PreferPayments = function () { return preferPayment; }
    this.PaymentType = function () { return paymentTypes; }
    this.SupportStatus = function () { return supportStatus; }

    this.Vehicles = function () {
        var vehdefer = $q.defer();
        $http({
            method: 'POST',
            url: '/Member/GetAllVehicles',
            async: true,
        }).success(function (data, status, headers, config) {
            vehdefer.resolve(data);
        })
        return vehdefer.promise;
    }

    //get list agentVehicle with agentId
    this.AgentVehicleList = function ($http, $scope) {
        var adefer = $q.defer();
        var agent = this.getAgent();
        if (agent != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetListVehicleByAgentId',
                data: { agentId: agent.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                adefer.resolve(data);
            });
        }
        else { adefer.resolve(null); }
        return adefer.promise;
    }


    //generation GUID 
    this.GenerateUUID = function () {
        return GeneralService.GenerateUUID();
    }

    //get Company setting to get defaut values
    this.CompanySetting = function () {
        var papromise = $q.defer();
        $http({
            method: 'GET',
            url: '/Company/GetCompany',
            async: true,
        }).success(function (data, status, headers, config) {
            $scope.CompanySetting = data;
            papromise.resolve(data);
        })
        return papromise.promise;
    }

    //get vendor
    this.CCVendorList = function () {
        var ccvendordeper = $q.defer();
        $http({
            method: 'POST',
            url: '/Vender/GetVenders',
            async: true,
        }).success(function (data, status, headers, config) {
            ccvendordeper.resolve(data);
        });
        return ccvendordeper.promise;
    }

    //get banks 
    this.BankList = function () {
        var bankdefer = $q.defer();
        $http({
            method: 'POST',
            url: '/Bank/GetBanks',
            async: true,
        }).success(function (data, status, headers, config) {
            bankdefer.resolve(data);
        });
        bankdefer.promise;
    }


    this.InsuranceDepositAgentList = function () {
        var idadefer = $q.defer();
        var agent = this.getAgent();
        if (agent == undefined) { idadefer.resolve(null); return idadefer.promise; }
        $http({
            method: 'POST',
            url: '/Agent/GetInsuranceDepositByAgentId',
            async: true, data: { agentId: agent.Id },
        }).success(function (data, status, headers, config) {
            idadefer.resolve(data);
        });
        return idadefer.promise;
    }

    this.SavingDepositAgentList = function () {
        var idadefer = $q.defer();
        var agent = this.getAgent();
        if (agent == undefined) { idadefer.resolve(null); return idadefer.promise; }
        $http({
            method: 'POST',
            url: '/Agent/GetSavingDepositByAgentId',
            async: true,
            data: { agentId: agent.Id },
        }).success(function (data, status, headers, config) {
            idadefer.resolve(data);
        });
        return idadefer.promise;
    }

    this.AccountReceivableAgentList = function () {
        var idadefer = $q.defer();
        var agent = this.getAgent();
        if (agent == undefined) { idadefer.resolve(null); return idadefer.promise; }
        $http({
            method: 'POST',
            url: '/Agent/GetAccountReceivableByAgentId',
            async: true, data: { agentId: agent.Id },
        }).success(function (data, status, headers, config) {
            idadefer.resolve(data);
        });
        return idadefer.promise;
    }

    this.AutoLoanSetupAgentList = function () {
        var idadefer = $q.defer();
        var agent = this.getAgent();
        if (agent == undefined) { idadefer.resolve(null); return idadefer.promise; }
        $http({
            method: 'POST',
            url: '/Agent/GetAutoLoanSetupByAgentId',
            async: true, data: { agentId: agent.Id },
        }).success(function (data, status, headers, config) {
            idadefer.resolve(data);
        });
        return idadefer.promise;
    }
});

window.app.service('CompanyService', function (GeneralService) {
    //get first company
    this.Company = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/Company/GetCompany',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.Company = data;
            $scope.BackChoose();
        })
    }

    this.States = function () { return GeneralService.States() };
    this.PaymentType = function () { return GeneralService.PaymentType() };
});

window.app.service('UserService', function ($q, GeneralService) {
    //get banks
    this.GetUserList = function ($http, $scope) {
        var userdefer = $q.defer();
        $http({
            method: 'GET',
            url: '/User/GetUsers',
            async: false,
        }).success(function (data, status, headers, config) {
            //$scope.Users = data;            
            userdefer.resolve(data)
        })
        return userdefer.promise;
    }
    this.States = function () { return GeneralService.States() };

});

window.app.service('RoleService', function () {
    //get banks
    this.GetRoles = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/User/GetRoles',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.Roles = data;
        })

    };

    this.GetCurrentRole = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/User/GetCurrentRole',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.role = data;
        })

    };

    this.GetUserInRoles = function ($http, $scope, userId) {

        $http({
            method: 'POST',
            url: '/User/GetUseRoles',
            data: { userId: userId },
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.Roles = data;
        })
    }


});

window.app.service('UserService', function () {


    this.getNotifcations = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/User/GetNotifications',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.Notifications = data;
        })

    };


});

window.app.service('BankService', function (GeneralService) {
    //get banks
    this.BankList = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/Bank/GetBanks',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.BankList = data;
        })
    }

    this.States = function () { return GeneralService.States() };

});

window.app.service('ChargeBackTypeService', function () {
    //get charge back types
    this.ChargeBackTypeList = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/ChargeBackType/GetChargeBackTypes',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.ChargeBackTypeList = data;
        })
    }
});

window.app.service('ModelYearInsuranceService', function () {
    //get model year insurance
    this.ModelYearInsuranceList = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/ModelYearInsurance/GetModelYearInsurances',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.ModelYearInsuranceList = data;
        })
    }
});

window.app.service('VehicleFeatureService', function () {
    //get vehicle feature
    this.VehicleFeatureList = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/VehicleFeature/GetVehicleFeatures',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.VehicleFeatureList = data;
        })
    }
});

window.app.service('VehicleMakeService', function () {
    //get vehicle make
    this.VehicleMakeList = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/VehicleMake/GetVehicleMakes',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.VehicleMakeList = data;
        })
    };
});

window.app.service('VehicleModelService', function (VehicleMakeService) {
    //get vehicle model
    this.VehicleModelList = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/VehicleModel/GetVehicleModels',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.VehicleModelList = data;
        })
    };

    this.VehicleMakeList = function ($http, $scope) { return VehicleMakeService.VehicleMakeList($http, $scope) };
});

window.app.service('VenderService', function (GeneralService) {
    //get banks
    this.VenderList = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/Vender/GetVenders',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.VenderList = data;
        })
    }

    this.States = function () { return GeneralService.States() };

});

window.app.service('WerkShopService', function (GeneralService) {
    //get banks
    this.WerkShopList = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/WerkShop/GetWerkShops',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.WerkShopList = data;
        })
    }

    this.States = function () { return GeneralService.States() };

});

window.app.service('CorporateClientService', function (GeneralService) {

    //get banks
    this.CorporateClientList = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/CorporateClient/GetCorporateClients',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.CorporateClientList = data;
        })
    }

    this.States = function () { return GeneralService.States() };

});

window.app.service('SupportService', function (GeneralService) {
    this.Ticket = new Object();

    this.setTicket = function (ticket) { this.Ticket = angular.copy(ticket); }

    this.getTicket = function () { return angular.copy(this.Ticket); }

    this.supportStatus = function () { return GeneralService.SupportStatus() };

});

window.app.service('MemberService', function (GeneralService, $q) {
    var SearchFilterList = "";
    var AccountNumber = "";
    var MemberName = "";

    this.setSearchFilter = function (searchFilter) { SearchFilterList = angular.copy(searchFilter); }
    this.getSearchFilter = function () { return SearchFilterList; }

    this.ReturnToMemberInfo = false;
    this.getReturnToMemberInfo = function () { return this.ReturnToMemberInfo };
    this.setReturnToMemberInfo = function (value) { this.ReturnToMemberInfo = angular.copy(value); }

    this.setAccountNumber = function (accountNumber) { AccountNumber = accountNumber; }
    this.getAccountNumber = function () { return AccountNumber; }
    this.setMemberName = function (memberName) { this.MemberName = memberName; }
    this.getMemberName = function () { return this.MemberName; }

    this.Member;
    this.Contact;
    this.Agent;
    this.Vehicle;
    this.GenerateUUID = function () { return GeneralService.GenerateUUID(); }
    this.CriterialSearch;
    //save criterial 
    this.setCriterialSearch = function (criterialSearch) {
        this.CriterialSearch = angular.copy(criterialSearch);
    }
    this.getCriterialSearch = function () {
        return this.CriterialSearch;
    };
    //end crirerial
    this.getMember = function () { return this.Member; }
    //set a member this function provice object to edit
    this.setMember = function (member) { this.Member = angular.copy(member); }
    //getter contact
    this.getContact = function () {
        return this.Contact;
    }
    //setter contact
    this.setContact = function (contact) {
        this.Contact = angular.copy(contact);
    }

    this.getAgent = function () {
        return this.Agent;
    }
    this.setAgent = function (agent) {
        this.Agent = angular.copy(agent);
    }

    //generate GUID
    this.GenerateUUID = function () { return GeneralService.GenerateUUID(); }
    //load states
    this.States = function () { return GeneralService.States() };
    //load status are active and inactive
    this.Status = function () { return GeneralService.Status() };
    //load prefer payment weekly or monthly
    this.PreferPayments = function () { return GeneralService.PreferPayments(); }
    //load options to search 
    this.ListSearchBy = function () { return GeneralService.ListSearchBy(); }
    //get Company setting to get defaut values
    this.CompanySetting = function ($http, $scope) {
        var papromise = $q.defer();
        $http({
            method: 'GET',
            url: '/Company/GetCompany',
            async: false,
        }).success(function (data, status, headers, config) {
            papromise.resolve(data);
            // $scope.CompanySetting = data;
        })

        return papromise.promise;
    }

    //load  all member
    this.AccountNumberMemberList = function ($http, $scope) {
        var qdefer = $q.defer();
        $http({
            method: 'POST',
            url: '/Member/GetAccountNumberListForAutoComplete',
            async: false,
        }).success(function (data, status, headers, config) {
            qdefer.resolve(data);
            //$scope.MemberList = data;
        })
        return qdefer.promise;
    };

    //load  all member
    this.MemberList = function ($http, $scope) {
        var qdefer = $q.defer();
        $http({
            method: 'POST',
            url: '/Member/GetMemberList',
            async: false,
        }).success(function (data, status, headers, config) {
            qdefer.resolve(data);
            $scope.MemberList = data;
        })
        return qdefer.promise;
    };

    this.StockholderList = function ($http, $scope) {
        var qdefer = $q.defer();
        var member = this.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetStockholderByMemberId',
                data: { member: member },
                async: false,
            }).success(function (data, status, headers, config) {
                qdefer.resolve(data);
                $scope.StockholderList = data;
            })
        }
        else { qdefer.resolve(null); }
        return qdefer.promise;
    }

    //start Contact
    this.ContactList = function ($http, $scope) {
        var qdefer = $q.defer();
        var member = this.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetListContacts',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                qdefer.resolve(data);
                $scope.ContactList = data;
            })
        }
        else { qdefer.resolve(null); }
        qdefer.promise;
    };

    //end contact

    //start Agent
    //get list agentVehicle with agentId
    this.AgentVehicleList = function ($http, $scope) {
        var qdefer = $q.defer();
        var agent = this.getAgent();
        if (agent != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetListVehicleByAgentId',
                data: { agentId: agent.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                qdefer.resolve(data);
                $scope.AgentVehicleList = data;
            });

        }
        else { qdefer.resolve(null); }
        return qdefer.promise;
    }
    this.AgentList = function ($http, $scope) {
        var qdefer = $q.defer();
        var member = this.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetListAgents',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                qdefer.resolve(data);
                $scope.AgentList = data;
            })
        } else { qdefer.resolve(null); }
        return qdefer.promise;
    };

    //end Agent

    //start Vehicle
    this.getVehicleList = function ($http, $scope) {
        var qdefer = $q.defer();
        var member = this.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetVehicles',
                data: { memberId: member.Id },
                async: true,
            }).success(function (data, status, headers, config) {
                qdefer.resolve(data);
                $scope.Vehicles = data;
            });
        } else { qdefer.resolve(null); }
        return qdefer.promise;
    };
    this.getVehicle = function () {
        return this.Vehicle;
    }
    this.setVehicle = function (vehicle) {
        this.Vehicle = angular.copy(vehicle);
    }
    //end contact

    //Medallion
    //get medallion
    this.MedallionList = function ($http, $scope) {
        var qdefer = $q.defer();
        var member = this.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetMedallions',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                qdefer.resolve(data);
                $scope.MedallionList = data;
            })
        } else { qdefer.resolve(null); }
        return qdefer.promise;
    }

    //get list vehicle assigned
    this.VehicleAssignedList = function ($http, $scope) {
        var qdefer = $q.defer();
        var member = this.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetVehicleAssigned',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                qdefer.resolve(data);
                $scope.VehicleAssignedList = data;
            })
        } else {
            qdefer.resolve(null);
        }
        qdefer.promise;
    }

    //end Medallion

    //start Standard Dues 
    //get standard due
    this.StandardDueList = function ($http, $scope) {
        var qdefer = $q.defer();
        var member = this.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetStandardDues',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                qdefer.resolve(data);
                $scope.StandardDueList = data;
            })
        } else {
            qdefer.resolve(null);
        }
        qdefer.promise;
    }
    //end Standard Dues
});

window.app.service('CashieringService', function ($http, $q, MemberService, GeneralService) {
    this.Medallion;
    this.TransactionHistory;
    this.getMedallion = function () { return this.Medallion; }
    this.setMedallion = function (medallion) { this.Medallion = angular.copy(medallion); }
    //getter and setter loan
    this.setLoan = function (loan) { this.Loan = loan; }
    this.getLoan = function () { return this.Loan }
    //getter and setter of transaction 
    this.getTransactionHistory = function () { return this.TransactionHistory; }
    this.setTransactionHistory = function (transaction) { this.TransactionHistory = angular.copy(transaction); }

    //get Company setting to get defaut values
    this.CompanySetting = function ($http, $scope) {
        var papromise = $q.defer();
        $http({
            method: 'GET',
            url: '/Company/GetCompany',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.CompanySetting = data;
            papromise.resolve(data);
        })
        return papromise.promise;
    }

    //get Current Member  
    this.Member = MemberService.getMember();

    //load states
    this.States = function () { return GeneralService.States() };

    //load status are active and inactive
    this.Status = function () { return GeneralService.Status() };

    //get vendor
    this.CCVendorList = function ($http, $scope) {
        $http({
            method: 'POST',
            url: '/Vender/GetVenders',
            async: true,
        }).success(function (data, status, headers, config) {
            $scope.CCVendorList = data;
        })
    }

    //get banks 
    this.BankList = function ($http, $scope) {
        $http({
            method: 'POST',
            url: '/Bank/GetBanks',
            async: true,
        }).success(function (data, status, headers, config) {
            $scope.BankList = data;
        })
    }

    //get VehicleList of current Member
    this.VehicleList = function ($http, $scope, $q) {
        var member = MemberService.getMember();
        var papromise = $q.defer();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetVehicles',
                data: { memberId: member.Id },
                async: true,
            }).success(function (data, status, headers, config) {
                papromise.resolve(data);
            })
            return papromise.promise;
        }
        else {
            return undefined;
        }
    }

    //get ContactList of current Member 
    this.ContactList = function ($http, $scope, $q) {
        var member = MemberService.getMember();
        var papromise = $q.defer();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetListContacts',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                papromise.resolve(data);
            })
            return papromise.promise;
        }
        else {
            return undefined;
        }
    }

    //get Medallion of current Member
    this.MedallionList = function ($http, $scope, $q) {
        var member = MemberService.getMember();
        var papromise = $q.defer();
        if (member != undefined) {

            $http({
                method: 'POST',
                url: '/Member/GetMedallionsRefer',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.MedallionList = data;
                papromise.resolve(data);
            });

        }
        else {
            return undefined;
        }
        return papromise.promise;
    };

    //get AgentList of current Member
    this.AgentList = function ($http, $scope, $q) {
        var member = MemberService.getMember();
        var papromise = $q.defer();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetListAgents',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                papromise.resolve(data);
            })
            return papromise.promise;
        }
        else {
            return undefined;
        }

    }

    //get StandardDueList of current Member 
    this.StandardDueList = function ($http, $scope, $q) {
        var member = MemberService.getMember();
        var papromise = $q.defer();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Member/GetStandardDues',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                papromise.resolve(data);
            })
            return papromise.promise;
        } else {
            return undefined;
        }
    }

    this.InsuranceDepositList = function ($http, $scope) {
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetListInsuranceDepositByMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.InsuranceDepositList = [];
                } else {
                    $scope.InsuranceDepositList = data;
                }
            })
        }
    };

    //get InsuranceDeposit  by memberId
    this.InsuranceDeposit = function ($http, $scope, $q) {
        var member = MemberService.getMember();
        var papromise = $q.defer();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetInsuranceDepositByMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.InsuranceDeposit = { 'TotalPaid': 0.0, 'CurrentBalance': -1, 'Status': 'Opened' };
                } else {
                    $scope.InsuranceDeposit = data;
                    if ($scope.InsuranceDeposit.CurrentBalance <= 0) {
                        $scope.InsuranceDeposit.Status = 'Closed';
                    }
                    papromise.resolve(data);
                }
            })
            return papromise.promise;
        }
    }

    this.CCSystemAirtimeList = function ($http, $scope) {
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetListCCSystemAirtimeByMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.CCSystemAirtimeList = [];
                } else {
                    $scope.CCSystemAirtimeList = data;
                }
            })
        }
    };

    //get CCSystemAirtime by memberId
    this.CCSystemAirtime = function ($http, $scope) {
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetCCSystemAirtimeByMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.CCSystemAirtime = { 'TotalPaid': 0.0, 'CurrentBalance': 0.0, 'Status': 'Opened' };
                } else {

                    $scope.CCSystemAirtime = data;
                    if ($scope.CCSystemAirtime.CurrentBalance <= 0) {
                        $scope.CCSystemAirtime.Status = 'Closed';
                    }
                }
            })

        }

    };

    //get loan list 
    this.LoanList = function ($scope) {
        var ldefer = $q.defer();
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetLoanListByMemberId',
                data: { id: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                ldefer.resolve(data);
            })
        }
        else {
            ldefer.resolve(null);
        }
        return ldefer.promise;
    };

    this.MedallionLoanSetupList = function ($http, $scope) {
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetListMedallionLoanSetupMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.MedallionLoanSetupList = [];
                } else {
                    $scope.MedallionLoanSetupList = data;
                }
            })
        }
    };

    //get Medallion Loans Setup
    this.MedallionLoanSetup = function ($http, $scope) {
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetMedallionLoanSetupMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.MedallionLoanSetup = { 'TotalPaid': 0.0, 'CurrentBalance': -1, 'Status': 'Opened', 'TotalPrincipalPaid': 0, 'TotalInterestPaid': 0, 'CalculatedMonthlyPayment': 0 };
                } else {
                    $scope.MedallionLoanSetup = data;
                    if ($scope.MedallionLoanSetup.CurrentBalance <= 0) {
                        $scope.MedallionLoanSetup.Status = 'Closed';
                    }
                }
            })
        }
    }


    this.AutoLoanSetupList = function ($http, $scope) {
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetListAutoLoanSetupMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.AutoLoanSetupList = [];
                } else {
                    $scope.AutoLoanSetupList = data;
                }
            })
        }
    };

    //get Auto Loans Setup
    this.AutoLoanSetup = function ($http, $scope) {
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetAutoLoanSetupMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.AutoLoanSetup = { 'TotalPaid': 0.0, 'CurrentBalance': 0.0, 'Status': 'Opened', 'TotalPrincipalPaid': 0, 'TotalInterestPaid': 0, 'CalculatedMonthlyPayment': 0 };
                } else {
                    $scope.AutoLoanSetup = data;
                    if ($scope.AutoLoanSetup.CurrentBalance <= 0) {
                        $scope.AutoLoanSetup.Status = 'Closed';
                    }
                }
            })
        }
    }

    this.SavingDepositList = function ($http, $scope) {
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetListSavingDepositMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.SavingDepositList = [];
                } else {
                    $scope.SavingDepositList = data;
                }
            })
        }
    };

    //get SavingDeposit  by memberId
    this.SavingDeposit = function ($http, $scope, $q) {
        var member = MemberService.getMember();
        var papromise = $q.defer();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetSavingDepositByMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.SavingDeposit = { 'TotalPaid': 0.0 };
                } else {
                    $scope.SavingDeposit = data;
                    //papromise.resolve(data);
                }
            })
            //return papromise.promise;
        }
    }

    this.AccountReceivableList = function ($http, $scope) {
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetListAccountReceivableMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.AccountReceivableList = [];
                } else {
                    $scope.AccountReceivableList = data;
                }
            })
        }
    };

    //get Account Receivable
    this.AccountReceivable = function ($http, $scope) {
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetAccountReceivableMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                if (data == undefined || data == "") {
                    $scope.AccountReceivable = { 'TotalPaid': 0.0, 'CurrentBalance': -1, 'Status': 'Opened', 'MonthlyPayment': 0 };
                } else {
                    $scope.AccountReceivable = data;
                    if ($scope.AccountReceivable.CurrentBalance <= 0) {
                        $scope.AccountReceivable.Status = 'Closed';
                    }
                }
            })
        }
    }

    //get Medallion Summary by member 
    this.MedallionSummaryList = function ($http, $scope) {
        var member = MemberService.getMember();
        if (member != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetMedallionSummaryListByMemberId',
                data: { memberId: member.Id },
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.MedallionSummaryList = data;

            })
        }
    };

    this.TransactionHistoryList = function ($http, $scope) {
        var member = MemberService.getMember();
        var medallion = this.Medallion;
        if (member != undefined && medallion != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetPaymentHistoryByMemberMedallionId',
                data: { memberId: member.Id, medallionId: medallion.MedallionId },
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.TransactionHistoryList = data;

            })
        }
    }

    this.MetricList = function ($http, $scope) {
        var member = MemberService.getMember();
        var medallion = this.Medallion;
        if (member != undefined && medallion != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/Get2PartSummary',
                data: { memberId: member.Id, medallionId: medallion.MedallionId },
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.MetricList = data;
            })
        }
    }

    this.BillList = function ($http, $scope) {
        var member = MemberService.getMember();
        var medallion = this.Medallion;
        if (member != undefined && medallion != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetBillsByMemberandMedallionId',
                data: { memberId: member.Id, medallionId: medallion.MedallionId },
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.BillList = data;
            })
        }
    }

    this.NewBill = function ($http, $scope) {
        var member = MemberService.getMember();
        var medallion = this.Medallion;
        if (member != undefined && medallion != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetNewBill',
                data: { memberId: member.Id, medallionId: medallion.MedallionId },
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.NewBill = data;
                $scope.addWatch();
            })
        }
    }

    this.EditBill = function ($http, $scope) {
        var transaction = $scope.TransactionHistory;
        if (transaction != undefined) {
            $http({
                method: 'POST',
                url: '/Cashiering/GetEditBill',
                data: { billId: transaction.BillId },
                async: false,
            }).success(function (data, status, headers, config) {
                $scope.NewBill = data;
                $scope.IsZeroOut = data.IsZeroOut;
                $scope.addWatch();
            })
        }
    }
});

window.app.service('MeterManufacturerService', function () {
    //get vehicle feature
    this.MeterManufacturerList = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/MeterManufacturer/GetMeterManufacturers',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.MeterManufacturerList = data;
        })
    }
});

window.app.service('SateService', function () {
    //get vehicle feature
    this.SateList = function ($http, $scope) {
        $http({
            method: 'GET',
            url: '/Sate/GetSates',
            async: false,
        }).success(function (data, status, headers, config) {
            $scope.SateList = data;
        })
    }
});

//directive to validate for user name 
window.app.directive('usernameUnique', function ($http, $resource, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var stop_timeout;
            return scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (name) {
                $timeout.cancel(stop_timeout);

                if (name === '')
                    return ngModel.$setValidity('usernameunique', true);
                //minlenght
                if (name.length <= 5) {
                    stop_timeout = $timeout(function () {
                        return ngModel.$setValidity('usernameunique', false)
                    });
                }
                //startwith numbercial
                var regex = /^(\w+)(\d*){6,20}$/;
                var partt = new RegExp(regex);
                if (!partt.test(name)) {
                    return ngModel.$setValidity('usernameunique', false)
                }

                var paramquery = attrs.paramQuery;
                $http({
                    method: 'POST',
                    url: '/User/CheckDuplicateUsername',
                    data: { username: name },
                    async: false,
                }).success(function (data, status, headers, config) {
                    stop_timeout = $timeout(function () {
                        return ngModel.$setValidity('usernameunique', data == "false");
                    });
                })

                //$resource(attrs.usernameUnique, paramquery);
            });
        }
    }
});

//directive to validate for phone US 999-999-9999
window.app.directive('validPhone', function ($http, $resource, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var stop_timeout;
            return scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (name) {
                $timeout.cancel(stop_timeout);

                if (name === '')
                    return ngModel.$setValidity('validphone', true);
                //minlenght
                if (name.length <= 8) {
                    stop_timeout = $timeout(function () {
                        return ngModel.$setValidity('validphone', false)
                    });
                }
                //startwith numbercial
                var regex = /^(\d{10})$/;
                var partt = new RegExp(regex);
                stop_timeout = $timeout(function () {
                    return ngModel.$setValidity('validphone', partt.test(name))
                });
            });
        }
    }
});

//directive to validate for zip 4 or 5
window.app.directive('validZip', function ($http, $resource, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var stop_timeout;
            return scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (name) {
                $timeout.cancel(stop_timeout);

                if (name === '')
                    return ngModel.$setValidity('validzip', true);
                //minlenght
                if (name.length <= 3) { return ngModel.$setValidity('validzip', false) }
                //startwith numbercial
                var regex = /^(\d{4,5})$/;
                var partt = new RegExp(regex);
                stop_timeout = $timeout(function () {
                    return ngModel.$setValidity('validzip', partt.test(name))
                });

            });
        }
    }
});

//directive to validate for password
window.app.directive('validPassword', function ($http, $resource, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var stop_timeout;
            return scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (name) {
                $timeout.cancel(stop_timeout);

                if (name === '')
                    return ngModel.$setValidity('validpassword', true);
                //minlenght
                if (name.length <= 7) { return ngModel.$setValidity('validpassword', false) }
                //startwith numbercial
                var regex = /((?=.+\d)((?=.+[a-z])|(?=.+[A-Z]))(?=.*).{8,20})/;
                var partt = new RegExp(regex);
                stop_timeout = $timeout(function () {
                    return ngModel.$setValidity('validpassword', partt.test(name))
                });

            });
        }
    }
});

//directive to validate for existed email
window.app.directive('emailUnique', function ($http, $resource, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var stop_timeout;
            return scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (email) {
                $timeout.cancel(stop_timeout);
                if (email == undefined) { email = ''; }
                if (email === '')
                    return ngModel.$setValidity('emailunique', true);
                if (email.length < 9) { return ngModel.$setValidity('emailunique', true); }
                $http({
                    method: 'POST',
                    url: '/User/CheckDuplicateEmail',
                    data: { email: email },
                    async: false,
                }).success(function (data, status, headers, config) {
                    stop_timeout = $timeout(function () {
                        return ngModel.$setValidity('emailunique', data == "false");
                    });
                })

            });
        }
    }
});

//directive to validate for existed email
window.app.directive('emaileditUnique', function ($http, $resource, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var stop_timeout;
            return scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (email) {
                $timeout.cancel(stop_timeout);

                if (email === '')
                    return ngModel.$setValidity('emaileditunique', true);
                if (email.length < 9) { return ngModel.$setValidity('email', true); }
                $http({
                    method: 'POST',
                    url: '/User/CheckDuplicateEmailEditUser',
                    data: { email: email, userId: scope.user.Id },
                    async: false,
                }).success(function (data, status, headers, config) {
                    stop_timeout = $timeout(function () {
                        return ngModel.$setValidity('email', data == "false");
                    });
                })

            });
        }
    }
});

//directive to validate for existed email
window.app.directive('numberlicenseUnique', function ($http, $resource, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var stop_timeout;
            return scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (email) {
                $timeout.cancel(stop_timeout);
                if (email == undefined) { email = ''; }
                if (email === '')
                    return ngModel.$setValidity('numberlicensenique', true);
                if (email.length < 4) { return ngModel.$setValidity('numberlicensenique', true); }
                $http({
                    method: 'POST',
                    url: '/Agent/CheckDuplicateAgent',
                    data: { numberlicense: email },
                    async: false,
                }).success(function (data, status, headers, config) {
                    stop_timeout = $timeout(function () {
                        return ngModel.$setValidity('numberlicensenique', data == "false");
                    });
                })

            });
        }
    }
});

//directive to validate for existed email
window.app.directive('numberlicenseeditUnique', function ($http, $resource, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var stop_timeout;
            return scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (email) {
                $timeout.cancel(stop_timeout);
                if (email == undefined) { email = ''; }
                if (email === '')
                    return ngModel.$setValidity('numberlicensenique', true);
                if (email.length < 4) { return ngModel.$setValidity('numberlicensenique', true); }
                $http({
                    method: 'POST',
                    url: '/Agent/CheckDuplicateEditAgent',
                    data: { numberlicense: email, id: scope.Agent.Id },
                    async: false,
                }).success(function (data, status, headers, config) {
                    stop_timeout = $timeout(function () {
                        return ngModel.$setValidity('numberlicensenique', data == "false");
                    });
                })

            });
        }
    }
});

//directive to validate for existed email
window.app.directive('loannameUnique', function ($http, $resource, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var stop_timeout;
            return scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (email) {
                $timeout.cancel(stop_timeout);
                if (email == undefined) { email = ''; }
                if (email === '')
                    return ngModel.$setValidity('loannameunique', true);
                if (email.length < 3) { return ngModel.$setValidity('loannameunique', true); }
                $http({
                    method: 'POST',
                    url: '/Cashiering/CheckDuplicateLoan',
                    data: { loanname: email },
                    async: false,
                }).success(function (data, status, headers, config) {
                    stop_timeout = $timeout(function () {
                        return ngModel.$setValidity('loannameunique', data == "false");
                    });
                })

            });
        }
    }
});

//directive to validate for existed email
window.app.directive('loannameeditUnique', function ($http, $resource, $timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var stop_timeout;
            return scope.$watch(function () {
                return ngModel.$modelValue;
            }, function (email) {
                $timeout.cancel(stop_timeout);
                if (email == undefined) { email = ''; }
                if (email === '')
                    return ngModel.$setValidity('loannameunique', true);
                if (email.length < 3) { return ngModel.$setValidity('loannameunique', true); }
                $http({
                    method: 'POST',
                    url: '/Cashiering/CheckDuplicateEditLoan',
                    data: { loanname: email, loanId: scope.Loan.Id },
                    async: false,
                }).success(function (data, status, headers, config) {
                    stop_timeout = $timeout(function () {
                        return ngModel.$setValidity('loannameunique', data == "false");
                    });
                })

            });
        }
    }
});

//driective for paging table 
window.app.directive('pagingTable', function ($compile) {
    return {
        restrict: 'A',
        link: function (scope, el, attrs, ctrl) {

            //registry for select 
            $('.select2me').select2({
                placeholder: "Select",
                allowClear: true
            });

            //registry for table 
            var oTable = $(el).dataTable({
                "aoColumns": scope.$eval("aoColumns"),
                "aoColumnDefs": scope.$eval("aoColumnDefs"),
                "aaSorting": [[1, 'asc']],
                "aLengthMenu": [
                   [5, 15, 20, -1],
                   [1, 2, 5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 2,
                "fnCreatedRow": function (nRow, aData, iDataIndex) {
                    ($compile(nRow)(scope)[0].innerHTML);// $compile(nRow)(scope);
                },
            });



            jQuery('#' + attrs.id + '_wrapper .dataTables_filter input').addClass("form-control input-small input-inline"); // modify table search input
            jQuery('#' + attrs.id + '_wrapper .dataTables_length select').addClass("form-control input-small"); // modify table per page dropdown
            jQuery('#' + attrs.id + '_wrapper .dataTables_length select').select2(); // initialize select2 dropdown

            $('#' + attrs.id + '_column_toggler input[type="checkbox"]').change(function () {
                /* Get the DataTables object again - this is not a recreation, just a get of the object */
                var iCol = parseInt($(this).attr("data-column"));
                var bVis = oTable.fnSettings().aoColumns[iCol].bVisible;
                oTable.fnSetColumnVis(iCol, (bVis ? false : true));
            });

            // watch for any changes to our data, rebuild the DataTable
            scope.$watch(attrs.aaData, function (value) {
                var val = value || null;
                if (val) {
                    oTable.fnClearTable();
                    oTable.fnAddData(scope.$eval(attrs.aaData));
                }
            });

        }
    };
})


//driective for CKEditor
app.directive('ckEditor', [function () {
    return {
        require: '?ngModel',
        link: function ($scope, elm, attr, ngModel) {

            var ck = CKEDITOR.replace(elm[0]);

            ck.on('pasteState', function () {
                $scope.$apply(function () {
                    ngModel.$setViewValue(ck.getData());
                });
            });

            ngModel.$render = function (value) {
                ck.setData(ngModel.$modelValue);
            };
        }
    };
}])

