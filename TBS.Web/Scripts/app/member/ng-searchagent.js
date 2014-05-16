var MainSearchAgentController = function ($http, $scope, $location, AgentService) {
    $scope.TabSearch = function () {
        $("#search").attr("class", "active");
        $("#info").removeAttr("class");
        $("#cash").removeAttr("class");
        $location.path('/Agent/Search');

    }
    $scope.TabMemberInfo = function () {
        if (AgentService.getAgent() == undefined) {
            alert('Agent not found')
            $location.path('/Agent/NewAgent');
        }
        $("#search").removeAttr("class");
        $("#info").attr("class", "active");
        $("#cash").removeAttr("class");
        $location.path('/Agent/EditAgent');

    }
    $scope.TabCashiering = function () {
        if (AgentService.getAgent() == undefined) {
            alert('Agent not found')
            $location.path('/Agent/Search');
        }

        $("#search").attr("class", "");
        $("#info").attr("class", "");
        $("#cash").attr("class", "active");
        $location.path('/Agent/CashieringAgent');

    };
};

var SearchAgentController = function ($http, $scope, $location, AgentService) {
    //intital criti
    $scope.AgentCritical = {
        'FirstName': '',
        'LastName': '',
        'Medallion': '',
        'ChaufferLic': ''
    };

    //back to Search Member 
    $scope.Cancel = function () {
        window.location.href = '/Member/Search#Member/Search';
    }

    //reset fields search
    $scope.Clear = function () {
        $scope.AgentCritical = {
            'FirstName': '',
            'LastName': '',
            'Medallion': '',
            'ChaufferLic': ''
        };
    };

    //search agent by critical
    $scope.Seach = function () {
        $http({
            method: 'POST',
            url: '/Agent/SearchAgentByCriterial',
            data: { criterial: $scope.AgentCritical },
            async: true
        }).success(function (data, status, headers, config) {
            $scope.SearchAgentList = data;
        });
    }

    $scope.Add = function () {
        //reload tab member
        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path('/Agent/NewAgent');
    }

    //swith to member Info
    $scope.Edit = function (agent) {
        //get agent  by Id 
        $http({
            method: 'POST',
            url: '/Agent/GetAgentById',
            data: { id: agent.Id },
            async: true
        }).success(function (data, status, headers, config) {
            if (data != undefined) {
                AgentService.setAgent(data);
                //reload tab member
                $("#search").attr("class", "");
                $("#info").attr("class", "active");
                $("#cash").attr("class", "");
                $location.path('/Agent/EditAgent');

            }
            else { alert("Can not find this agent"); }
        });
    }

    $scope.Delete = function (agent) {
        if (confirm("Are you sure you want to delete this agent?")) {
            //get agent  by Id 
            $http({
                method: 'POST',
                url: '/Agent/GetAgentById',
                data: { id: agent.Id },
                async: true
            }).success(function (data, status, headers, config) {
                if (data != undefined) {
                    $http({
                        method: 'POST',
                        url: '/Agent/DeleteAgent',
                        data: { agent: data },
                        async: true
                    }).success(function (data, status, headers, config) {
                        alert("Delete success!");
                        $scope.SearchAgentList = jQuery.grep($scope.SearchAgentList, function (value) {
                            return value.Id != agent.Id;
                        });
                    });
                }
                else { alert("Can not find this agent"); }
            });
        }
    }
};

var NewAgentController = function ($http, $scope, $modal, $location, AgentService) {
    $scope.Agent = {};
    $scope.AgentVehicleList = [];
    AgentService.Vehicles().then(function (result) {
        $scope.Vehicles = result;
    });

    $scope.IsSubmitted = false;
    AgentService.States().then(function (result) {
        $scope.States = result;
    });
    $scope.Status = AgentService.Status();
    //upload picture of profile
    UploadImage("divUploadImage", "/Member/UploadFileImage");
    function UploadImage(id, urlServer) {
        // Initialize the jQuery File Upload plugin
        $('#' + id).fileupload({
            url: urlServer,
            type: 'post',
            dataType: 'json',
            autoUpload: true,
            maxFileSize: 10000000,
            acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
            formData: {},
            // This function is called when a file is added to the queue;
            // either via the browse button, or via drag/drop:
            add: function (e, data) {
                // Automatically upload the file once it is added to the queue
                var jqXHR = data.submit();

            },
            success: function (result, textStatus, jqXHR) {
                $scope.Agent.PictureAgent = result;
                $scope.$apply();
            },
            fail: function (e, data) {
                // Something has gone wrong!
                data.context.addClass('error');
            }

        });
    }

    $scope.Save = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {
            $http({
                method: 'POST',
                url: '/Agent/NewAgent',
                data: { agent: $scope.Agent, AgentVehicleList: $scope.AgentVehicleList },
                async: true
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    alert("Save success!");
                    //redirect to edit agent                      
                    $("#search").attr("class", "active");
                    $("#info").attr("class", "");
                    $("#cash").attr("class", "");
                    $location.path('/Agent/Search');
                }
                else { alert("Can not save this agent"); }
            });
        }
    }

    $scope.Cancel = function () {
        //reload tab member
        $("#search").attr("class", "active");
        $("#info").attr("class", "");
        $("#cash").attr("class", "");
        $location.path('/Agent/Search');
    }

    $scope.NewAgentVehicle = function () {
        var agentVehicle = {};
        var modalInstance = $modal.open({
            templateUrl: '/Member/NewAgentVehicle',
            controller: 'AgentVehicleController',
            resolve: {
                agentVehicle: function () {
                    return agentVehicle;
                },
                Vehicles: function () {
                    return $scope.Vehicles;
                }

            }
        });

        //reload list
        modalInstance.result.then(function (agentVehicle) {
            agentVehicle.Id = AgentService.GenerateUUID();
            var vehicle = $.grep($scope.Vehicles, function (e) { return e.Id == agentVehicle.VehicleId; });
            if (vehicle != undefined) {
                agentVehicle.LicenseNumber = vehicle[0].LicenseNumber;
            }
            $scope.AgentVehicleList.push(agentVehicle);

        });
    }

    $scope.DelAgentVehicle = function (agentvehicle) {
        $scope.AgentVehicleList = jQuery.grep($scope.AgentVehicleList, function (value) {
            return value.Id != agentvehicle.Id;
        });
    };
};

