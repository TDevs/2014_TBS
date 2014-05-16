var VehicleListController = function ($scope, $http, $location, MemberService) {
    //start config paging
    $scope.aoColumns = [
        { "sTitle": "License Number" },
        { "sTitle": "License State" },
        {
            "sTitle": "Make",
            "fnRender": function (obj) {
                var sReturn = obj.aData.VehicleMake;
                if (sReturn != null) {
                    return sReturn.Name;
                }
                return "";
            }
        },
        {
            "sTitle": "Model",
            "fnRender": function (obj) {
                var sReturn = obj.aData.VehicleModel;
                if (sReturn != null) {
                    return sReturn.Name;
                }
                return "";
            }
        },
        { "sTitle": "Year" },
        { "sTitle": "EIN" },
        
        { "sTitle": "VIN" },
         { "sTitle": "Medallion#" },
          {
              "sTitle": "Underserved",
              "fnRender": function (obj) {
                  var sReturn = obj.aData[obj.iDataColumn];
                  var action = " <input type=\"checkbox\" class=\"group-checkable\" checked=" + obj.aData.UnderServed + " disabled=\"disabled\" />"

                  return action;
              }
          },
        {
            "sTitle": "",
            "fnRender": function (obj) {
                var sReturn = obj.aData[obj.iDataColumn];
                var action = "<a href=\"\" onclick=\"angular.element(this).scope().EditVehicle('" + obj.aData.Id + "')\">" +
                                "<i class=\"fa fa-edit\"></i>" +
                            "</a>" +
                            "<a href=\"\" onclick=\"angular.element(this).scope().DeleteVehicle('" + obj.aData.Id + "')\"><i class=\"fa fa-ban\"></i></a>";

                return action;
            }
        }
    ];

    $scope.aoColumnDefs = [
                        { "mDataProp": "LicenseNumber", "aTargets": [0] },
                        { "mDataProp": "LicenseState", "aTargets": [1] },
                        { "mDataProp": "LicenseState", "aTargets": [2] },
                         { "mDataProp": "LicenseState", "aTargets": [3] },
                        { "mDataProp": "ModelYear", "aTargets": [4] },
                        { "mDataProp": "EINNumber", "aTargets": [5] },
                         { "mDataProp": "VINNumber", "aTargets": [6] },
                        { "mDataProp": "Medallion", "aTargets": [7] },
                        { "mDataProp": "UnderServed", "aTargets": [8] },
                        { "mDataProp": "Id", "aTargets": [9] }
                        
    ];

    //end config paging 


    var member = MemberService.getMember();
    if (member != null) {
        $scope.AddVehicle = function () {

            $location.path('/Member/NewVehicle');

        };

        $scope.EditVehicle = function (id) {
            var u = $.grep($scope.Vehicles, function (e) { return e.Id == id; });
            var vehicle = u[0];
            MemberService.setVehicle(vehicle);
            window.location.href = "#/Member/EditVehicle";
            // $location.path("/Member/EditVehicle");
        };



        $scope.DeleteVehicle = function (id) {
            var u = $.grep($scope.Vehicles, function (e) { return e.Id == id; });
            var vehicle = u[0];
            if (confirm("Are you sure you want to delete this vehicle?")) {
                $http({
                    method: 'POST',
                    url: '/Member/DeleteVehicle',
                    data: vehicle,
                    async: false,
                }).success(function (data, status, headers, config) {
                    if (data == "true") {
                        $("#search").attr("class", "");
                        $("#info").attr("class", "active");
                        $("#cash").attr("class", "");
                        $location.path("/Member/Vehicle");
                    }
                    else {
                        alert('The record is inused');
                    }
                })
            }
        };


        MemberService.getVehicleList($http, $scope);

    }
    else {
        alert('Member data is not setup');
    }
};


