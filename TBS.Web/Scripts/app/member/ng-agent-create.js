var CreateAgentController = function ($http, $scope, $location, $modal, MemberService) {
    $scope.States = MemberService.States().then(function (result) { $scope.States = result; });;
    $scope.Status = MemberService.Status();
    $scope.IsSubmitted = false;
    $scope.Agent = {};
    $scope.AgentVehicleList = [];
    MemberService.getVehicleList($http, $scope);
    $scope.Member = MemberService.getMember();
    //save member 
    UploadImage("divUploadImage", "/Member/UploadFileImage");
    $scope.Save = function (inValid) {
        $scope.IsSubmitted = true;
        if (!inValid && $scope.Member != undefined) {
            $scope.Agent.MemberId = $scope.Member.Id
            //save agent
            $http({
                method: 'POST',
                url: '/Member/InsertAgent',
                data: { Agent: $scope.Agent, AgentVehicleList: $scope.AgentVehicleList },
                async: false
            }).success(function (data, status, headers, config) {
                alert('Save Agent Info success!');
                $("#search").attr("class", "");
                $("#info").attr("class", "active");
                $("#cash").attr("class", "");
                $location.path('/Member/ListAgent');
            });
        }
    };

    $scope.Cancel = function () {

        $("#search").attr("class", "");
        $("#info").attr("class", "active");
        $("#cash").attr("class", "");
        $location.path("/Member/ListAgent");
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
            agentVehicle.Id = MemberService.GenerateUUID();
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
}

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