var EditAgentController = function ($http, $scope, $modal, $location, AgentService) {
    $scope.IsSubmitted = false;
    AgentService.States().then(function (result) {
        $scope.States = result;
    })
    AgentService.AgentVehicleList($http, $scope).then(function (result) {
        $scope.AgentVehicleList = result;
    });

    AgentService.Vehicles().then(function (result) {
        $scope.Vehicles = result;
    });

    $scope.Agent = AgentService.getAgent();
    $scope.Status = AgentService.Status();
    //upload picture of profile
    UploadImage("divUploadImage", "/Member/UploadFileImage");
    function UploadImage(id, urlServer) {
        // Initialize the jQuery File Upload plugin
        $('#' + id).fileupload({
            url: urlServer,
            type: 'post',
            dataType: 'json',
            autoUpload: true,
            maxFileSize: 10000000,
            acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
            formData: {},
            // This function is called when a file is added to the queue;
            // either via the browse button, or via drag/drop:
            add: function (e, data) {
                // Automatically upload the file once it is added to the queue
                var jqXHR = data.submit();

            },
            success: function (result, textStatus, jqXHR) {
                $scope.Agent.PictureAgent = result;
                $scope.$apply();
            },
            fail: function (e, data) {
                // Something has gone wrong!
                data.context.addClass('error');
            }

        });
    }

    $scope.Save = function (invalid) {
        $scope.IsSubmitted = true;
        if (!invalid) {
            $http({
                method: 'POST',
                url: '/Agent/EditAgent',
                data: { agent: $scope.Agent, AgentVehicleList: $scope.AgentVehicleList },
                async: true
            }).success(function (data, status, headers, config) {
                if (data == "true") {
                    alert("Save success!");
                    //redirect to edit agent                      
                    $("#search").attr("class", "active");
                    $("#info").attr("class", "");
                    $("#cash").attr("class", "");
                    $location.path('/Agent/Search');
                }
                else { alert("Can not save this agent"); }
            });
        }
    }

    $scope.Cancel = function () {
        //reload tab member
        $("#search").attr("class", "active");
        $("#info").attr("class", "");
        $("#cash").attr("class", "");
        $location.path('/Agent/Search');
    }

    $scope.NewAgentVehicle = function () {
        var agentVehicle = {};
        var modalInstance = $modal.open({
            templateUrl: '/Member/NewAgentVehicle',
            controller: 'AgentVehicleController',
            resolve: {
                agentVehicle: function () {
                    return agentVehicle;
                },
                Vehicles: function () {
                    return $scope.Vehicles;
                }

            }
        });

        //reload list
        modalInstance.result.then(function (agentVehicle) {
            agentVehicle.Id = AgentService.GenerateUUID();
            agentVehicle.IsNew = true;
            var vehicle = $.grep($scope.Vehicles, function (e) { return e.Id == agentVehicle.VehicleId; });
            if (vehicle != undefined) {
                agentVehicle.AgentVehicle.Id = agentVehicle.Id;
                agentVehicle.AgentVehicle.VehicleId = agentVehicle.VehicleId;
                agentVehicle.AgentVehicle.AgentFee = agentVehicle.AgentFee;
                agentVehicle.LicenseNumber = vehicle[0].LicenseNumber;
            }
            $scope.AgentVehicleList.push(agentVehicle);

        });
    }

    $scope.DelAgentVehicle = function (agentresult) {
        if (agentresult.IsNew) {
            $scope.AgentVehicleList = jQuery.grep($scope.AgentVehicleList, function (value) {
                return value.AgentVehicle.Id != agentresult.AgentVehicle.Id;
            });
        } else {
            $http({
                method: 'POST',
                url: '/Member/DelAgentVehicle',
                data: { agentvehicle: agentresult.AgentVehicle },
                async: false
            }).success(function (data, status, headers, config) {
                AgentService.AgentVehicleList($http, $scope).then(function (result) {
                    $scope.AgentVehicleList = result;
                });
            });
        }
    };
};

//controller for Agent Vehicle
var AgentVehicleController = function ($scope, $http, $modalInstance, agentVehicle, Vehicles) {
    $scope.VehicleList = Vehicles;
    $scope.AgentVehicle = agentVehicle;
    $scope.IsSubmitted = false;
    $scope.Save = function (inValid) {
        $scope.IsSubmitted = true;
        if (!inValid) {
            $modalInstance.close($scope.AgentVehicle);
        }
    };

    $scope.Cancel = function () {
        $modalInstance.dismiss('cancel');
    };
}