var VehicleCreateController = function ($scope, $http, $location, MemberService, GeneralService) {    
    $scope.Member = MemberService.getMember();
    $scope.FormTitle = 'Create a new Vehicle';
    $scope.IsSubmitted = false;
    $scope.States = GeneralService.States().then(function (result) {
        $scope.States = result;
    });
    
    $scope.VehicleModels = new Array();
    $scope.Medallions = new Array();
    $scope.VehicleMakes = new Array();
    $scope.VehicleFeatures = new Array();
    $scope.MeterManufacturers = new Array();
    $scope.ModelYears = new Array();
    $scope.memberId = $scope.Member.Id;

    $scope.vehicle = {
        'MemberId ': $scope.Member.Id,
        'LicenseNumber ': '',
        'VehicleMakeId ': '',
        'VehicleModelId ': '',
        'VehicleFeatureId ': '',
        'MeterManufacturerId ': '',
        'VINNumber ': '',
        'InsurancePolicyNumber ': '',
        'WorkmanCompNumber ': '',
        'LicenseState ': '',
        'RadioSerialNumber ': '',
        'MeterNumber ': '',
        'EINNumber ': '',
        'MedallionId ': '',
        'ModelYear ': '',
        'UnderServed ': ''
    };
    $scope.Save = function (isVaild) {
        $scope.IsSubmitted = true;
        $scope.vehicle.memberId = $scope.Member.Id;
        if (isVaild) {
            //save vehicle
            $http({
                method: 'POST',
                url: '/Member/NewVehicle',
                data: $scope.vehicle,
                async: true
            }).success(function (data, status, headers, config) {
                alert("Save vehicle successfull");
                $("#search").attr("class", "");
                $("#info").attr("class", "active");
                $("#cash").attr("class", "");
                $location.path("/Member/Vehicle");
            });

        }
    };


    GetVehicleMakes($http, $scope);
    GetVehiclFeatures($http, $scope);
    GetVehiclModels($http, $scope);
    GetVehiclModelYears($http, $scope);
    GetManufactures($http, $scope);
    GetMedallions($http, $scope);

    $scope.Cancel = function () {

        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path("/Member/Vehicle");
    }
};

var VehicleEditController = function ($scope, $http, $location, MemberService, GeneralService) {
    
    $scope.vehicle = MemberService.getVehicle();
    $scope.memberId = $scope.vehicle.MemberId;
    $scope.IsSubmitted = false;
    $scope.States = GeneralService.States().then(function (result) {
        $scope.States = result;
    });
    $scope.FormTitle = 'Edit Vehicle';
    $scope.Save = function (valid) {
        $scope.IsSubmitted = true;

        if (valid) {
            //save vehicle
            $http({
                method: 'POST',
                url: '/Member/EditVehicle',
                data: $scope.vehicle,
                async: true
            }).success(function (data, status, headers, config) {
                alert("Save vehicle successfull");
                $("#search").attr("class", "");
                $("#info").attr("class", "active");
                $("#cash").attr("class", "");
                $location.path("/Member/Vehicle");
            });
        }


    };

    GetVehicleMakes($http, $scope);
    GetVehiclFeatures($http, $scope);
    GetVehiclModels($http, $scope);
    GetVehiclModelYears($http, $scope);
    GetManufactures($http, $scope);
    GetMedallions($http, $scope);

    $scope.Cancel = function () {

        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path("/Member/Vehicle");
    }
};


function GetMemberVehicle($http, $scope) {
    var memberId = $scope.MemberId;

    $http({
        method: 'POST',
        url: '/Member/GetVehicle',
        data: { memderId: memberId },
        async: false,
    }).success(function (data, status, headers, config) {
        if (data) {
            UserService.GetUserList($http, $scope);
        }
        else {
            alert('You cannot delete yourself');
        }
    })
}


function GetVehicleMakes($http, $scope) {

    $http({
        method: 'GET',
        url: '/VehicleMake/GetVehicleMakes',
        async: true,
    }).success(function (data, status, headers, config) {
        if (data) {
            $scope.VehicleMakes = data;
        }
        else {
            alert('Errors');
        }
    })
}


function GetVehiclFeatures($http, $scope) {

    $http({
        method: 'GET',
        url: '/VehicleFeature/GetVehicleFeatures',
        async: true,
    }).success(function (data, status, headers, config) {
        if (data) {
            $scope.VehicleFeatures = data;
        }
        else {
            alert('Errors');
        }
    })
}



function GetMedallions($http, $scope) {
    $http({
        method: 'POST',
        url: '/Member/GetMemberMedallions',
        async: true,
        data: { memberId: $scope.memberId }
    }).success(function (data, status, headers, config) {
        if (data) {
            $scope.Medallions = data;
        }
        else {
            alert('Errors');
        }
    })
}


function GetVehiclModels($http, $scope) {

    $http({
        method: 'GET',
        url: '/VehicleModel/GetVehicleModels',
        async: true,
    }).success(function (data, status, headers, config) {
        if (data) {
            $scope.VehicleModels = data;
        }
        else {
            alert('Errors');
        }
    })
}

function GetManufactures($http, $scope) {

    $http({
        method: 'GET',
        url: '/Member/GetManufactures',
        async: true,
    }).success(function (data, status, headers, config) {
        if (data) {
            $scope.MeterManufacturers = data;
        }
        else {
            alert('Errors');
        }
    })
}

function GetVehiclModelYears($http, $scope) {

    $http({
        method: 'GET',
        url: '/Member/GetModelYears',
        async: true,
    }).success(function (data, status, headers, config) {
        if (data) {
            $scope.ModelYears = data;
        }
        else {
            alert('Errors');
        }
    })
}

